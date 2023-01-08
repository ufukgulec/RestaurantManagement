using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application;
using RestaurantManagement.Persistence.Contexts;

namespace RestaurantManagement.TaskForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            newOrder();
        }

        private async void newOrder()
        {
            ManagementContext context = getContext();
            Order order = new Order(new UnitOfWork(context));
            _ = await order.NewOrder();

            Environment.Exit(0);
        }

        private static ManagementContext getContext()
        {
            var contextBuilder = new DbContextOptionsBuilder();
            contextBuilder.UseSqlServer("server=UFUK;database=ManagementDB;integrated security=true;TrustServerCertificate=True;");

            var context = new ManagementContext(contextBuilder.Options);
            return context;
        }
    }
}