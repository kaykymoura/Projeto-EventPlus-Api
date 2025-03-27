using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.event_.Domains;
using webapi.event_.Interfaces;

namespace webapi.event_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresencasEventosController : ControllerBase
    {
        private readonly IPresencasEventosRepository _presencasRepository;

        public PresencasEventosController(IPresencasEventosRepository presencasRepository)
        {
            _presencasRepository = presencasRepository;
        }

        [HttpPut]


        public IActionResult Put(Guid Id, PresencasEventos presencas)
        {
            try
            {
                _presencasRepository.Atualizar(Id, presencas);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                _presencasRepository.Deletar(Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<PresencasEventos> ListaPresencas = _presencasRepository.Listar();
                return Ok(ListaPresencas);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("ListarMinhas/{Id}")]
        public IActionResult Get(Guid Id)
        {
            try
            {
                List<PresencasEventos> ListaMinhas = _presencasRepository.ListarMinhas(Id);
                return Ok(ListaMinhas);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("BuscarPorId/{id}")]
        public IActionResult GetById(Guid Id)
        {
            try
            {
                PresencasEventos presencaBuscada = _presencasRepository.BuscarPorId(Id);

                return Ok(presencaBuscada);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
