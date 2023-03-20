using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.AppEntities;
using RestaurantManagement.Domain.Entities;
using System;
using System.IO;


namespace RestaurantManagement.MVC.Controllers
{
    public class Root
    {
        public int ID { get; set; }
        public int Head_ID { get; set; }
        public string Caption { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Params { get; set; }
        public string Icon { get; set; }
        public string Visible { get; set; }
        public string Type { get; set; }
    }

    public class MenuController : BaseController
    {
        private readonly IMenuRepository _menuRepository;
        public MenuController(IUnitOfWork service) : base(service)
        {
            _menuRepository = service.MenuRepository;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _menuRepository.GetListAsync(default, false);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> DXInsert([FromBody] List<Menu> links)
        {
            foreach (var link in links)
            {
                if (link.Id.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    await _menuRepository.AddAsync(link);
                }
                else
                {
                    await _menuRepository.Update(link);
                }
            }
            //await _menuRepository.UpdateOrAdd(links);
            //var result = await _menuRepository.AddRangeAsync(links);
            //var result = await _menuRepository.AddAsync(new Menu { Caption = "ufuk kod" });

            string json = JsonConvert.SerializeObject(links);
            System.IO.File.WriteAllText(@"wwwroot\menuJson.json", json);

            if (true)
            {
                return Ok(links);
            }
            else
            {
                return BadRequest(links);
            }

        }

        [HttpPost]
        public async Task<IActionResult> DXRemove([FromBody] Menu menu)
        {
            var result = await _menuRepository.Remove(menu);
            if (result)
            {
                return Ok(new Menu());
            }
            else
            {
                return BadRequest(new Menu());
            }

        }
    }
}
