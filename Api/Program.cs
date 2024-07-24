using Application;
using Data;
using Data.DataSeeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    seeder.ApplySeeding();
}

await app.RunAsync();
