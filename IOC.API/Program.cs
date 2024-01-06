using IOC.API.ExceptionHandler;
using IOC.API.ModelMappings;
using IOC.Application;
using IOC.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
}).ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
        new BadRequestObjectResult(context.ModelState)
        {
            ContentTypes =
            {
                    // using static System.Net.Mime.MediaTypeNames;
                    Application.Json,
                    Application.Xml
            }
        };
})
    .AddXmlSerializerFormatters();
//builder.Services.AddProblemDetails();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IIOCRepository, IOCRepository>();
builder.Services.AddScoped<IIOCServices, IOCService>();
//builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddAutoMapper(typeof(investmentProfile));
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
  c.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" }));
var app = builder.Build();
app.UseStatusCodePages();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
    app.UseDeveloperExceptionPage(); // show error in detailed view
}
else
{
    app.UseExceptionHandler("/error");
}
//app cors
app.UseRouting();
app.UseCors("corsapp");
//app.UseCors(prodCorsPolicy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();