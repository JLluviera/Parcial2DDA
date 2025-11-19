using System.ComponentModel.DataAnnotations;

namespace Parcial2DDA.Models
{
    public class Medicion
    {
        public int Id { get; set; }
        [Required]
        public string Huella { get; set; }

        public decimal Peso { get; set; }

        public string Tipo { get; set; }

        public DateTime FechaHora { get; set; }
    }
}
