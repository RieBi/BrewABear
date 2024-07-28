using Api.Services;
using Application;
using Application.Services;
using Data;
using Data.DataSeeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.SupportNonNullableReferenceTypes());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string could not be loaded from settings.");
}

builder.Services.AddDatabase(connectionString!);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IStarter>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfiles>());

builder.Services.AddSingleton<IGuidCreator, GuidCreator>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IExceptionHandlerService, ExceptionHandlerService>();
builder.Services.AddScoped<ISaleService, SaleService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var isTestMode = Array.Exists(AppDomain.CurrentDomain.GetAssemblies(),
    a => a.FullName?.StartsWith("xunit", StringComparison.InvariantCultureIgnoreCase) ?? false);

if (!isTestMode)
{
    using var scope = app.Services.CreateScope();

    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    seeder.ApplySeeding();
}

await app.RunAsync();

#pragma warning disable S1118 // Utility classes should not have public constructors
public partial class Program { }
#pragma warning restore S1118 // Utility classes should not have public constructors
