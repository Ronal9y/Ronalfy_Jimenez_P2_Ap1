using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ronalfy_Jimenez_P2_Ap1.Models
{
    public class CursosDetalle
    {
        [Key]
        public int DetalleId { get; set; }

        [Required]
        public int CursosId { get; set; } // 👈 Clave foránea correcta

        [Required]
        public int CiudadId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public double Monto { get; set; }

        // Relación con la tabla Cursos
        [ForeignKey("CursosId")]
        public virtual Cursos? Curso { get; set; }  // 👈 Referencia a Cursos corregida

        // Relación con la tabla Ciudades
        [ForeignKey("CiudadId")]
        public virtual Ciudades? Ciudad { get; set; }
    }
}