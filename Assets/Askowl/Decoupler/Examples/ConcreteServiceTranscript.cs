//- Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
//- We have created a new decoupled service called Adze and now want to add our first interface to an advertising provider. We will use Chartboost.
//- Askowl decoupler provides a wizard to make life a little easier. [[Assets // Create // Decoupled // Adze // Add Concrete Service]]
//- This wizard is much simpler, with only the name of the service to be provided. For reasons that will become obvious, use the same name as the service.
//- The only additional file created is AdzeServiceForChartboost.cs. When we open it we see that it is mostly disabled. To go any further we will need to install the Chartboost Unity package.
//- Before we can go any further we need to install the Chartboost package. Since this is a mobile advertising package we need to set the platform to iOS or Android [[File // Build Settings]]
//- Download the Chartboost Unity package [[https://answers.chartboost.com/en-us/articles/download]]
//- And import it into Unity [[Assets // Import Package // Custom Package...]]
//- AdzeServiceForChartboost.cs looks much healthier. This is because Chartboost creates a folder under Assets. Other services may not be so obliging and you will have to change the code in DetectService.
