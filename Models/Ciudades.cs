using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ronalfy_Jimenez_P2_Ap1.Models
{
    public class Ciudades
    {

        [Key]
        public int CiudadId { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo se permiten letras")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "Este campo obligatorio")]
        public double Monto { get; set; }

    }
}
