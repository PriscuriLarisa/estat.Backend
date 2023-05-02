using eStat.BLL.Configuration;
using eStat.BLL.Implementations;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context;
using eStat.Library.Helpers;
using eStat.Library.Models;

var builder = WebApplication.CreateBuilder(args);

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

void SeedData()
{
    using var scope = app.Services.CreateScope();
    IProducts productsBL = (ProductsBL)scope.ServiceProvider.GetService(typeof(IProducts))!;
    IUsers usersBL = (UsersBL)scope.ServiceProvider.GetService(typeof(IUsers))!;
    IUserProducts userProductsBL = (UserProductsBL)scope.ServiceProvider.GetService(typeof(IUserProducts))!;
    bool hasProductsData = productsBL.GetAll().Count != 0;
    bool hasUserData = usersBL.GetAll().Count != 0;
    bool hasUserProductsData = userProductsBL.GetAll().Count != 0;

    //if (!hasProductsData)
    //{
    //    List<Product> products = ExcelHelper.GetProductsFromDataset();

    //    foreach (Product product in products)
    //    {
    //        productsBL.Add(product);
    //    }
    //}

    if (!hasUserData)
    {
        List<string> firstNames = new List<string> { "James", "Robert", "Mary", "Patricia", "John", "Michael",
                                                    "Jennifer", "Linda", "David", "William", "Elizabeth", "Barbara",
                                                    "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Sarah",
                                                    "Charles", "Karen"};

        List<string> lastNames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia",
                                                    "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez",
                                                    "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore",
                                                    "Jackson", "Martin"};
        for (int i=0; i<20; i++)
        {
            UserCreate newUser = new UserCreate
            {
                UserGUID = Guid.NewGuid(),
                FirstName = firstNames[i],
                LastName = lastNames[i],
                Email = $"{firstNames[i]}.{lastNames[i]}@gmail.com",
                Role = eStat.Common.Enums.Roles.Owner,
                Password = $"{firstNames[i]}{lastNames[i]}",
                Birthday = DateHelper.GetRandomDate(),
                Membership = eStat.Common.Enums.Memberships.OwnerFirstTier
            };

            usersBL.Add(newUser);
        }
    }

    //if (!hasUserProductsData)
    //{
    //    List<User> users = usersBL.GetAll();
    //    List<Product> products = productsBL.GetAll();

    //    foreach (User user in users)
    //    {
    //        List<Guid> guids = new List<Guid>();
    //        Random randomNumber = new Random();
    //        int nbOfProducts = randomNumber.Next(20, 50);
    //        for (int i=0;i< nbOfProducts; i++)
    //        {
    //            Random newRandomNumber = new Random();
    //            int productIndex;
    //            do
    //            {
    //                productIndex = randomNumber.Next(0, products.Count - 1);
    //            } while (guids.Contains(products[productIndex].ProductGUID));
    //            guids.Add(products[productIndex].ProductGUID);
    //            Product product = products[productIndex];
    //            newRandomNumber = new Random();
    //            float percentage = randomNumber.Next(-10, +10);
    //            int quantity = randomNumber.Next(0, 20);

    //            UserProduct newUserProduct = new UserProduct
    //            {
    //                UserProductGUID = Guid.NewGuid(),
    //                ProductGUID = product.ProductGUID,
    //                UserGUID = user.UserGUID,
    //                Quantity = quantity,
    //                Price = (float)((float)product.BasePrice + Math.Truncate((float)product.BasePrice * percentage) / 100f)
    //            };
    //            userProductsBL.Add(newUserProduct);
    //        }
            
    //    }
    //}

    //List<User> users = usersBL.GetAll();
    //List<Product> products = productsBL.GetAll();

    //foreach (Product product in products)
    //{
    //    List<Guid> guids = new List<Guid>();
    //    Random randomNumber = new Random();
    //    int nbOfProducts = randomNumber.Next(0, 15);
    //    for (int i = 0; i < nbOfProducts; i++)
    //    {
    //        Random newRandomNumber = new Random();
    //        int userIndex;
    //        do
    //        {
    //            userIndex = randomNumber.Next(0, users.Count - 1);
    //        } while (guids.Contains(users[userIndex].UserGUID));
    //        guids.Add(users[userIndex].UserGUID);
    //        User user = users[userIndex];
    //        newRandomNumber = new Random();
    //        float percentage = randomNumber.Next(-10, +10);
    //        int quantity = randomNumber.Next(0, 20);

    //        UserProduct newUserProduct = new UserProduct
    //        {
    //            UserProductGUID = Guid.NewGuid(),
    //            ProductGUID = product.ProductGUID,
    //            UserGUID = user.UserGUID,
    //            Quantity = quantity,
    //            Price = (float)((float)product.BasePrice + Math.Truncate((float)product.BasePrice * percentage) / 100f)
    //        };
    //        userProductsBL.Add(newUserProduct);
    //    }

    //}
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRouting();

app.MapControllers();

//await DBContextMigrationHelper.Migrate(app);
SeedData();

app.Run();
