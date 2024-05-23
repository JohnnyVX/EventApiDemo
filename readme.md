# Event API Demo Overview

This project is a .NET Core Web API application that provides endpoints for event management.

# Setup
The app should be all set to run as is. Main branch contains vulnerabilities. Other branches contain solutions, some with commits to show various steps.


## Running the Application
After setting up, you can build and run the application:
dotnet build
dotnet run

The application will be available at http://localhost:5175.

# Endpoints
The application provides the following endpoints:

GET http://localhost:5175/api/Event/IsOnEventAccessList/{mpnId}: Checks if an event is on the access list. Replace {mpnId} with the ID of the event.

For example, to check if event `1234` is on the access list, use the following URL:
http://localhost:5175/api/Event/IsOnEventAccessList/1234

Note: If you use an invalid ID such as `error`, the application will throw an exception.

# Alternative Scripts Folder Start from Scratch Setup

If one wanted to create their own demo from scratch, some scripts that were used as a basis for creating the repo are in `SetupScripts` directory. These scripts will create the necessary directories and files, initialize a Git repository, and set up the project for first use.

# Handling Exceptions in the GetIsOnEventAccessList Method

During the development of the `GetIsOnEventAccessList` method, we identified a potential security risk: the method was returning stack traces in the response when an exception occurred. To mitigate this risk, we used GitHub Copilot to guide us through the process of improving the method.

## Potenial Questions with GitHub Copilot

**JohnnyVReply & TaThompson: I want to give a demo and how I can use Github copilot to chat to fix. security errors that a stacktrace is coming back from the is an event access list endpoint. What prompts should I ask you?**

**GitHub Copilot Chat**: It gave the questions below

**JohnnyVReply & TaThompson: What is the purpose of the GetIsOnEventAccessList method?**

**GitHub Copilot Chat**: The `GetIsOnEventAccessList` method is an API endpoint that checks if an event is on the access list. It takes an event ID (`mpnId`) as a parameter.

**JohnnyVReply & TaThompson: What are the potential security risks in the GetIsOnEventAccessList method?**

**GitHub Copilot Chat**: The method was initially designed to return a stack trace in the response when an exception occurred. This could potentially expose sensitive information about the application's internal workings, which is a security risk.

**JohnnyVReply & TaThompson: How can I prevent stack traces from being returned in the response of the GetIsOnEventAccessList method?**

**GitHub Copilot Chat**: You can handle exceptions within the method and return a more generic error message to the client, without exposing the stack trace.

**JohnnyVReply & TaThompson: How can I handle exceptions in the GetIsOnEventAccessList method to avoid exposing stack traces?**

**GitHub Copilot Chat**: You can use a try-catch block to catch exceptions that occur in the method. In the catch block, you can log the exception and return a generic error message.

**JohnnyVReply & TaThompson: Can you generate a code snippet to demonstrate how to implement these security improvements?**

**GitHub Copilot Chat**: Authors note: it's not quite that simple, so the answer is below. It has context

## Actual Conversation with GitHub Copilot

**JohnnyVReply & TaThompson: A QA informed me that a stack trace is coming back from the is on event access list endpoint. I'm not allowed to send stack traces back for API calls. Can you fix that?**

**GitHub Copilot Chat**: Yes, you can handle exceptions within the method and return a more generic error message to the client, without exposing the stack trace.

**JohnnyVReply & TaThompson: The name '_logger' does not exist in the current context.**

**GitHub Copilot Chat**: `_logger` is an instance of `ILogger`, which is used for logging in .NET. You need to inject it into your controller through the constructor.

**JohnnyVReply & TaThompson: Apparently I can't return 500 errors.**

**GitHub Copilot Chat**: If returning a 500 error is considered a security vulnerability in your context, you can return a 200 OK status with a custom error object.

**JohnnyVReply & TaThompson: My QA didn't like that, are there other options?**

**GitHub Copilot Chat**: Yes, there are other options. One common approach is to return a 200 OK status with a custom response object that includes both a success flag and a message.

