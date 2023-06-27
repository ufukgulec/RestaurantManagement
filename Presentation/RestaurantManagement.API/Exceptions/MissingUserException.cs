namespace RestaurantManagement.API.Exceptions
{
    public class MissingUserException : Exception
    {
        public MissingUserException() : base("Kayıtlı Kullanıcı Bulunamadı!")
        {

        }
    }
}
