using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Shared.DTO
{
    public class LoginUserInfoDTO
    {
        public Employee Employee { get; set; }
        public string AccessToken { get; set; }
    }
}
