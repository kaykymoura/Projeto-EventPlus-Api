using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.event_.Domains;
using webapi.event_.Interfaces;

namespace webapi.event_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosEventosController : ControllerBase
    {
        private readonly IComentariosEventosRepository _comentariosEventosRepository;

        public ComentariosEventosController(IComentariosEventosRepository comentariosEventosRepository)
        {
            _comentariosEventosRepository = comentariosEventosRepository;
        }

        /// <summary>
        /// Endpoint para cadastrar Comentarios
        /// </summary>
        /// <param name="comentarioEvento"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ComentariosEventos comentarioEvento)
        {
            try
            {
                _comentariosEventosRepository.Cadastrar(comentarioEvento);
                // Melhor prática REST: Retornar status 201 com a URL do recurso criado
                return CreatedAtAction(nameof(GetById), new { UsuarioId = comentarioEvento.IdUsuario, EventoId = comentarioEvento.IdEvento }, comentarioEvento);
            }
            catch (Exception e)
            {
                // Retornar erro detalhado para depuração
                return BadRequest($"Erro ao cadastrar comentário: {e.Message}");
            }
        }

        /// <summary>
        /// Endpoint para listar Comentarios
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(Guid id)
        {
            try
            {
                List<ComentariosEventos> comentarios = _comentariosEventosRepository.Listar(id);
                if (comentarios == null || comentarios.Count == 0)
                {
                    return NotFound("Nenhum comentário encontrado.");
                }

                return Ok(comentarios);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao listar comentários: {e.Message}");
            }
        }

        /// <summary>
        /// Endpoint para deletar Comentarios
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                // Alteração: O método Deletar agora não retorna nada, então não capturamos um valor booleano
                _comentariosEventosRepository.Deletar(id);
                return NoContent(); // 204 - Sucesso sem conteúdo
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao deletar comentário: {e.Message}");
            }
        }

        /// <summary>
        /// Endpoint para buscar Comentarios por Id dos usuarios
        /// </summary>
        /// <param name="UsuarioId"></param>
        /// <param name="EventoId"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorIdUsuario/{UsuarioId},{EventoId}")]
        public IActionResult GetById(Guid UsuarioId, Guid EventoId)
        {
            try
            {
                ComentariosEventos comentario = _comentariosEventosRepository.BuscarPorIdUsuario(UsuarioId, EventoId);
                if (comentario == null)
                {
                    return NotFound($"Comentário não encontrado para o usuário {UsuarioId} e o evento {EventoId}.");
                }

                return Ok(comentario);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar comentário: {e.Message}");
            }
        }
    }
}
