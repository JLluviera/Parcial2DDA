using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parcial2DDA.Data;
using Parcial2DDA.DTOs;
using Parcial2DDA.Utils;

namespace Parcial2DDA.Controllers
{
    [Route("/reportes")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly GestionRegistros _gestor;

        public ReportesController(AppDbContext context, GestionRegistros gestor)
        {
            _context = context;
            _gestor = gestor;
        }

        [HttpGet]
        [Route("total")]
        public IActionResult TotalMediciones()
        {
            RespuestaTotalMedicionesDTO resp = new RespuestaTotalMedicionesDTO();
            resp.total_mediciones_completadas = _gestor.CantMedicionesCompletas();

            return Ok(resp);
        }

        [HttpGet]
        [Route("maxima_diferencia_peso")]
        public IActionResult ReporteMaxPeso()
        {
            RespuestaMaxDiferenciaPesoDTO resp = new RespuestaMaxDiferenciaPesoDTO();

            resp.maxima_diferencia_peso = _gestor.MaxDiferenciaDePeso();

            return Ok(resp);
        }

        [HttpGet]
        [Route("maximo_tiempo")]
        public IActionResult ReporteMaximoTiempo()
        {
            int maximo = _context.MedicionesCompletas.Max(m => m.DuracionEnSegs);

            RespMaxTiempoDTO resp = new RespMaxTiempoDTO();
            resp.maximo_tiempo = _gestor.SegundosAString(maximo);

            return Ok(resp);

        }
    }
}