**JohnnyVReply & TaThompson: Is there a different option? They don't like the 200.**

**GitHub Copilot Chat**: If returning a 200 status code is not acceptable in the case of an error, you can return a 400 (Bad Request) or 503 (Service Unavailable) status code.

**JohnnyVReply & TaThompson: They like that, but my lead developer is always encouraging us to improve our logs. is there a way to improve the log message so I know more info on what went wrong?**

**GitHub Copilot Chat**: Yes, you can include additional information in the log message to help diagnose the issue. One common approach is to include the input parameters of the method in the log message.

## ✅ QA 'Accepted' Solution
This conversation with GitHub Copilot helped us improve the security of the `GetIsOnEventAccessList` method by preventing stack traces from being returned in the response.
```csharp
[HttpGet("IsOnEventAccessList/{mpnId}")]
public async Task<IActionResult> GetIsOnEventAccessList(string mpnId)
{
    try
    {
        var result = await _eventService.IsOnEventAccessList(mpnId);
        return Ok(result);
    }
    catch (Exception ex)
    {
        // Log the exception details internally along with the mpnId
        _logger.LogError(ex, $"Error occurred while checking event access list for mpnId: {mpnId}");

        // Return a generic error message with a 400 status
        return BadRequest(new { success = false, message = "An error occurred while processing your request. Please try again later." });
    }
}
```

## Original Code

```csharp
[HttpGet("IsOnEventAccessList/{mpnId}")]
public async Task<IActionResult> GetIsOnEventAccessList(string mpnId)
{
    var result = await _eventService.IsOnEventAccessList(mpnId);
    return Ok(result);
}
```

## To Infinity...And Beyond
Ask questions? `Why?` `How?` `@workspace /explain` Take this chance to learn, to grow and to make yourself better

## Stacktraces in General
**JohnnyVReply & TaThompson:**
Why is the qa complaining (I jest😂) about that for security? Here is the stacktrace:

System.Exception: MD5 hash computation failed with ArgumentOutOfRangeException ---> System.ArgumentException: Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection. at System.Security.Cryptography.HashAlgorithm.ComputeHash(Byte[] buffer, Int32 offset, Int32 count) at Service.Events.EventService.IsOnEventAccessList(String mpnId) in c:\Users\john\code\EventApiDemo\Services\EventService.cs:line 35 --- End of inner exception stack trace --- at Service.Events.EventService.IsOnEventAccessList(String mpnId) in c:\Users\john\code\EventApiDemo\Services\EventService.cs:line 42 at Api.Controllers.EventController.GetIsOnEventAccessList(String mpnId) in c:\Users\john\code\EventApiDemo\Controllers\EventController.cs:line 40 at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.TaskOfIActionResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync() --- End of stack trace from previous location --- at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope) at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context) at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext) at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider) at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)

**GitHub Copilot**: 

The stack trace reveals several pieces of sensitive information:

1. File paths and usernames: The stack trace includes file paths like c:\Users\john\code\EventApiDemo\Services\EventService.cs:line 35. This reveals the username john and the structure of your file system.
1. Code structure: The stack trace shows the flow of method calls leading up to the exception. This can give an attacker insights into how your code is structured and where potential vulnerabilities might exist.
2. Specific error details: The stack trace reveals that an ArgumentOutOfRangeException occurred when trying to compute an MD5 hash in the IsOnEventAccessList method. This could give an attacker clues about what kind of data you're working with and how you're processing it.
2. Middleware details: The stack trace also reveals that you're using Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware and Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware. This information could be used by an attacker to exploit known vulnerabilities in these libraries.

## Bad Hashing
**JohnnyVReply & TaThompson:** Is there anything else concerning about that stacktrace?

**GitHub Copilot**: 

The stack trace you provided earlier indicates that an ArgumentOutOfRangeException occurred when trying to compute an MD5 hash in the IsOnEventAccessList method. This suggests that there might be a problem with the way you're computing the hash.

