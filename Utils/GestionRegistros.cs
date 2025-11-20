using Parcial2DDA.Data;
using Parcial2DDA.DTOs;
using Parcial2DDA.Models;

namespace Parcial2DDA.Utils
{
    public interface IGestionRegistros
    {
        public bool CompletarMedicion(string huella, Medicion salida);
        public decimal CalcularPeso(decimal entrada, decimal salida);
        public int CalcularTiempo(DateTime entrada, DateTime salida);

        public string SegundosAString(int segundos);

    }

    public class GestionRegistros : IGestionRegistros
    {
  
        private readonly AppDbContext _context;

        public GestionRegistros (AppDbContext context)
        {
            _context = context;
        }
        public bool CompletarMedicion(string huella, Medicion salida)
        {
            Medicion? entrada = _context.Mediciones.FirstOrDefault(m => m.Huella == huella);

            if (entrada == null)
            {
                _context.Mediciones.Add(salida);
                _context.SaveChanges();

                return false;
            }

            MedicionCompleta medicionCompleta = new MedicionCompleta();

            medicionCompleta.Huella = huella;
            medicionCompleta.Fecha = DateTime.Now;
            medicionCompleta.DiferenciaPeso = CalcularPeso(entrada.Peso, salida.Peso);
            medicionCompleta.DuracionEnSegs = CalcularTiempo(entrada.FechaHora, salida.FechaHora);

            _context.Mediciones.Remove(entrada);

            _context.MedicionesCompletas.Add(medicionCompleta);
            _context.SaveChanges();

            return true;
        }

        public decimal CalcularPeso(decimal entrada, decimal salida) 
        {
            return salida - entrada;
        }

        public int CalcularTiempo(DateTime entrada, DateTime salida) 
        {
            int resultado;

            int tiempo1 = (int)((DateTimeOffset)entrada).ToUnixTimeSeconds();
            int tiempo2 = (int)((DateTimeOffset)salida).ToUnixTimeSeconds();

            resultado = tiempo2 - tiempo1;

            return resultado;
        }

        public string SegundosAString(int segundos)
        {
            int minutos = segundos / 60;
            int horas = minutos / 60;

            return $"{horas} horas, {minutos - (horas*60) } minutos";
        }

        public int CantMedicionesCompletas()
        {
            int result = _context.MedicionesCompletas.Count();

            return result;
        }

        public decimal MaxDiferenciaDePeso()
        {
            decimal max = _context.MedicionesCompletas.Max(m => m.DiferenciaPeso);

            return max;
        }

        public string MaximoTiempo()
        {
            int max = _context.MedicionesCompletas.Max(m => m.DuracionEnSegs);

            return SegundosAString(max);
        }
    }
}






