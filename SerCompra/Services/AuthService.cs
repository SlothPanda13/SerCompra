using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SerCompra.Models.DataBase;

namespace SerCompra.Services
{
    public class AuthService : IAuthService
    {
        private readonly SercompraContext _context;

        public AuthService(SercompraContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ValidateUserAsync(string email, string password)
        {
            var user = await _context.Usuarios
                .Where(d => d.Email == email)
                .FirstOrDefaultAsync();

            if (user == null) return null;

            try
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Contraseña);
            }
            catch (Exception)
            {
                // Handle invalid hash format if necessary, or let it return false
                isPasswordValid = false;
            }

            return isPasswordValid ? user : null;
        }

        public async Task<Usuario?> GetUserByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetUserByRecoveryTokenAsync(string token)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.RecoveryToken == token);
        }

        public async Task<string> GenerateRecoveryTokenAsync(Usuario user)
        {
            string token = GetSha256(Guid.NewGuid().ToString());
            user.RecoveryToken = token;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task ResetPasswordAsync(Usuario user, string newPassword)
        {
            user.Contraseña = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.RecoveryToken = null;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private string GetSha256(string str)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] stream = sha256.ComputeHash(Encoding.ASCII.GetBytes(str));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in stream)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}