using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nyous.Api.NOSQL.Domains;
using Nyous.Api.NOSQL.Interfaces.Repositories;

namespace Nyous.Api.NOSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly IEventoRepository _eventorepository;

        public EventosController(IEventoRepository eventoRepository)
        {
            _eventorepository = eventoRepository;
        }

        [HttpPost]
        public ActionResult<EventoDomain> Post(EventoDomain evento)
        {
            try
            {
                _eventorepository.Adicionar(evento);
                return Ok(evento);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<EventoDomain>> Get()
        {
            try
            {
                return _eventorepository.Listar();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            try
            {
                var evento = _eventorepository.BuscarPorId(id);
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                _eventorepository.Remover(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, EventoDomain evento)
        {
            try
            {
                evento.Id = id;
                _eventorepository.Atualizar(id, evento);

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
