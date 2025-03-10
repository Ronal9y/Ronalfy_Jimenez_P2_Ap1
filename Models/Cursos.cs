using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ronalfy_Jimenez_P2_Ap1.Models
{
    public class Cursos
    {
        [Key]
        public int CursosId { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir la fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo se permiten letras")]
        public string? Asignatura { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir la monto")]
        public double Monto { get; set; }

    }
}
