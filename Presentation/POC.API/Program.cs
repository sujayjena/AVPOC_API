using Microsoft.OpenApi.Models;
using POC.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using POC.Application.Models;
using POC.Application.Helpers;
using POC.Persistence;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//JWT configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

//To validate parameters (Model State)
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = (actionContext) =>
    {
        ResponseModel response = ModelStateHelper.GetValidationErrorsList(actionContext);
        return new BadRequestObjectResult(response);
    };
});

builder.Services.ConfigurePersistence(builder.Configuration);

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "POC API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

//Web Application related configurations
{
    //app.UseMiddleware<ExceptionMiddleware>();
    //app.UseMiddleware<JwtMiddleware>();

    //Global CORS policy - To disable CORS error
    app.UseCors(cors => cors
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    //app.UseHttpsRedirection();
    app.UseRouting();

    #region Swagger Configurations
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "POC API");
        //s.RoutePrefix = string.Empty;
    });
    //}
    #endregion

    //app.UseStaticFiles();
    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")),
        RequestPath = new PathString("/Uploads")
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}
app.Run();
