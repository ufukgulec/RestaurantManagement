using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application;
using RestaurantManagement.Console;
using RestaurantManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

var contextBuilder = new DbContextOptionsBuilder();
contextBuilder.UseSqlServer("server=UFUK;database=ManagementDB;integrated security=true;TrustServerCertificate=True;");

var context = new ManagementContext(contextBuilder.Options);

OrderCounts OrderCounts = new OrderCounts(new UnitOfWork(context));

var data = await OrderCounts.CategoryOrder(OrderCounts.Filter.d);

Console.WriteLine("Hello, World!");
