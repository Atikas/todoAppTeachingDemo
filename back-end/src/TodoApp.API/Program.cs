using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.Json.Serialization;
using TodoApp.API.Mappers;
using TodoApp.API.Mappers.Interfaces;
using TodoApp.API.Services;
using TodoApp.API.Services.Interfaces;
using TodoApp.BLL;
using TodoApp.DAL;



var builder = WebApplication.CreateBuilder(args); //This line creates a new instance of WebApplicationBuilder, which is used to configure and build the application. The CreateBuilder method is a factory method that creates a new instance of WebApplicationBuilder, which is used to configure and build the application. The args parameter is an array of command-line arguments passed to the application when it's started. This method is called to create a new instance of WebApplicationBuilder, which is used to configure and build the application. The args parameter is an array of command-line arguments passed to the application when it's started.

// Add services to the container.
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IAccountMapper, AccountMapper>();
builder.Services.AddTransient<ITodoItemMapper, TodoItemMapper>();
builder.Services.AddTransient<IImageMapper, ImageMapper>();
builder.Services.AddTransient<ITodoWeatherMapper, TodoWeatherMapper>();

builder.Services.ConfigureBusinessLayerServices();
builder.Services.ConfigureDataLayerServices(builder.Configuration);

builder.Services.AddHttpContextAccessor(); //Registers the IHttpContextAccessor service with the dependency injection (DI) container. This service provides access to the HttpContext outside of controllers, such as in services or middleware, where it's not directly available through method parameters. This is particularly useful when you need to obtain information about the authenticated user in parts of your application where the HttpContext is not directly available, such as in services, repositories or middleware.


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //This method call configures the application to use authentication middleware, specifically setting it up to use JWT bearer tokens as the default scheme. This tells ASP.NET Core that when a request comes in, it should expect the user's identity to be represented in a JWT format.
  .AddJwtBearer(options => //This extension method adds and configures JWT bearer token-based authentication to the project. Within the lambda (options => { ... }), you're specifying how incoming tokens are validated.
  {
      var secretKey = builder.Configuration.GetSection("Jwt:Key").Value!; //retrieves the secret key used to sign the tokens from the application's configuration, such as an appsettings.json file. This key is crucial for the security of the JWTs, as it's used to validate the signature of incoming tokens.
      var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey)); //creates a new instance of SymmetricSecurityKey using the secret key. This key is used to validate the signature of the token, ensuring it was issued by a trusted party and has not been tampered with.
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true, //ensures that the issuer of the token matches the expected issuer, a security measure to prevent tokens issued by an unauthorized server from being accepted.
          ValidateAudience = true, //ensures that the token's audience matches the expected audience value, verifying that the token is intended to be used by the application.
          ValidateLifetime = true, //checks that the token has not expired.
          ValidateIssuerSigningKey = true, //confirms that the token is signed with the expected key, helping to prevent forgery.
          ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value, //specifies the expected issuer of the token, usually a URL or an identifier for the authentication server.
          ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value, //specifies the intended recipient of the token, typically the identifier of the ASP.NET Core application.
          IssuerSigningKey = key //sets the key used to validate the token's signature.
      };
  });


builder.Services.AddControllers() //This method call adds services required for MVC controllers to the application's dependency injection container, enabling your application to respond to incoming web requests with controllers. Controllers are a fundamental part of the MVC (Model-View-Controller) pattern, used for handling user input and responses.
    .AddNewtonsoftJson(options => //This extension method configures the application to use Newtonsoft.Json for JSON serialization and deserialization in ASP.NET Core. By default, ASP.NET Core uses System.Text.Json, but this method allows for the use of Newtonsoft.Json, which provides more features and customization options.
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; // Inside the lambda, this setting configures how the Newtonsoft.Json serializer handles null values when serializing objects to JSON. By setting NullValueHandling to Ignore, the serializer will skip null values when serializing objects, which can help reduce the size of the JSON response and make it more readable.
    });
