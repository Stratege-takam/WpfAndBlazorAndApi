using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Brewery.API.Helpers;
using Brewery.DAL;
using Elia.Core.Containers;
using Elia.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;

namespace Brewery.API;


/// <summary>
/// 
/// </summary>
public static class ProjectDiContainer
{
    #region Extensions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddProjectScoped(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors();
        services.AutoInject(SolutionAssembly.GetAllAssemblies);
        services.AddInfraScoped(configuration);

        services.AddEndpointsApiExplorer();
        
        // swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Brewery webservice", Version = "v1"});

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
            
        services.Configure<FormOptions>(o => {
            o.ValueLengthLimit = int.MaxValue;
            o.MultipartBodyLengthLimit = int.MaxValue;
            o.MemoryBufferThreshold = int.MaxValue;
        });
        
        // Inject jwt param
        var jwt = configuration.GetSection("Jwt");
        services.Configure<AppSettings.Jwt>(jwt);
        
        // add jwt auth
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // Validate the server (ValidateIssuer = true) that generates the token
                    ValidateAudience =
                        true, // Validate the recipient of the token is authorized to receive (ValidateAudience = true)
                    ValidateLifetime =
                        true, //Check if the token is not expired and the signing key of the issuer is valid (ValidateLifetime = true)
                    ValidateIssuerSigningKey = true, //Validate signature of the token
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
        
        return services;
    }
    

    #endregion
}