# [Askowl Decoupler](http://www.askowl.net/unity-decoupler-package)

[TOC]

> Read the code in the Examples Folder.

## Introduction
Decoupling software components and systems have been a focus for many decades. In the 80s we talked about software black boxes. You didn't care what was inside, just on the inputs and outputs.

Microsoft had a lot of fun in the 90's designing and implementing COM and DCOM. I still think of this as the high point in design for supporting decoupled interfaces.

Now we have Web APIs, REST or SOAP interfaces and microservices. Design patterns such as the Factory Pattern are here to "force" decoupling at the enterprise software level. There have been dozens of standards over the years.

Despite this, programmers have continued to create tightly coupled systems even while enforcing the requirements of the framework.

Consider a simple example. I have an app that uses a Google Maps API to translate coordinates into a description "Five miles south-west of Gundagai". My app is running on an iPhone calling into a cloud of Google servers. The hardware is different and remote, and they both use completely different software systems. But, my app won't run, or at least perform correctly, without Google. Worse still if I am using a Google library, it won't even compile without a copy.

## What is the Askowl Decoupler
First and foremost, the Askowl Decoupler is a way to decouple your app from packages in the Unity3D ecosystem.

It works at the C# class level, meaning that it does not provide the physical separation. That is provided by the Unity packages when needed. In approach, it acts very much like a C# Interface.

## What does the Askowl Decoupler give me?w
1. You can build and test your app while waiting for supporting Unity packages to be complete.
2. You can choose between unity packages without changing your app code. Changing from Google Analytics to Unity Analytics to Fabric is as simple as getting or writing the connector code.
3. You can provide a standard interface to a related area. For social media, the interface could support FaceBook, Twitter, Youtube and others. You could then send a command to one, some or all of them. Think of this regarding posting to multiple platforms.
4. You can have more than one service then cycle through them or select one at random. For advertising, you can move to a new platform if the current one cannot serve you an ad.

## Decoupling Packages

### How do I use a decoupled package?
Always get an instance through static methods on the interface.

#### For singleton services
Access the registered service using the Instance selector. If keeping a reference, set it in Awake or later. It gives the services an opportunity to register.
```C#
Decoupled.Authentication auth;
void Awake() { auth = Decoupled.Authentication.Instance; }
```

#### To cycle through a list of services
Access the next registered service using the Instance selector.
```C#
        Adze.Server server = Adze.Server.Instance;
        int cycleIndex = server.CycleIndex;
        do {
          yield return server.Show(currentMode);
          if  (server.error) break;
          server = Adze.Server.Instance;
        } while (server.CycleIndex != cycleIndex);
```
In the example, the code will cycle through all the advertising services, stopping when one display an ad or when the list has been exhausted.

#### To select a named service
All services have a name. Names are set by either specifying the name in `Register`/`Load` or using the default name is the class name of the service. A service can then be retrieved by name using `Named`.

```C#
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RegisterService() { Social.Register <Facebook>(); }

```
```C#
  Decoupled.Social facebook = Decoupled.Social.Named("Facebook");
```

#### Send to All Services
Another type of service is to have multiple instances, and we will need to do something with all of them. It could be anything from display a list of names for user selection or call a method on some or all of them. `Social` is one of these where we may be connected to multiple social networks and send a message to some.

```C#
Decoupled.Social.ForEach((svs) => svs.Send(myMessage));
```

#### To choose a service randomly
`Random()` and `Exhaustive()` are static methods on the interface. Random selection can cause a perceived imbalance with short lists. Exhaustive is also a random picker, but it ensures all choices are exhausted before starting again.

```C#
  Decoupled.Social.Random();
  Decoupled.Social.Exhaustive();
```

#### How do I know if there is a service implemented
All service interfaces have a static member `Available`.

```C#
  if (!Decoupled.Social.Available) Debug.Log("Oops");
```