//Newtonsoft.Json offers a wide range of features, including extensive support for custom serialization and deserialization, converters, and settings for handling circular references, polymorphism, and more. But System.Text.Json is faster in serialization and deserialization operations due to its implementation and use of spans. Newtonsoft.Json might be slower, especially in high-throughput scenarios.
//If you want to use System.Text.Json instead of Newtonsoft.Json, you can remove the AddNewtonsoftJson method and use the default System.Text.Json serializer. You can also configure System.Text.Json options using the AddJsonOptions method, which is commented out in the original code.
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // This is essential for tools like Swagger/OpenAPI to automatically generate documentation, discover API endpoints, and understand their routes, parameters, and responses.

builder.Services.AddSwaggerGen(opt =>
{ //Adds and configures Swagger generator services. SwaggerGen is a Swagger tool that generates Swagger documents for your API based on your controllers, models, and descriptions.
    opt.SwaggerDoc("v1", new OpenApiInfo //This line creates a Swagger document named "v1" with specified API information
    {
        Version = "0.9.0", //The version of the API. This is useful when you have multiple versions of your API and want to generate separate Swagger documents for each.
                           //Use Semantic Versioning: Follow Semantic Versioning (https://semver.org/) principles (MAJOR.MINOR.PATCH) to manage your API versions. This makes it clear to users how changes in versions might affect their use of your API.
        Title = "ToDo API Teaching Demo", // Sets a human-readable title for the API documentation. This title is displayed in the Swagger UI as the name of the API.
                                          //The title should clearly reflect the purpose of your API but remain succinct. It should be immediately clear to the developer what the API does or relates to.
        Description = "An ASP.NET Core Web API for managing ToDo items.  " +
                       "API allows for the detailed tracking and categorization of tasks, ranging from simple to-dos to comprehensive projects. " +
                       "Each TodoItem encompasses not only basic identifiers and titles but extends functionality to include types (categorization), " +
                       "detailed descriptions, locations, and associated imagery for a richer task representation." +
                       "Key features include the ability to set due dates for tasks, mark completion, " +
                       "and associate tasks with specific user accounts for personalized task management.",
        //Provides a longer description of what the API does. In this case, it indicates that the API is for managing ToDo items, which helps developers understand the API's purpose at a glance.
        //While the title is concise, the description allows for more detailed information about the API's purpose, its target audience, and key features or use cases.
    });
    var securitySchema = new OpenApiSecurityScheme //This object defines a security scheme that describes how the API is secured. In this case, it's configured for JWT authentication using the Bearer token scheme.
    {
        Description = "JWT Authorization header is using Bearer scheme. \r\n\r\n" +
                   "Enter token. \r\n\r\n" +
                   "Example: \"d5f41g85d1f52a\"", // Provides information on how to use the security scheme, explaining that the JWT token should be provided in the Authorization header using the Bearer scheme, and gives an example token format.
        Name = "Authorization", //The name of the header where the token should be supplied, which is "Authorization".
        In = ParameterLocation.Header, // Specifies where the security scheme applies, in this case, in the Header of the request.
        Type = SecuritySchemeType.Http, //The type of the security scheme, set to SecuritySchemeType.Http, indicating HTTP Authentication.
        Scheme = "bearer", // Specifies the scheme used, in this case, "bearer", indicating Bearer Token authentication.
        BearerFormat = "JWT", //Indicates the format of the token, here specified as "JWT" to denote that the bearer token is a JWT.
        Reference = new OpenApiReference // Provides a reference object that allows the security scheme to be referenced within the Swagger document. It's identified by "Bearer".
        {
            Type = ReferenceType.SecurityScheme, //Specifies the type of the reference, which is a SecurityScheme.
            Id = "Bearer", //Don't forget to set the same name "Bearer" as in AddSecurityDefinition
        }
    };
    opt.AddSecurityDefinition("Bearer", securitySchema); //This method call adds the defined security scheme to the Swagger document, making it known that the API uses Bearer Token authentication.
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } }); //This adds a security requirement to the API documentation, specifying that the defined security scheme must be used. It effectively requires that the JWT token be provided for accessing the API, making it clear in the documentation that authentication is required.

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; //is called to include XML comments in the Swagger UI. This makes the API documentation richer and more informative. The XML comments are generated by the compiler and contain descriptions of the API's endpoints, parameters, responses, etc.
                                                                           //Don't forget to set xml tag <GenerateDocumentationFile> to true in .csproj file, or check the checkbox in project properties
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); //  This line creates the full path to the XML file. It combines the base directory of the app's context (where the application is running) with the filename of the XML documentation. This is where the XML documentation file is expected to be located at runtime.
    opt.IncludeXmlComments(xmlPath); //This instructs Swagger to include the XML comments found at xmlPath in the generated Swagger documentation. These XML comments come from the triple-slash comments you write above your action methods and parameters in your controllers, which are compiled into the XML file by enabling XML documentation file generation in your project settings.
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //This conditional statement checks if the application is running in the development environment. The IsDevelopment method is part of the IWebHostEnvironment interface, which provides information about the web hosting environment the application is running in. This check ensures that Swagger is only enabled when the application is being developed, as exposing Swagger in production environments can lead to security risks by providing detailed API documentation publicly.
{
    app.UseSwagger(); //This line enables the generation of Swagger documents. Swagger documents are JSON or YAML files that describe the API's endpoints, parameters, responses, etc. This is necessary for enabling the interactive API documentation provided by Swagger UI.
    app.UseSwaggerUI(); //This line enables Swagger UI, a web-based UI that reads the Swagger documentation to provide an interactive documentation experience. Developers can use Swagger UI to test API endpoints directly from their browsers, making it easier to understand and use the API's functionalities.
}

