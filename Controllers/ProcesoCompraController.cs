using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcesoCompraController : ControllerBase
    {

        private readonly DatabaseContext context;

        public ProcesoCompraController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProcesoCompra>>> Get()
        {
            var procesos = await context.ProcesoCompras.Include(p => p.Planta).Include(p => p.Preparatorias)
                            .Include(p => p.Estado)
                            .Include(p => p.Etapa).Include(p => p.Procedimiento).ToListAsync();
            if (!procesos.Any())
                return NotFound();
            return procesos;
        }

        [HttpGet("ProcesoFiltro")]
        public async Task<ActionResult<ICollection<procesoFiltro>>> GetProcesoFiltro()
        {
            var procesos = await context.ProcesoCompras
                            .Include(p => p.Estado)
                            .Include(p => p.Etapa).Include(p => p.Procedimiento).Select(a => new procesoFiltro()
                            {
                                IDproceso = a.ProcesoCompraId,
                                Avance = a.Avance,
                                Descripcion = a.descripcion,
                                Estado = a.Estado.tipoEstado,
                                Etapa = a.Etapa.tipoEtapa,
                                ProcesoContra = a.Procedimiento.tipoProcedimiento,
                                MesPlanificado = a.mesPlanificado

                            }).ToListAsync();
            if (!procesos.Any())
                return NotFound();
            return procesos;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<ProcesoCompra>>> GetProceso(string id)
        {
            var proceso = await context.ProcesoCompras.Where(e => e.ProcesoCompraId.Equals(id)).Include(p => p.Planta).Include(e => e.Preparatorias)
                            .Include(p => p.AlertasDSPPP).Include(p => p.Observaciones).FirstOrDefaultAsync();
            if (proceso == null)
                return NotFound();
            return Ok(proceso);
        }

        [HttpGet(template: ApiRoutes.Proceso.Etapa)]
        public async Task<ActionResult<ICollection<ProcesoCompra>>> GetProcesoEtapa(string idEtapa)
        {
            var proceso = await context.ProcesoCompras.Where(e => e.EtapaId.Equals(idEtapa)).Include(p => p.Planta)
                            .Include(p => p.Estado)
                            .Include(p => p.Etapa).Include(p => p.Procedimiento).ToListAsync();
            if (proceso == null)
                return NotFound();
            return Ok(proceso);
        }

        [HttpGet(template: ApiRoutes.Proceso.Estado)]
        public async Task<ActionResult<ICollection<ProcesoCompra>>> GetProcesoEstado(string idEstado)
        {
            var proceso = await context.ProcesoCompras.Where(e => e.EstadoId.Equals(idEstado)).Include(p => p.Planta)
                            .Include(p => p.Estado)
                            .Include(p => p.Etapa).Include(p => p.Procedimiento).ToListAsync();
            if (proceso == null)
                return NotFound();
            return Ok(proceso);
        }

        [HttpGet(template: ApiRoutes.Proceso.Area)]
        public async Task<ActionResult<ICollection<ProcesoCompra>>> GetProcesoArea(string idArea)
        {
            var proceso = await context.ProcesoCompras.Where(e => e.PlantaId.Equals(idArea)).Include(p => p.Planta)
                            .Include(p => p.Estado)
                            .Include(p => p.Etapa).Include(p => p.Procedimiento).ToListAsync();
            if (proceso == null)
                return NotFound();
            return Ok(proceso);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(ProcesoCompra procesoCompra)
        {
            var created = context.ProcesoCompras.Add(procesoCompra);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetProceso", new { id = procesoCompra.ProcesoCompraId }, created.Entity);

        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, ProcesoCompra procesoCompra)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            procesoCompra.ProcesoCompraId = id;
            context.ProcesoCompras.Update(procesoCompra);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var proceso = await context.ProcesoCompras.FindAsync(id);
            context.ProcesoCompras.Remove(proceso);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.ProcesoCompras.AnyAsync(p => p.ProcesoCompraId == id);
        }
    }
}

