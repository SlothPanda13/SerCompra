using System.ComponentModel.DataAnnotations;

namespace SerCompra.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email es requerido")]
        [StringLength(45, ErrorMessage = "Logitud máxima 45")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Error Correo")]
        [EmailAddress(ErrorMessage = "Correo electrónico incorrecto")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        [StringLength(45, ErrorMessage = "Logitud máxima 45")]
        [DataType(DataType.Password, ErrorMessage = "Error contraseña")]
        public required string Contraseña { get; set; }
    }
}