using eStat.BLL.Configuration;
using eStat.BLL.Implementations;
using eStat.BLL.Interfaces;
using eStat.BLL.Jobs;
using eStat.BLL.MLLogic;
using eStat.BLL.MLLogic.Interfaces;
using eStat.DAL.Core.Context;
using eStat.DAL.Entities;
using eStat.DAL.Implementations;
using eStat.Library.Models;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File($"Logs/{String.Format("{0}", DateTime.Now.ToString("dd_MM_yyyy"))}.log")
    .CreateLogger();

builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBLLServices(builder.Configuration);
builder.Services.AddDALServices(builder.Configuration);

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

void GenerateMLExcelData()
{
    using var scope = app.Services.CreateScope();
    IPredicitonDataGenerator initialDataGenerator = (PredictionDataGenerator)scope.ServiceProvider.GetService(typeof(IPredicitonDataGenerator))!;
    //initialDataGenerator.GenerateInitialDataset();
}

void SeedData()
{
    var x = BCrypt.Net.BCrypt.HashPassword("password");
    //using var scope = app.Services.CreateScope();
    ////IProducts productsBL = (ProductsBL)scope.ServiceProvider.GetService(typeof(IProducts))!;
    //IUsers usersBL = (UsersBL)scope.ServiceProvider.GetService(typeof(IUsers))!;
    //IShoppingCarts shoppingCartsBL = (ShoppingCartsBL)scope.ServiceProvider.GetService(typeof(IShoppingCarts))!;
    //IUserProducts userProductsBL = (UserProductsBL)scope.ServiceProvider.GetService(typeof(IUserProducts))!;
    //IPurchases purchasesBL = (PurchasesBL)scope.ServiceProvider.GetService(typeof(IPurchases))!;

    //List<User> normalUsers = usersBL.GetAll().Where(u => u.Role == eStat.Common.Enums.Roles.Purchaser).ToList();
    //List<ShoppingCart> shoppingCarts = shoppingCartsBL.GetAll();
    //List<UserProduct> products = userProductsBL.GetAll();

    //Random random = new Random();
    //foreach (User user in normalUsers)
    //{
    //    ShoppingCart shoppingCart = shoppingCarts.FirstOrDefault(sc => sc.UserGUID == user.UserGUID);
    //    if (shoppingCart == null)
    //        continue;
    //    int nbOfPurchases = random.Next(30, 90);
    //    for (int i = 0; i < nbOfPurchases; i++)
    //    {
    //        int nbOfProducts = random.Next(1, 20);
    //        for (int j = 0; j < nbOfProducts; j++)
    //        {
    //            int userProductIndex = random.Next(0, products.Count);
    //            UserProduct userProduct = products[userProductIndex];
    //            while (userProduct.Quantity < 1)
    //            {
    //                userProductIndex = random.Next(0, products.Count);
    //                userProduct = products[userProductIndex];
    //            }
    //            ShoppingCartProductAdd scp = new ShoppingCartProductAdd
    //            {
    //                Quantity = random.Next(1, (int)Math.Ceiling(userProduct.Quantity * 1.0 / 6)),
    //                ShoppingCartGUID = shoppingCart.ShoppingCartGUID,
    //                UserProductGUID = userProduct.UserProductGUID,
    //            };
    //            shoppingCartsBL.AddItemToCart(scp);
    //        }
    //        purchasesBL.AddPurchaseWithDate(shoppingCart.ShoppingCartGUID, GetRandomDate());
    //    }
    //}
}

DateTime GetRandomDate()
{
    DateTime startDate = new DateTime(2022, 11, 1, 10, 0, 0);
    DateTime endDate = new DateTime(2023, 7, 5, 17, 0, 0);
    var randomTest = new Random();

    TimeSpan timeSpan = endDate - startDate;
    TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
    DateTime newDate = startDate + newSpan;

    return newDate;
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationSystemHub>("/notification");
});
app.UseWebSockets();


app.MapControllers();

//await DBContextMigrationHelper.Migrate(app);
SeedData();
//GenerateMLExcelData();

app.Run();
