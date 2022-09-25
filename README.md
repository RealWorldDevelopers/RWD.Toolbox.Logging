
[Real World Developers](https://www.realworlddevelopers.com)
-----------------------------

[Documentation](https://realworlddevelopers.github.io/RWD.Toolbox.SMTP/)
-----------------------

[Nuget Package](https://www.nuget.org/packages/RWD.Toolbox.SMTP/)
---------------

# RWD.Toolbox.Logging.Infrastructure
A library with Helpers to Assist in basic logging task.

---

> ## RWD.Toolbox.Logging.Demo.Static  
> A sample project to demonstrating use of logging as a Static Class.  

> ## RWD.Toolbox.Logging.Demo.Console
> A sample project to demonstrating use of logging in a Console App.

> ## RWD.Toolbox.Logging.Demo.ClassLibrary
> A sample project to demonstrating use of logging in a Class Library.

> ## RWD.Toolbox.Logging.Demo.Communication
> A sample project to demonstrating use of logging in a Communication Agent.

> ## RWD.Toolbox.Logging.Demo.MVC
> A sample project to demonstrating use of logging server side in a MVC Application.  
> A sample project to demonstrating use of logging client side in a MVC Application. 

> ## RWD.Toolbox.Logging.Demo.WebAPI
> A sample project to demonstrating use of logging in a REST Web API.
 

### Release Notes
- v1.0.0.0
	- Initial Release
	- NET 6.0

  
___

>### Log Event Levels (enum value)

>>**None (6)**  
Specifies that a logging category should not write any messages. `[Not used for writing log messages.`]

>>**Critical (5)**  
Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention.

>>**Error (4)**  
Logs that highlight when the current flow of execution is stopped due to a failure. `[These indicate a failure in the current activity.`]

>>**Warning (3)**  
Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application execution to stop.

>>**Information (2)**	- *DEFAULT*  
Logs that track the general flow of the application. `[These logs should have long-term value.`]

>>**Debug (1)**  
Logs that are used for interactive investigation during development. `[These logs should primarily contain information useful for debugging.`]

>>**Trace (0)**  
Logs that contain the most detailed messages. `[These messages should never be enabled in a production as the may contain sensitive data.`] 
  

