using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Shared.CustomExceptions;
using RestaurantManagement.Shared.ResponseModels;

namespace RestaurantManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationController : BaseController
    {
        public NavigationController(IUnitOfWork service) : base(service)
        {
        }
        [ResponseCache(Duration = 120)]
        [HttpGet("GetList")]
        public async Task<ServiceResponse<List<Navigation>>> GetList()
        {

            var response = new ServiceResponse<List<Navigation>>();

            response.Result = await service.NavigationRepository.GetListAsync(default, false);

            if (response.Result == null)
                throw new ApiException("Kayıt Bulunamadı");

            return response;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(Navigation? entity)
        {
            var result = false;
            var Message = "";
            if (entity != null)
            {
                var exist = await service.NavigationRepository.GetSingleAsync(x => x.Caption.ToLower() == entity.Caption.ToLower());

                if (exist == null)
                {
                    result = await service.NavigationRepository.AddAsync(entity);
                    if (result)
                    {
                        Message = "Başarılı";
                    }
                    else
                    {
                        Message = "Eklerken bir hata oluştu";
                    }
                }
                else
                {
                    Message = "Eklemeye çalıştığınız menünün ismiyle bir tane daha kategori vardır.";
                }
            }
            if (result)
            {
                return Ok(Message);
            }
            else
            {
                return BadRequest(Message);
            }

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Navigation? entity)
        {
            var result = false;
            var Message = "";
            if (entity != null)
            {
                var exist = await service.NavigationRepository.GetByIdAsync(entity.Id.ToString(), false);

                if (exist != null)
                {
                    result = await service.NavigationRepository.Update(entity);
                    if (result)
                    {
                        Message = "Başarılı";
                    }
                    else
                    {
                        Message = "Güncellerken bir hata oluştu";
                    }
                }
                else
                {
                    Message = "Güncellerken çalıştığınız kategori bulunamadı.";
                }
            }
            if (result)
            {
                return Ok(Message);
            }
            else
            {
                return BadRequest(Message);
            }

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var result = false;
            var Message = "";
            var exist = await service.NavigationRepository.GetByIdAsync(id);

            if (exist != null)
            {
                if (exist.Active)
                {
                    exist.Active = false;
                    await service.NavigationRepository.Update(exist);
                    Message = "Kategori Pasif duruma getirildi.";
                }
                else
                {
                    result = await service.NavigationRepository.Remove(id);
                    if (result)
                    {
                        Message = "Başarılı";
                    }
                    else
                    {
                        Message = "Silerken bir hata oluştu";
                    }
                }

            }
            else
            {
                Message = "Silmeye çalıştığınız kategori bulunamadı";
            }

            if (result)
            {
                return Ok(Message);
            }
            else
            {
                return BadRequest(Message);
            }
        }
    }
}
