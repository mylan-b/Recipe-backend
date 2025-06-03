using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Recipe.Application;
using Recipe.Infrastructure;

namespace Recipe.API;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            opt.Filters.Add(new ProducesAttribute("application/json"));
            opt.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "This field is Required");
            opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        }).AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddHsts(opt =>
        {
            opt.MaxAge = TimeSpan.FromDays(365);
            opt.IncludeSubDomains = true;
            opt.Preload = true;
        });

        services.AddHttpsRedirection(opts =>
        {
            opts.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
            opts.HttpsPort = 443;
        });

        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Recipe API",
                Version = "v1",
                Description = "Recipe API",
            });
        });

        services.AddApplication();
        services.RegisterServices();
        services.AddInfrastructure(Configuration);
    }

    public void Configure(IApplicationBuilder builder, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        builder.UseCors(x => x
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true));
        
        builder.UseRouting();
        builder.UseEndpoints(endpoint => endpoint.MapControllers());
        builder.UseSwagger();
        builder.UseSwaggerUI(opt =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
            }
        });
    }
}