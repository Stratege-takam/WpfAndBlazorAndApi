using Brewery.API;
using Elia.Core.Enums;
using Elia.Share.ASP.Middlewares;
using Newtonsoft.Json.Serialization;

using Environments = Elia.Core.Utils.Environments;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var env = Environments.Current.ToString();
var appsettings = $"appsettings.{env}.json";
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile(appsettings, true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

builder.Configuration.AddConfiguration(config);
builder.Services.AddProjectScoped(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            
if (Environments.IsEnvironment(EnvironmentEnum.Debug) )
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseMiddleware<NotFoundMiddleware>();
app.UseAuthorization();


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Brewery user webservice /api v1"));

//app.UseHttpsRedirection();



app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
SeeManager.Run(app, builder.Configuration);

app.Run();
