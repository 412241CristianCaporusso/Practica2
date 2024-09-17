using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Practica2.Models;
using Practica2.Repositories.Implementations;
using System.Collections.Generic;

namespace Practica2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ArticlesController : ControllerBase
    {

        private AplicacionRepository aplicacionRepository ;


        public ArticlesController()
        {
            aplicacionRepository = new AplicacionRepository();
        }


        [HttpGet("ArticulosGET")]

        public IActionResult Get()
        {
            return Ok(aplicacionRepository.GetAll());
        }

        [HttpPost("ArticuloPOST")]
        public IActionResult Save([FromBody] Article article)
        {
            try {
                if (article == null)
                {
                    return BadRequest("Se esperaba un articulo completo");
                }
           
            if (aplicacionRepository.Add(article))

                return Ok("Se añadió existosamente!");
            else
                return StatusCode(500, "No se pudo registrar el articulo!");
            }
            catch (Exception )
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }

        }

        [HttpPut("ArticuloPUT")]

        public IActionResult Put([FromBody] Article article)
        {
            if (aplicacionRepository.Edit(article))
            {
                return Ok("Se actualizó correctamente");
            }
            else 
            {
                return StatusCode(500, "No se pudo actualizar el articulo!");
            }
        }


        [HttpDelete("ArticuloDELETE")]

        public IActionResult Delete(int id) 
        {

            if (aplicacionRepository.Delete(id))
            {
                return Ok("Se eliminó correctamente");
            }
            else
            {
                return StatusCode(500, "No se pudo eliminar el articulo!");
            }
        }

    }
}
