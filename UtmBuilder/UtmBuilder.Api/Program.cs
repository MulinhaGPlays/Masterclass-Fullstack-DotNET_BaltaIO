using Microsoft.AspNetCore.Http.HttpResults;
using UtmBuilder;
using UtmBuilder.Exceptions;
using UtmBuilder.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerUI(SwaggerUIOptions =>
{
    SwaggerUIOptions.SwaggerEndpoint(url: "v1/swagger.json", name: "UTM Builder V1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost(pattern: "v1/utm", handler:(UtmModel model) =>
{
    try 
	{
		return Results.Ok(new { url = ((Utm)model).ToString() });
	}
	catch (InvalidUtmException ex)
	{
		return Results.BadRequest(error: new { ex.Message });
	}
	catch
	{
		return Results.BadRequest(error: new { Message = "Failed to generate UTM" });
	}
})
.WithDescription("Generate an UTM based on URL and Metadata")
.WithSummary("Generate UTM")
.Produces<Utm>()
.WithOpenApi();

app.Run();
