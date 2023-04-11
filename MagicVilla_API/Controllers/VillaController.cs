using MagicVilla_API.Datos;
using MagicVilla_API.DTOs;
using MagicVilla_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VillaController : ControllerBase
    {
        //INYENCCION DE DEPENDENCIA
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;


        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        //FIN INYECCION DE DEPENDENCIA


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //Con el ActionResult anadido podemos enviar los status code en el "return"
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {

            //Lista todos las propiedades quemadas en la clase static en el return
            return Ok(VillaStore.villalist);
        }

        //Colocar id en el verbo para diferenciarlo del metodo anterior.
        [HttpGet("id:int", Name = "GetVilla")]
        //Documentacion tipos de respuesta
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)  
            {

                _logger.LogError( "Error al traer la villa con el Id: " + id);
                return BadRequest();
            }

            var villa = VillaStore.villalist.FirstOrDefault(x => x.Id == id);

            if (id == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]

        public ActionResult<VillaDTO> CrearVilla([FromBody] VillaDTO villaDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //SIRVE PARA VALIDAR SI YA EXISTE UN NOMBRE REGISTRADA ANTERIORMENTE
            if (VillaStore.villalist.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe.");         
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if (villaDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDto.Id = VillaStore.villalist.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            VillaStore.villalist.Add(villaDto);


            return CreatedAtRoute("GetVilla", new {id = villaDto.Id}, villaDto);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villalist.FirstOrDefault(v => v.Id == id);

            if (villa != null)
            {
                return NotFound();
            }

            VillaStore.villalist.Remove(villa);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }

            var villa = VillaStore.villalist.FirstOrDefault(v => v.Id == id);
            villa.Nombre = villaDTO.Nombre;
            villa.Ocupantes = villaDTO.Ocupantes;
            villa.MetrosCuadrados = villaDTO.MetrosCuadrados;

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id,JsonPatchDocument<VillaDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villalist.FirstOrDefault(v => v.Id == id);

            patchDto.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
            { 
            return BadRequest(ModelState);
            }
            return NoContent();
        }
       
    }
}
