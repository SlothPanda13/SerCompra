using SerCompra.Models.DataBase;

namespace SerCompra.Services
{
    public interface IAuthService
    {
        Task<Usuario?> ValidateUserAsync(string email, string password);
        Task<Usuario?> GetUserByEmailAsync(string email);
        Task<Usuario?> GetUserByRecoveryTokenAsync(string token);
        Task<string> GenerateRecoveryTokenAsync(Usuario user);
        Task ResetPasswordAsync(Usuario user, string newPassword);
    }
}