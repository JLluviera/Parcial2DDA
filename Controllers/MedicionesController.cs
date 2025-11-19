using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parcial2DDA.Data;
using Parcial2DDA.DTOs;
using Parcial2DDA.Models;

namespace Parcial2DDA.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MedicionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicionesController(AppDbContext context)
        {
            _context = context;
        }

        [Route("/medicion")]
        [HttpPost]
        public IActionResult PostMedicion([FromBody] IngresoMedicionDTO medicion)
        {
            if (medicion == null)
            {
                return BadRequest("Debe ingresar una medicion");
            }

            decimal peso;

            if (!decimal.TryParse(medicion.peso, out peso)) 
            {
                return BadRequest("Peso invalido");
            }

            if(medicion.tipo != "entrada" && medicion.tipo != "salida")
            {
                return BadRequest("Tipo invalido");
            }

            Medicion medicionNueva = new Medicion();

            medicionNueva.Huella = medicion.huella;
            medicionNueva.Peso = peso;
            medicionNueva.Tipo = medicion.tipo;
            medicionNueva.FechaHora = DateTime.Now;

            _context.Mediciones.Add(medicionNueva);
            _context.SaveChanges();

            return Ok();
        }
    }
}