### How much work do I need to do to implement a decoupler?
#### For an already written decoupled package
##### If it comes with a controller or *prefab*
1. Create an empty gameObject in the first scene of your game
2. Drag the controller code or *prefab* to the gameObject
3. Fill any requirements in the controller from the Unity editor
4. Run the app. The decoupled package will replace the default placeholder
##### If it has an initialiser in an Editor directory
There is nothing more to do.

In either case, if external dependencies are needed, you will see a message in the log.

#### For a new package and an existing interface
1. Create a new project
2. Import any unity packages required
4. Create an API where the base class is the interface
5. Implement the virtual methods as needed (using override)
6. Create a loader method to register this service

A sample service would look something like this.
```C#
#if AnalyticsFabric
  public sealed class AnalyticsFabric: Analytics {
    public override void Event(string name){/* etc etc */}
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RegisterService() { Analytics.Register<AnalyticsFabric>(); }
  }
#endif
```
By using ***Askowl.DefineBuild** set a definition file in an ***Editor*** directory. Here we are deciding on the existence of a package by the existence or not of a directory.

```C#
  [InitializeOnLoad]
  public sealed class DetectMyUnityPackage : DefineSymbols {
    static DetectMyUnityPackage() {
      bool usable = HasFolder("Fabric");
      AddOrRemoveDefines(addDefines: usable, named: "AnalyticsFabric");
    }
  }
}
```

#### For a new interface
The decoupler interface is not an Interface in the Java/C# sense. It is a base class. It provides decoupling support as well as default functionality.

If the decoupler interface is for a new package you are writing, then the methods are a matter for software design. If it is an existing package, then the contents will reflect the functionality you want to access. It can be just the parts you need or if for distribution, it may be exhaustive. For an example of the latter, look at ***Askowl-Decoupler/Services/Analytics***. There are multiple classes here to represent different aspects of the analytics requirement.

```C#
namespace Decoupled.Analytics {
  public class Play : Decoupled.Service<Play> {

    public virtual void Screen(string name, string clazz) {
      Debug.Log("**** Screen '" + name + "' - " + clazz);
    }
  // ...more
}
```

Most interface methods will do nothing. Since analytics is a form of logging, it is best to display to the console by default. In our production code, we may override the interface with Firebase Analytics for Android and iOS, but falling back to the default for the Editor. We might even choose a different analytics system for OS X, Windows or Windows Phone.

Often when the interface is for remote services, results will be asynchronous.

```C#
namespace Decoupled {
  public class Authentication: Decoupled.Service<Authentication> {

    public class User {
      public string Name = "guest";
      public string Email = "";
      public string PhotoUrl = "";
      public string PhoneNumber = "";
      public string ProviderId = "";
      public string UserId = "";
      public bool IsVerified = false;
      public bool IsLoggedIn = false;
      public object MetaData = null;
    }

    public User user = new User ();

    public virtual IEnumerator CreateUser(string email, string password, Action<string> error = null) {
      user.Name = user.Email = email;
      yield return null;
    }
  // ... more
}
```

In a service that will talk to a server, the yield will wait for a response. This example also shows an inner data class. Each application will have to fill it from server supplied data.

It may seem like a lot of work, but it is quite simple. Writing an interface is a matter of learning what is available and deciding what is required.

### Built-In Interfaces
To use a decoupled service, you will need to have an interface class. Often these will be provided with packages that need them, but for commonly needed ones, we have included them in Askowl Decoupler.

* <!--[Analytics](Analytics.md)-->
* <!--[Authentication](Authentication.md)-->
* <!--[Database](Database.md)-->
* <!--[DynamicLinks](DynamicLinks.md)-->
* <!--[Invites](Invites.md)-->
* <!--[Messaging](Messaging.md)-->
* <!--[RemoteConfig](RemoteConfig.md)-->
* <!--[Social](Social.md)-->
* <!--[Storage](Storage.md)-->

## Decoupling Components

### How do I use a decoupled components?
### Built-In Interfaces
### Decoupled.TextComponent