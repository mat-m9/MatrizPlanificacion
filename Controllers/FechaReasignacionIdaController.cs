﻿using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FechaReasignacionIdaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public FechaReasignacionIdaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<FechaReasignacionIda>>> Get()
        {
            var fechaIda = await context.FechaReasignacionIdas.ToListAsync();
            if (!fechaIda.Any())
                return NotFound();
            return fechaIda;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<FechaReasignacionIda>>> GetFechaIda(string id)
        {
            var fechaIda = await context.FechaReasignacionIdas.Where(e => e.IdIda.Equals(id)).FirstOrDefaultAsync();
            if (fechaIda == null)
                return NotFound();
            return Ok(fechaIda);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(FechaReasignacionIda fechasReasigIda)
        {
            var created = context.FechaReasignacionIdas.Add(fechasReasigIda);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetFechaIda", new { id = fechasReasigIda.IdIda }, created.Entity);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, FechaReasignacionIda fechasReasigIda)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            fechasReasigIda.IdIda = id;
            context.FechaReasignacionIdas.Update(fechasReasigIda);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var fechaIda = await context.FechaReasignacionIdas.FindAsync(id);
            context.FechaReasignacionIdas.Remove(fechaIda);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.FechaReasignacionIdas.AnyAsync(p => p.IdIda == id);
        }
    }
}
