using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.API.Exceptions;
using RestaurantManagement.API.Security;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Shared.CustomExceptions;
using RestaurantManagement.Shared.DTO;
using RestaurantManagement.Shared.ResponseModels;
using System.Text;

namespace RestaurantManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IConfiguration configuration;
        public AuthController(IUnitOfWork service, IConfiguration configuration) : base(service)
        {
            this.configuration = configuration;
        }
        [HttpGet("Login/{id}")]
        public async Task<ServiceResponse<LoginUserInfoDTO>> Login(string id)
        {
            var response = new ServiceResponse<LoginUserInfoDTO>();

            var emp = await service.EmployeeRepository.GetSingleAsync(x => x.PhoneNumber == id, false, x => x.Role);

            if (emp == null)
            {
                throw new AuthException("Eşleşen kullanıcı bulunamadı.");
            }
            else if (!emp.Active)
            {
                throw new AuthException("Kullanıcı pasif durumda.");
            }
            else
            {
                var token = TokenHandler.CreateToken(configuration, emp);

                response.Result = new LoginUserInfoDTO()
                {
                    Employee = emp,
                    AccessToken = token.AccessToken
                };
            }

            return response;
        }

        [HttpGet("ValidateToken/{token}")]
        public ServiceResponse<bool> ValidateToken(string token)
        {
            var response = new ServiceResponse<bool>();
            response.Result = TokenHandler.ValidateToken(configuration, token);
            return response;
        }
    }
}
