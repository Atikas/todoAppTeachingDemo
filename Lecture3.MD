# Lecture 3: Data Handling and Service Lifetimes in ASP.NET Core Web API

The third lecture of our ASP.NET Core Web API series dives into essential topics surrounding data handling, endpoint configuration, parameter binding, and the management of service lifetimes within the ASP.NET Core dependency injection system. We will also explore the differences between singleton services and static classes, enhancing your ability to design efficient, scalable, and maintainable web APIs.

## Topics Covered

- **JSON**: Understand how ASP.NET Core Web API handles JSON data, including serialization and deserialization processes. Learn best practices for working with JSON in your APIs, ensuring efficient data exchange between the server and clients.

- **Endpoints**: Explore how to define and configure endpoints in ASP.NET Core Web APIs. We'll cover the basics of setting up routes to handle various HTTP requests and how to organize your endpoints for clarity and maintainability.

- **Parameter Binding Attributes**:
  - `[FromRoute]`: Binding parameters from the route data.
  - `[FromQuery]`: Binding parameters from the query string.
  - `[FromBody]`: Binding parameters from the request body.
  - `[FromForm]`: Binding parameters from form data.
  - `[FromHeader]`: Binding parameters from HTTP headers.
  - `[FromService]`: Injecting services directly into action method parameters.
  Learn about these attributes and how they enable fine-grained control over the source of action method parameters.

- **Scoped, Transient, Singleton Explanation**: Delve into the service lifetimes available in ASP.NET Core's dependency injection container. Understand when to use Scoped, Transient, or Singleton lifetimes for your services to ensure the appropriate object instance management and application behavior.


## Learning Outcomes

By the end of this lecture, you will:

- Be proficient in handling JSON data within ASP.NET Core Web APIs.
- Know how to define and organize endpoints effectively.
- Understand the purpose and usage of various parameter binding attributes to control data input in your API.
- Be able to distinguish between Scoped, Transient, and Singleton service lifetimes and decide which to use for different scenarios in your application.
- Understand the differences between singleton services and static classes, and their implications on application design and performance.

## Prerequisites

Participants should have a foundational understanding of ASP.NET Core Web API development, including basic knowledge of C#, familiarity with RESTful principles, and experience with the Visual Studio IDE or the .NET Core CLI. This lecture builds on concepts introduced in previous sessions.

---

This description outlines the key topics and expected learning outcomes for the third lecture of your course, ensuring participants gain a deeper understanding of advanced ASP.NET Core Web API features. Feel free to adjust the content to better align with your teaching approach and the specific details of .NET 7/8 you're focusing on.
