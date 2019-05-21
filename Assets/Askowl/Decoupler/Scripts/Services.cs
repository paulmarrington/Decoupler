// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.Linq;
using Askowl;
using CustomAsset;
using JetBrains.Annotations;
using UnityEngine;
using Cache = Askowl.Cache;
using Object = System.Object;

namespace Decoupler.Services {
  /// <a href="http://bit.ly/2uRJubf">Interface for services managers</a>
  public interface IServicesManager {
    IServiceAdapter First();
    IServiceAdapter Next();
//    Fiber.IClosure  CallProvider(IService service);
  }
  /// <a href="http://bit.ly/2uRJubf">Interface for service contexts</a>
  public interface IContext { }
  /// <a href="http://bit.ly/2uRJubf">Interface for service adapters and concrete instances</a>
  public interface IServiceAdapter { }

  public class Entry<TRequest, TResponse> : Fiber.Closure {
    public TRequest  request;
    public TResponse response;
    public string    Error;
    public Emitter   Emitter = Emitter.Instance;
    public bool      Failed => Error != null;

    public static T Call<T>(TRequest request) where T : Entry<TRequest, TResponse> {
      var instance = Cache<T>.Instance;
      instance.request = request;
      instance.Fiber.Go();
      return instance;
    }

    private static string serviceName, entryPointName;

    private IServicesManager manager;
    private IServiceAdapter  serviceAdapter;

    public static void Name(string service, string entry) {
      serviceName    = service;
      entryPointName = entry;
    }

    protected override void Activities(Fiber fiber) {
      manager = (IServicesManager) Managers.Find($"{serviceName}ServicesManager");
      if (manager == default)
        Debug.LogError(Error = $"No service manager '{serviceName}ServicesManager' in a Managers game object");
      else
        fiber.OnError(exit: true, actor: message => Error = message).Do(Reset)
             .Begin
             .WaitFor(_ => MethodCache.Call(serviceAdapter, "Call", this) as Emitter)
             .Until(_ => (Error == null) || ((serviceAdapter = manager.Next()) == null))
             .If(_ => serviceAdapter == null).Error($"No Server '{serviceName} {entryPointName}' can Respond").End
             .Finish();
    }

    private void Reset(Fiber fiber) {
      Error          = null;
      serviceAdapter = manager.First();
    }
  }
  /// <inheritdoc cref="services" />
  /// <a href="http://bit.ly/2uRJubf">Separate selection and service from context for easy Inspector configuration</a>
  public class Services<TS, TC> : Manager, IServicesManager
    where TS : Services<TS, TC>.ServiceAdapter
    where TC : Services<TS, TC>.Context {
    /// <a href="http://bit.ly/2PMWqLs">How the next service request is filled</a>
    // ReSharper disable MissingXmlDoc
    public enum Order { TopDown, RoundRobin, Random, RandomExhaustive }
    // ReSharper restore MissingXmlDoc

    [SerializeField] private TS[]  services = default;
    [SerializeField] private TC    context  = default;
    [SerializeField] private Order order    = Order.TopDown;

    /// <a href="http://bit.ly/2PMWqLs">List of possible service - to be filtered by platform, etc</a>
    public TS[] ServiceList => services;

    /// <a href="http://bit.ly/2uZxnc1">Selector instance - public for use in testing</a>
    public Selector<TS> selector;
    /// <a href="http://bit.ly/2uZxnc1">How many times to use this service before asking another</a>
    [HideInInspector] public int usagesRemaining;
    /// <a href="http://bit.ly/2uZxnc1">The service currently being used</a>
    [HideInInspector] public TS currentService;

    /// <inheritdoc />
    protected override void Initialise() {
      // only use services that are valid for the current context
      var useful = services.Where(service => context.Equals(service.context) && service.IsExternalServiceAvailable());
      services = useful.ToArray();
      // service processing order is dependent on the priority each gives
      Array.Sort(array: services, comparison: (x, y) => x.priority.CompareTo(value: y.priority));

      selector = new Selector<TS> {
        IsRandom        = order > Order.RoundRobin
      , ExhaustiveBelow = order == Order.RandomExhaustive ? services.Length + 1 : 0
      , Choices         = services
      };
      usagesRemaining = 0;
    }

    /// <a href="http://bit.ly/2uZxnc1">Get the next service instance given selection order and repetitions</a>
    public IServiceAdapter First() {
      if (order == Order.TopDown) {
        selector.Top();
      } else if (--usagesRemaining > 0) {
        return currentService;
      }
      currentService  = selector.Pick();
      usagesRemaining = currentService.usageBalance;
      return currentService;
    }

    /// <a href="http://bit.ly/2uZxnc1">If the last service fails, ask for another. If none work, returns null</a>
    public IServiceAdapter Next() {
      currentService = selector.Next();
      if (currentService == default) return default;
      usagesRemaining = currentService.usageBalance;
      return currentService;
    }

    /// <inheritdoc cref="ServiceAdapter" />
    /// <a href="http://bit.ly/2uRJubf">Parent class for decoupled services</a>
    [Serializable] public abstract class ServiceAdapter : Base, IServiceAdapter {
      /// <a href="http://bit.ly/2uZxnc1">Used to sort service so most important are first</a>
      [SerializeField] internal int priority = 1;
      /// <a href="http://bit.ly/2uZxnc1">How many repeats a service has before a new one is asked for</a>
      [SerializeField] internal int usageBalance = 1;
      /// <a href="http://bit.ly/2uZxnc1">Context to be compared with running service to see if we can use this one</a>
      [SerializeField] public TC context = default;

      /// <a href="http://bit.ly/2uRJubf">Helper method to log a message to the analytics system</a>
      protected Log.MessageRecorder Log;
      /// <a href="http://bit.ly/2uRJubf">Helper to log an error to the analytics system</a>
      protected Log.EventRecorder Error;

      /// <a href="http://bit.ly/2uUKZ8x">Concrete service implements this to prepare for action</a>
      protected abstract void Prepare();

      /// <a href="http://bit.ly/2Pf34e4">Set by concrete external services</a>
      public abstract bool IsExternalServiceAvailable();

      /// <inheritdoc />
      /// <a href="http://bit.ly/2uUKZ8x">Override to initialise concrete service instances</a>
      protected override void Initialise() {
        base.Initialise();
        Log   = Askowl.Log.Messages();
        Error = Askowl.Log.Errors();
        Prepare();
      }
    }

    /// <inheritdoc cref="IContext" />
    /// <a href="http://bit.ly/2uZxnc1">Context used to share common service data and select valid services</a>
    [Serializable] public class Context : Base, IContext {
      /// <a href="http://bit.ly/2uZxnc1">Production, Staging, Test, Dev or user defined environment</a>
      [SerializeField] private Environment environment = default;

      /// <a href="http://bit.ly/2uZxnc1">Compare services to see if the current one can be used</a>
      // ReSharper disable once VirtualMemberNeverOverridden.Global
      protected virtual bool Equals(Context other) => base.Equals(other) && Equals(environment, other.environment);

      /// <inheritdoc />
      public override int GetHashCode() {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        unchecked { return (base.GetHashCode() * 397) ^ (environment != null ? environment.GetHashCode() : 0); }
        // ReSharper restore NonReadonlyMemberInGetHashCode
      }
    }
  }
}