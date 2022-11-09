using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Persistence.Contexts;

namespace RestaurantManagement.Persistence.SeedDatas
{
    public class SeedData
    {
        private static List<Category> GetCategories()
        {
            List<Category> result = new List<Category>() {
                new Category{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme Kategorisi 1",Description="Deneme Kategorisi 1",Active=true},
                new Category{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme Kategorisi 2",Description="Deneme Kategorisi 2",Active=true},
                new Category{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme Kategorisi 3",Description="Deneme Kategorisi 3",Active=true},
                new Category{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme Kategorisi 4",Description="Deneme Kategorisi 4",Active=true},
                new Category{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme Kategorisi 5",Description="Deneme Kategorisi 5",Active=true},
            };
            return result;
        }

        private static List<Supplier> GetSuppliers()
        {
            var result = new Faker<Supplier>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Active, i => i.PickRandom(true, false))
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .RuleFor(i => i.FullName, i => i.Person.FullName)
                .RuleFor(i => i.PhoneNumber, i => i.Phone.ToString())
                .RuleFor(i => i.Description, i => i.Lorem.Sentences(2))
                .Generate(50);

            return result;
        }

        private static List<Ingredient> GetIngredients()
        {
            var result = new Faker<Ingredient>("tr")
                .RuleFor(i => i.Name, i => i.Name.FirstName())
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Active, i => i.PickRandom(true, false))
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .RuleFor(i => i.Finished, i => i.PickRandom(true, false))
                .Generate(20);

            return result;
        }

        private static List<Wholesale> GetWholesales(List<Guid> supplierId, List<Guid> ing)
        {
            var result = new Faker<Wholesale>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Active, i => true)
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .RuleFor(i => i.Description, i => i.Lorem.Sentences(2))
                .RuleFor(i => i.SupplierId, i => i.PickRandom(supplierId))
                .RuleFor(i => i.IngredientId, i => i.PickRandom(ing))
                .RuleFor(i => i.Paid, i => i.PickRandom(true, false))
                .RuleFor(i => i.Price, i => i.Random.Decimal())
                .Generate(50);

            return result;
        }

        private static List<Product> GetProducts(List<Guid> categoryIds)
        {
            var result = new Faker<Product>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Active, i => i.PickRandom(true, false))
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .RuleFor(i => i.Description, i => i.Lorem.Sentences(2))
                .RuleFor(i => i.Name, i => i.Person.FirstName)
                .RuleFor(i => i.Price, i => i.Random.Decimal())
                .RuleFor(i => i.ImgUrl, i => i.Person.Avatar)
                .RuleFor(i => i.CategoryId, i => i.PickRandom(categoryIds))
                .Generate(50);

            return result;
        }

        private static List<Recipe> GetRecipes(List<Guid> productIds, List<Guid> ing)
        {
            var result = new Faker<Recipe>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Active, i => i.PickRandom(true, false))
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .RuleFor(i => i.Description, i => i.Lorem.Sentences(2))
                .RuleFor(i => i.ProductId, i => i.PickRandom(productIds))
                .RuleFor(i => i.IngredientId, i => i.PickRandom(ing))
                .Generate(50);

            return result;
        }

