using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SerCompra.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email es requerido")]
        [StringLength(45, ErrorMessage = "Logitud máxima 45")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Error Correo")]
        [EmailAddress(ErrorMessage = "Correo electrónico incorrecto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        [StringLength(45, ErrorMessage = "Logitud máxima 45")]
        [DataType(DataType.Password, ErrorMessage = "Error contraseña")]
        public string Contraseña { get; set; }
    }
}
