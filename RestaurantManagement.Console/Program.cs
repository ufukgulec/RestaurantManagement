using RestaurantManagement.Domain.Entities;
using System.Net.Http.Json;

internal class Program
{
    private static async Task Main(string[] args)
    {
        HttpClient client = new HttpClient();

        Console.WriteLine("category - 1");

        var u = Convert.ToInt32(Console.ReadLine());

        switch (u)
        {
            case 1:
                var response = await client.GetFromJsonAsync<List<Category>>("https://localhost:5001/api/category/getall?top=2&count=true");
                Console.WriteLine(response.Count);
                break;
        }
        Console.ReadLine();
    }
}