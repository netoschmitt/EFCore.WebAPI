using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly HeroiContext _context;
        public ValuesController(HeroiContext context)
        {
            _context = context;
        }

        // GET: api/Values
        [HttpGet("filtro/{nome}")]
        public IActionResult GetFiltro(string nome)
        {
            var listHeroi = _context.Herois
                            .Where(h => EF.Functions.Like(h.Nome, $"%{nome}%"))
                            .OrderBy(h => h.Id)
                            .LastOrDefault();

            //var listHeroi = (from heroi in _context.Herois
            //                 where heroi.Nome.Contains(nome)
            //                 select heroi).ToList();

            

            return Ok(listHeroi);
        }

        // GET api/Values/5
        [HttpGet("Atualizar/{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            //var heroi = new Heroi { Nome = nameHero };

            var Heroi = _context.Herois
                        .Where(h => h.Id == 3)
                        .FirstOrDefault();

            Heroi.Nome = "Homem Aranha";

            //_context.Herois.Add(heroi);
            _context.SaveChanges();
            return Ok();
        }


        // GET api/Values/5
        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        {
            _context.AddRange(
                new Heroi { Nome = "Capitão América" },
                new Heroi { Nome = "Doutor Estranho" },
                new Heroi { Nome = "Pantera Negra" },
                new Heroi { Nome = "Viuva Negra" },
                new Heroi { Nome = "Hulk" },
                new Heroi { Nome = "Gavião Arqueiro" },
                new Heroi { Nome = "Capitã Marvel" }
                );
            _context.SaveChanges();
            return Ok();
        }

        // POST api/Values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Values/5
        [HttpGet("Delete/{id}")]
        public void Delete(int id)
        {
            var heroi = _context.Herois
                        .Where(x => x.Id == id)
                        .Single();
            _context.Herois.Remove(heroi);
            _context.SaveChanges();
        }
    }
}
