﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ronalfy_Jimenez_P2_Ap1.Models
{
    public class Ciudades
    {

        [Key]
        public int CiudadId { get; set; }

        public string Nombre { get; set; }

        public double Monto { get; set; }

    }
}
