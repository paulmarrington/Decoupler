//- Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
//- The Askowl Decoupler is here to provide an interface between your code and Unity packages. This video shows how to call decoupled packages. There are other videos on creation and testing.
// ReSharper disable MissingXmlDoc

using System.Collections;
using CustomAsset;
using Decoupler.Services;
using UnityEngine.TestTools;

#if !ExcludeAskowlTests
namespace Askowl.Transcripts {
  public class CallServiceTranscript {
    private void CallDecoupledService() { }
    [UnityTest] public IEnumerator CallDecoupledServiceExample() {
      //- A test is without a managers game object in the scene. So, we poke a copy in manually. It will only work in the editor, but now we can use the service manager just like we would in production.
      Managers.Add<ServiceExampleServicesManager>($"TopDownAddServiceManager.asset");
      //- We would not usually need to retain a reference to the service being called, but for test we want to wait until it is done.
      var add =
        //- Following is the only line that would be needed to make a call in production code entering request data

        // ****** START PRODUCTION-LIKE CODE ******
        ServiceExampleServiceAdapter.Add.Call((11, 12));
      // ****** END PRODUCTION-LIKE CODE ******

      //- Wait for the asynchronous call to complete before stopping the test
      return Fiber.Start().WaitFor(add.OnComplete).AsCoroutine();
    }
////- Use a fiber closure to mitigate concurrency issues. There will only be as many instances of the AddFiber closure as there are ever concurrent calls. In practice a service entry point will only have a few calls waiting on a response at any one time. The Activities method is only called once when an AddFiber instance is created. It precompiles the fiber. The static Go method fetches a closure from the recycle stack, initialises the scope and sets it running. When the fiber is done you have 10 frames (1/6th of a second) to collect and data you want before the closure is cleaned up and placed back in recycling for future reuse.
//    private abstract class AddFiber : Fiber.Closure<AddFiber, (int a, int b)> {
//      private Service<ServiceExampleServiceAdapter.AddDto> addService = null;
//
//      protected override void Activities(Fiber fiber) {
//        fiber.WaitFor((addService = ServiceExampleServiceAdapter.AddDto.Call(Scope)).OnComplete)
//             .Do(_ => OnResponse(Scope, addService.Dto.response, addService.ErrorMessage));
//      }
//
//      protected abstract void OnResponse((int a, int b) request, int response, string errorMessage = null);
//    }
//- As before, only the first line is functional. The second is only so the test can wait. This is the simplest call so far with all the scaffolding encapsulated in the closure above. In addition there is no garbage collector load since all is cached. This may not seem like much, but is depends on the user of the service on how heavily it is used.
    [UnityTest] public IEnumerator WithFiber() {
      var closure = ServiceExampleServiceAdapter.Add.Call((56, 67));
      return closure.Fiber.AsCoroutine();
    }
  }
}
#endif

//- All the work is implemented in two files. There is a concrete interfacing class for each service provider that takes the request information and makes the provider-specific calls. The service adapter is common to all concrete services and does what is needed with the response when it arrives.