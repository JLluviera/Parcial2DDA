using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parcial2DDA.Data;
using Parcial2DDA.DTOs;
using Parcial2DDA.Models;
using Parcial2DDA.Utils;

namespace Parcial2DDA.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MedicionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly GestionRegistros _gestionRegistros;

        public MedicionesController(AppDbContext context, GestionRegistros gestor)
        {
            _context = context;
            _gestionRegistros = gestor;
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

            if(medicionNueva.Tipo == "entrada")
            {
                _context.Mediciones.Add(medicionNueva);
                _context.SaveChanges();

                return Ok("Entreda registrada");
            }

            bool result = _gestionRegistros.CompletarMedicion(medicionNueva.Huella, medicionNueva);

            if (result == false)
            {
                return NotFound("No se encontro el ingreso");
            }
            else
            {
                return Ok("Se completo la medicion");
            }
        }
    }
}
