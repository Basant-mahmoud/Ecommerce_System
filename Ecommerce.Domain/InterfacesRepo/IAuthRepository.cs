namespace Ecommerce_System.Ecommerce.Domain.InterfacesRepo
{
    public interface IAuthRepository
    {
        Task<bool> EmailExistAsync(string email) ;
        Task<bool> UsernameExistsAsync(string username);

    }
}
