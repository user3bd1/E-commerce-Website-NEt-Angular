using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using StackExchange.Redis;
using Infrastructure.CartService;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<StoreContext>(opt=> opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddCors();
builder.Services.AddSingleton<IConnectionMultiplexer>(config => {
    var connectionString = builder.Configuration.GetConnectionString("Redis")?? throw new Exception("cannt get redis conn");
    var configuration = ConfigurationOptions.Parse(connectionString,true);
    return ConnectionMultiplexer.Connect(configuration);
     
});

builder.Services.AddSingleton<ICartService, CartService>();


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.UseCors(x=> x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200","https://localhost:4200"));


app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();


try
{
 using var scope = app.Services.CreateScope();
 var services = scope.ServiceProvider;
 var context = services.GetRequiredService<StoreContext>();
 await context.Database.MigrateAsync();
 await StoreContextSeed.SeedAsync(context);
}
catch(Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();

