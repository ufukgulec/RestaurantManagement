namespace RestaurantManagement.API.Exceptions
{
    public class MissingLoginUserException : Exception
    {
        public MissingLoginUserException() : base(message: "Kullanıcı Id'si bulunamadı.")
        {
        }
    }
}
