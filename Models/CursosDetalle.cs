using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ronalfy_Jimenez_P2_Ap1.Models
{
    public class CursosDetalle
    {

        [Key]
        public int DetalleId { get; set; }

        [Required]
        public int CursosId { get; set; }

        [Required]
        public int CiudadId { get; set; }

        [Required]
        public string NombreCiudad { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto adicional debe ser mayor a 0")]
        public double MontoAdicional { get; set; }

        [NotMapped]
        public double MontoTotal => MontoBaseCiudad + MontoAdicional;

        [NotMapped]
        public double MontoBaseCiudad { get; set; }

        [ForeignKey("CursosId")]
        public virtual Cursos Cursos { get; set; }

        [ForeignKey("CiudadId")]
        public virtual Ciudades Ciudades { get; set; }

    }
}
