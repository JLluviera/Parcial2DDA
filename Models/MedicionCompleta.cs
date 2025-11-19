namespace Parcial2DDA.Models
{
    public class MedicionCompleta
    {
        public int Id { get; set; }

        public string Huella { get; set; }

        public decimal DiferenciaPeso { get; set; }

        public int DuracionEnSegs { get; set; }

        public DateTime Fecha { get; set; }
    }
}
