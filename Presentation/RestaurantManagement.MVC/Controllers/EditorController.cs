using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using NuGet.Packaging;
using RestaurantManagement.Application.Provider;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.MVC.Models;
using RestaurantManagement.Persistence.Provider;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RestaurantManagement.MVC.Controllers
{
    public class EditorController : Controller
    {
        private readonly IDbProvider _provider;
        public EditorController(IDbProvider dbProvider)
        {
            _provider = dbProvider;
        }
        public IActionResult Index()
        {
            ViewBag.PageHeader = "Sql Editör";
            return View();
        }
        [HttpGet]
        public IActionResult getDBTable()
        {
            string query = @"SELECT DISTINCT TABLE_NAME TABLES,
STUFF((SELECT ',' + S.COLUMN_NAME as [text()] from INFORMATION_SCHEMA.COLUMNS S where S.TABLE_NAME=D.TABLE_NAME ORDER BY ORDINAL_POSITION for xml path('')),1,1,'') COLUMNS
FROM INFORMATION_SCHEMA.COLUMNS D
WHERE TABLE_NAME NOT IN ('__EFMigrationsHistory','sysdiagrams')
ORDER BY TABLE_NAME";
            DataTable dt = _provider.GetData(query);

            List<DBTable> tableData = new List<DBTable>();

            foreach (DataRow row in dt.Rows)
            {
                DBTable table = new DBTable();
                table.Name = row["TABLES"].ToString();
                table.items.AddRange(row["COLUMNS"].ToString().Split(","));

                tableData.Add(table);
            }
            return Json(JsonConvert.SerializeObject(tableData));
        }
        [HttpGet]
        public IActionResult Sql()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RunSql([FromBody] string query)
        {
            DataTable dataTable = _provider.GetData(query);
            if (dataTable is not null)
            {
                return this.Json(JsonConvert.SerializeObject(dataTable));
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult CreateView([FromBody] string query)
        {
            DataTable dataTable = _provider.GetData("CREATE VIEW Deneme AS " + query);
            if (dataTable is not null)
            {
                return this.Json(JsonConvert.SerializeObject(dataTable));
            }
            return BadRequest();
        }
    }
}
