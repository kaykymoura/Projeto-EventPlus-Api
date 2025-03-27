using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.event_.Domains;
using webapi.event_.Interfaces;
using System;
using System.Collections.Generic;

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

        // POST: api/presencaseventos
        [HttpPost]
        public IActionResult Post(PresencasEventos presencas)
        {
            try
            {
                // Chama o repositório para adicionar a nova presença no evento
                _presencasRepository.Inscrever(presencas);
                // Retorna o status 201 (Created) com a URL do novo recurso
                return CreatedAtAction(nameof(GetById), new { id = presencas.IdPresencaEvento }, presencas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/presencaseventos/{id}
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, PresencasEventos presencas)
        {
            try
            {
                _presencasRepository.Atualizar(id, presencas);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("Erro ao atualizar presença");
            }
        }

        // DELETE: api/presencaseventos/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _presencasRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/presencaseventos
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
                return BadRequest("Erro ao listar presenças");
            }
        }

        // GET: api/presencaseventos/ListarMinhas/{id}
        [HttpGet("ListarMinhas/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                List<PresencasEventos> ListaMinhas = _presencasRepository.ListarMinhas(id);
                return Ok(ListaMinhas);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao listar presenças do usuário");
            }
        }

        // GET: api/presencaseventos/BuscarPorId/{id}
        [HttpGet("BuscarPorId/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                PresencasEventos presencaBuscada = _presencasRepository.BuscarPorId(id);
                return Ok(presencaBuscada);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