app.UseHttpsRedirection(); //This middleware automatically redirects HTTP requests to HTTPS, ensuring that all communications between the client and server are encrypted and secure.

app.UseCors(builder => //This method is called to add the CORS middleware to the application's request processing pipeline. 
{
    builder
    .AllowAnyOrigin() //This method call configures the CORS policy to allow requests from any origin. In a production environment, it's generally recommended to be more specific about which origins are allowed to ensure the security of your web application.
    .AllowAnyMethod() //This allows the CORS policy to accept requests made with any HTTP method (such as GET, POST, PUT, DELETE, etc.). This is useful for a RESTful API that needs to support a wide range of actions on resources.
    .AllowAnyHeader(); //his configures the CORS policy to allow any headers in the requests. Headers are often used in requests to carry information about the content type, authentication, etc. Allowing any header supports a wide range of requests that might include custom or standard headers.
}); //This configuration is very permissive, allowing any web application to make requests to your ASP.NET Core Web API regardless of the origin, HTTP method, or headers used in the request. While this setup is useful for development or when you need to allow wide access to your API, it's important to tighten the CORS policy for production environments to minimize security risks. You would typically do this by specifying allowed origins, methods, and headers that match the requirements of your specific client applications.

app.UseAuthentication(); //This enables the authentication middleware, which processes authentication tokens in incoming requests. It's necessary for identifying the user making a request based on their credentials.
app.UseAuthorization(); //After authentication, this enables the authorization middleware, which determines whether an authenticated user has permission to access specific resources or execute certain operations in your application.
                        //The order of UseAuthentication and UseAuthorization middleware in an ASP.NET Core application is critical. UseAuthentication must come before UseAuthorization in the middleware pipeline. This order ensures that the system first authenticates a user, determining their identity, before attempting to authorize them, which involves determining if the authenticated user has permission to access the requested resources. If the order is reversed, authorization checks would occur without any authenticated user context, leading to failures in properly securing resources.

app.MapControllers(); //This line sets up routing to map incoming requests to the appropriate controller actions. It's essential for directing requests to the correct endpoints based on the route and HTTP method.

app.Run(); //This starts the application's request processing pipeline. It listens for incoming HTTP requests and processes them through the configured middleware (including authentication, authorization, and routing) before responding.


