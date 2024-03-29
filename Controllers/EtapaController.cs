﻿using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EtapaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public EtapaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Etapa>>> Get()
        {
            var etapas = await context.Etapas.ToListAsync();
            if (!etapas.Any())
                return NotFound();
            return etapas;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Etapa>>> GetEtapa(string id)
        {
            var etapa = await context.Etapas.Where(e => e.EtapaId.Equals(id)).FirstOrDefaultAsync();
            if (etapa == null)
                return NotFound();
            return Ok(etapa);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Etapa etapa)
        {
            var created = context.Etapas.Add(etapa);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetEtapa", new { id = etapa.EtapaId }, created.Entity);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Etapa etapa)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            etapa.EtapaId = id;
            context.Etapas.Update(etapa);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Etapas.Remove(new Etapa() { EtapaId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Etapas.AnyAsync(p => p.EtapaId == id);
        }
    }
}
