using Asp.Versioning;
using MerchStore.Models;
using MerchStore.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MerchStoreDatabaseSettings>(
	builder.Configuration.GetSection("StoreDatabase"));

	builder.Services.AddSingleton<MerchService>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
	{
		options.ReportApiVersions = true;
		options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
		options.AssumeDefaultVersionWhenUnspecified = true;

		options.ApiVersionReader = new HeaderApiVersionReader("X-API-version");
	}
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
