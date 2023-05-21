using Service;
using Service.DrivenAdapters.DatabaseAdapters.Configuration;
using Service.DrivingAdapters.Configuration;
using Service.DrivingAdapters.RestAdapters;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


ConfigurationManager configuration = builder.Configuration;
builder.Services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
AppSettings appSettings = new();
configuration.GetSection(nameof(AppSettings)).Bind(appSettings);



builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddUseCases();
builder.Services.AddThirdParties(appSettings);
builder.Services.AddAutoMapper(Assembly.Load(typeof(Program).Assembly.GetName().Name!));
builder.Services.AddEndpointsApiExplorer();


WebApplication app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseRouting();


ScoreRestAdapter.RegisterApis(app);

app.Run();