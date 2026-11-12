using Microsoft.AspNetCore.Mvc;
using Recuperatorio.Entities;

namespace Recuperatorio.Controllers;

[ApiController]
[Route("[controller]")]
public class ElectrodomesticoController : ControllerBase
{
    private static readonly List<Electrodomestico> Electrodomesticos = new()
    {
        new Electrodomestico { Id = 1, Nombre = "Electrodomestico 1", Modelo = "Accion", Estado = true },
        new Electrodomestico { Id = 2, Nombre = "Electrodomestico 2", Modelo = "Aventura", Estado = true },
        new Electrodomestico { Id = 3, Nombre = "Electrodomestico 3", Modelo = "Estrategia", Estado = false }
    };

    private readonly ILogger<ElectrodomesticoController> _logger;

    public ElectrodomesticoController(ILogger<ElectrodomesticoController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public ActionResult<Electrodomestico> Create([FromBody] Electrodomestico? electrodomestico)
    {
        try
        {
            if (electrodomestico is null ||
                electrodomestico.Id <= 0 ||
                string.IsNullOrWhiteSpace(electrodomestico.Nombre) ||
                string.IsNullOrWhiteSpace(electrodomestico.Modelo))
            {
                return BadRequest("El electrodoméstico debe tener un Id válido, Nombre y Modelo.");
            }

            if (Electrodomesticos.Any(e => e.Id == electrodomestico.Id))
            {
                return Conflict($"Ya existe un electrodoméstico con Id {electrodomestico.Id}.");
            }

            Electrodomesticos.Add(electrodomestico);
            return Ok(electrodomestico);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el electrodoméstico.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al crear el electrodoméstico.");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult<Electrodomestico> Update(int id, [FromBody] Electrodomestico? electrodomestico)
    {
        try
        {
            if (id <= 0 ||
                electrodomestico is null ||
                string.IsNullOrWhiteSpace(electrodomestico.Nombre) ||
                string.IsNullOrWhiteSpace(electrodomestico.Modelo))
            {
                return BadRequest("El Id debe ser válido y el electrodoméstico debe tener Nombre y Modelo.");
            }

            var existente = Electrodomesticos.FirstOrDefault(e => e.Id == id);
            if (existente is null)
            {
                return NotFound($"No se encontró un electrodoméstico con Id {id}.");
            }

            existente.Nombre = electrodomestico.Nombre;
            existente.Modelo = electrodomestico.Modelo;
            existente.Estado = electrodomestico.Estado;

            return Ok(existente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el electrodoméstico con Id {Id}.", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al actualizar el electrodoméstico.");
        }
    }
}