        private static List<OrderType> GetOrderTypes()
        {
            List<OrderType> result = new List<OrderType>() {
                new OrderType{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme Sİpariş türü 1"},
                new OrderType{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme Sİpariş türü 2"},
                new OrderType{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme Sİpariş türü 3"},
                new OrderType{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme Sİpariş türü 4"},
                new OrderType{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme Sİpariş türü 5"},
            };
            return result;
        }

        private static List<Process> GetProcess()
        {
            List<Process> result = new List<Process>() {
                new Process{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme aşama türü 1", IconUrl="deneme"},
                new Process{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme aşama türü 2", IconUrl="deneme"},
                new Process{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme aşama türü 3", IconUrl="deneme"},
                new Process{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme aşama türü 4", IconUrl = "deneme"},
                new Process{Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Deneme aşama türü 5", IconUrl = "deneme"},
            };
            return result;
        }

        private static List<Role> GetRoles()
        {
            List<Role> result = new List<Role>() {
                new Role() { Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Admin" },
                new Role() { Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Employee" },
                //new Role() { Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Customer" },
            };
            return result;
        }

        private static List<Employee> GetEmployees(List<Guid> rolesId)
        {
            var result = new Faker<Employee>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Active, i => i.PickRandom(true, false))
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .RuleFor(i => i.RoleId, i => i.PickRandom(rolesId))
                .RuleFor(i => i.Fullname, i => i.Person.FullName)
                .RuleFor(i => i.PhoneNumber, i => i.Phone.ToString())
                .Generate(10);
            return result;
        }

        private static List<Order> GetOrders(List<Guid> empId, List<Guid> typeId, List<Guid> processId)
        {
            var result = new Faker<Order>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Active, i => i.PickRandom(true, false))
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .RuleFor(i => i.ProcessId, i => i.PickRandom(processId))
                .RuleFor(i => i.EmployeeId, i => i.PickRandom(empId))
                .RuleFor(i => i.OrderTypeId, i => i.PickRandom(typeId))
                .Generate(100);
            return result;
        }
        private static List<OrderDetail> GetOrderDetails(List<Guid> orderId, List<Guid> productId)
        {
            var result = new Faker<OrderDetail>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Active, i => i.PickRandom(true, false))
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .RuleFor(i => i.ProductId, i => i.PickRandom(productId))
                .RuleFor(i => i.OrderId, i => i.PickRandom(orderId))
                .RuleFor(i => i.Description, i => i.Lorem.Sentences(1))
                .Generate(10);
            return result;
        }

        public async Task SeedAsync(IConfiguration configuration)
        {
            var contextBuilder = new DbContextOptionsBuilder();
            contextBuilder.UseSqlServer(configuration["ConnectionString"]);

            var context = new ManagementContext(contextBuilder.Options);

            var roles = GetRoles();
            var rolesIds = roles.Select(i => i.Id).ToList();
            await context.Roles.AddRangeAsync(roles);

            var categories = GetCategories();
            var categoryIds = categories.Select(i => i.Id).ToList();
            await context.Categories.AddRangeAsync(categories);

            var Processes = GetProcess();
            var processIds = Processes.Select(i => i.Id).ToList();
            await context.Processes.AddRangeAsync(Processes);

            var OrderTypes = GetOrderTypes();
            var OrderTypesIds = OrderTypes.Select(i => i.Id).ToList();
            await context.OrderTypes.AddRangeAsync(OrderTypes);

            var Suppliers = GetSuppliers();
            var SuppliersIds = Suppliers.Select(i => i.Id).ToList();
            await context.Suppliers.AddRangeAsync(Suppliers);

            var Ingredients = GetIngredients();
            var IngredientsIds = Ingredients.Select(i => i.Id).ToList();
            await context.Ingredients.AddRangeAsync(Ingredients);

            var Wholesales = GetWholesales(SuppliersIds, IngredientsIds);
            var WholesalesIds = Wholesales.Select(i => i.Id).ToList();
            await context.Wholesales.AddRangeAsync(Wholesales);

            var Products = GetProducts(categoryIds);
            var ProductsIds = Products.Select(i => i.Id).ToList();
            await context.Products.AddRangeAsync(Products);

            var Recipes = GetRecipes(ProductsIds, IngredientsIds);
            var RecipesIds = Recipes.Select(i => i.Id).ToList();
            await context.Recipes.AddRangeAsync(Recipes);

            var Employees = GetEmployees(rolesIds);
            var EmployeesIds = Employees.Select(i => i.Id).ToList();
            await context.Employees.AddRangeAsync(Employees);

            var Orders = GetOrders(EmployeesIds, OrderTypesIds, processIds);
            var OrdersIds = Orders.Select(i => i.Id).ToList();
            await context.Orders.AddRangeAsync(Orders);

            var OrderDetails = GetOrderDetails(OrdersIds, ProductsIds);
            var OrderDetailsIds = OrderDetails.Select(i => i.Id).ToList();
            await context.OrderDetails.AddRangeAsync(OrderDetails);

            await context.SaveChangesAsync();
        }
    }
}