However, there's another security concern related to MD5: it's considered to be a weak hash function for many cryptographic purposes. MD5 is vulnerable to hash collisions, where different inputs produce the same hash output. This makes it unsuitable for functions like SSL certificates or encryption that require a high level of security.

If you're using MD5 hashes for sensitive information, you should consider switching to a more secure hash function like SHA-256. If you're using them to hash passwords, you should switch to a function designed for password hashing, like bcrypt, scrypt, or Argon2.

Without seeing the code for the IsOnEventAccessList method, I can't provide a specific fix for the ArgumentOutOfRangeException. However, the error message suggests that you're trying to compute the hash of a subset of a byte array, and the subset is not within the bounds of the array. You should check the values you're passing to the ComputeHash method to make sure they're valid.
The stack trace you provided earlier indicates that an ArgumentOutOfRangeException occurred when trying to compute an MD5 hash in the IsOnEventAccessList method. This suggests that there might be a problem with the way you're computing the hash.

However, there's another security concern related to MD5: it's considered to be a weak hash function for many cryptographic purposes. MD5 is vulnerable to hash collisions, where different inputs produce the same hash output. This makes it unsuitable for functions like SSL certificates or encryption that require a high level of security.

If you're using MD5 hashes for sensitive information, you should consider switching to a more secure hash function like SHA-256. If you're using them to hash passwords, you should switch to a function designed for password hashing, like bcrypt, scrypt, or Argon2.

Without seeing the code for the IsOnEventAccessList method, I can't provide a specific fix for the ArgumentOutOfRangeException. However, the error message suggests that you're trying to compute the hash of a subset of a byte array, and the subset is not within the bounds of the array. You should check the values you're passing to the ComputeHash method to make sure they're valid.

## SQL Error
**JohnnyVReply & TaThompson:**

What about this stacktrace? 

System.Exception: SQL Error with Query: SELECT * FROM Users WHERE Username = '{mpnId}' at Service.Events.EventService.IsOnEventAccessList(String mpnId) in c:\Users\john\code\EventApiDemo\Services\EventService.cs:line 51 at Api.Controllers.EventController.GetIsOnEventAccessList(String mpnId) in c:\Users\john\code\EventApiDemo\Controllers\EventController.cs:line 40 at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.TaskOfIActionResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync() --- End of stack trace from previous location --- at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope) at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context) at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext) at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider) at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)

**GitHub Copilot:**

The stack trace indicates that there's an SQL error occurring when executing the query SELECT * FROM Users WHERE Username = '{mpnId}' in the IsOnEventAccessList method.

The most concerning part of this stack trace is the SQL query itself. It appears that the mpnId is being directly inserted into the SQL query. This is a serious security vulnerability known as SQL Injection, where an attacker could potentially manipulate the mpnId input to execute arbitrary SQL commands on your database.

To fix this, you should use parameterized queries or an ORM (Object-Relational Mapping) that automatically handles this for you. Here's an example of how you can modify the query using parameterized queries:

```csharp
string query = "SELECT * FROM Users WHERE Username = @mpnId";
SqlCommand command = new SqlCommand(query, yourSqlConnection);
command.Parameters.Add(new SqlParameter("@mpnId", mpnId));
```

**JohnnyVReply & TaThompson:**

We are not allowed to return 500 errors. Why is that bad for security? This is a backend api and we've now made sure to give a generic error message and no stack trace

**GitHub Copilot:**

Returning a 500 error from a backend API, even with a generic error message and no stack trace, can still be problematic from a security perspective:

Information Leakage: While you've ensured that no stack trace or specific error details are returned, a 500 error still signals to a potential attacker that an unhandled exception occurred. This could provide them with clues about potential vulnerabilities in your application.

Denial of Service: If an attacker discovers a particular request that consistently causes a 500 error, they might use this to perform a Denial of Service (DoS) attack, repeatedly triggering the unhandled exception to consume server resources and potentially disrupt service for legitimate users.

Error Handling: The presence of 500 errors suggests that your application may not be correctly handling all possible error conditions. This could indicate broader issues with the robustness of your error handling and exception management practices, which could have security implications.
