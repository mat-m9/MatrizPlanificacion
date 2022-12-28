using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreparatoriaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public PreparatoriaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Preparatoria>>> Get()
        {
            var preparatorias = await context.Preparatorias.Include(p => p.ProcesoCompra).ToListAsync();
            if (!preparatorias.Any())
                return NotFound();
            return preparatorias;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Preparatoria>>> GetPreparatoria(string id)
        {
            var preparatoria = await context.Preparatorias.Where(e => e.PreparatoriaId.Equals(id)).Include(p => p.ProcesoCompra).FirstOrDefaultAsync();
            if (preparatoria == null)
                return NotFound();
            return Ok(preparatoria);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Preparatoria preparatoria)
        {
            try
            {
                preparatoria.informeNecesidad = DateTime.SpecifyKind(preparatoria.informeNecesidad.Value, DateTimeKind.Utc);
                preparatoria.terminosReferencia = DateTime.SpecifyKind(preparatoria.terminosReferencia.Value, DateTimeKind.Utc);
                preparatoria.solicitudPublicacion = DateTime.SpecifyKind(preparatoria.solicitudPublicacion.Value, DateTimeKind.Utc);
                preparatoria.publicacionNecesidad = DateTime.SpecifyKind(preparatoria.publicacionNecesidad.Value, DateTimeKind.Utc);


                preparatoria.recepcionCotizaciones = DateTime.SpecifyKind(preparatoria.recepcionCotizaciones.Value, DateTimeKind.Utc);
                preparatoria.elaboracionEstudioMercado = DateTime.SpecifyKind(preparatoria.elaboracionEstudioMercado.Value, DateTimeKind.Utc);
                preparatoria.solicitudPAPP = DateTime.SpecifyKind(preparatoria.solicitudPAPP.Value, DateTimeKind.Utc);
                preparatoria.emisionPAPP = DateTime.SpecifyKind(preparatoria.emisionPAPP.Value, DateTimeKind.Utc);

                preparatoria.solicitudPresup = DateTime.SpecifyKind(preparatoria.solicitudPresup.Value, DateTimeKind.Utc);
                preparatoria.emisionPresup = DateTime.SpecifyKind(preparatoria.emisionPresup.Value, DateTimeKind.Utc);
                preparatoria.solicitudPAC = DateTime.SpecifyKind(preparatoria.solicitudPAC.Value, DateTimeKind.Utc);
                preparatoria.emisionPAC = DateTime.SpecifyKind(preparatoria.emisionPAC.Value, DateTimeKind.Utc);

                preparatoria.solicitudCoordinadorZonal = DateTime.SpecifyKind(preparatoria.solicitudCoordinadorZonal.Value, DateTimeKind.Utc);
                preparatoria.resolucionInicio = DateTime.SpecifyKind(preparatoria.resolucionInicio.Value, DateTimeKind.Utc);
                preparatoria.publicacionProceso = DateTime.SpecifyKind(preparatoria.publicacionProceso.Value, DateTimeKind.Utc);
              
                var created = context.Preparatorias.Add(preparatoria);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetPreparatoria", new { id = preparatoria.PreparatoriaId }, created.Entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Preparatoria preparatoria)
        {
            ProgressService progress = new ProgressService(context);
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            preparatoria.PreparatoriaId = id;
            try
            {
                preparatoria.informeNecesidad = DateTime.SpecifyKind(preparatoria.informeNecesidad.Value, DateTimeKind.Utc);
                if(preparatoria.informeNecesidadReal != null)
                    preparatoria.informeNecesidadReal = DateTime.SpecifyKind(preparatoria.informeNecesidadReal.Value, DateTimeKind.Utc);
                preparatoria.terminosReferencia = DateTime.SpecifyKind(preparatoria.terminosReferencia.Value, DateTimeKind.Utc);
                if(preparatoria.terminosReferenciaReal!= null)
                    preparatoria.terminosReferenciaReal = DateTime.SpecifyKind(preparatoria.terminosReferenciaReal.Value, DateTimeKind.Utc);
                preparatoria.solicitudPublicacion = DateTime.SpecifyKind(preparatoria.solicitudPublicacion.Value, DateTimeKind.Utc);
                if(preparatoria.solicitudPublicacionReal != null)
                    preparatoria.solicitudPublicacionReal = DateTime.SpecifyKind(preparatoria.solicitudPublicacionReal.Value, DateTimeKind.Utc);
                preparatoria.publicacionNecesidad = DateTime.SpecifyKind(preparatoria.publicacionNecesidad.Value, DateTimeKind.Utc);
                if(preparatoria.publicacionNecesidadReal!= null)
                    preparatoria.publicacionNecesidadReal = DateTime.SpecifyKind(preparatoria.publicacionNecesidadReal.Value, DateTimeKind.Utc);
                preparatoria.recepcionCotizaciones = DateTime.SpecifyKind(preparatoria.recepcionCotizaciones.Value, DateTimeKind.Utc);
                if(preparatoria.recepcionCotizacionesReal!= null)
                    preparatoria.recepcionCotizacionesReal = DateTime.SpecifyKind(preparatoria.recepcionCotizacionesReal.Value, DateTimeKind.Utc);
                preparatoria.elaboracionEstudioMercado = DateTime.SpecifyKind(preparatoria.elaboracionEstudioMercado.Value, DateTimeKind.Utc);
                if(preparatoria.elaboracionEstudioMercadoReal != null)
                    preparatoria.elaboracionEstudioMercadoReal = DateTime.SpecifyKind(preparatoria.elaboracionEstudioMercadoReal.Value, DateTimeKind.Utc);
                preparatoria.solicitudPAPP = DateTime.SpecifyKind(preparatoria.solicitudPAPP.Value, DateTimeKind.Utc);
                if(preparatoria.solicitudPAPPReal!= null)
                    preparatoria.solicitudPAPPReal = DateTime.SpecifyKind(preparatoria.solicitudPAPPReal.Value, DateTimeKind.Utc);
                preparatoria.emisionPAPP = DateTime.SpecifyKind(preparatoria.emisionPAPP.Value, DateTimeKind.Utc);
                if(preparatoria.emisionPAPPReal!= null)
                    preparatoria.emisionPAPPReal = DateTime.SpecifyKind(preparatoria.emisionPAPPReal.Value, DateTimeKind.Utc);
                preparatoria.solicitudPresup = DateTime.SpecifyKind(preparatoria.solicitudPresup.Value, DateTimeKind.Utc);
                if(preparatoria.solicitudPresupReal != null)
                    preparatoria.solicitudPresupReal = DateTime.SpecifyKind(preparatoria.solicitudPresupReal.Value, DateTimeKind.Utc);
                preparatoria.emisionPresup = DateTime.SpecifyKind(preparatoria.emisionPresup.Value, DateTimeKind.Utc);
                if(preparatoria.emisionPresupReal != null)
                    preparatoria.emisionPresupReal = DateTime.SpecifyKind(preparatoria.emisionPresupReal.Value, DateTimeKind.Utc);
                preparatoria.solicitudPAC = DateTime.SpecifyKind(preparatoria.solicitudPAC.Value, DateTimeKind.Utc);
                if(preparatoria.solicitudPACReal!= null)
                    preparatoria.solicitudPACReal = DateTime.SpecifyKind(preparatoria.solicitudPACReal.Value, DateTimeKind.Utc);
                preparatoria.emisionPAC = DateTime.SpecifyKind(preparatoria.emisionPAC.Value, DateTimeKind.Utc);
                if (preparatoria.emisionPACReal != null)
                    preparatoria.emisionPACReal = DateTime.SpecifyKind(preparatoria.emisionPACReal.Value, DateTimeKind.Utc);
                preparatoria.solicitudCoordinadorZonal = DateTime.SpecifyKind(preparatoria.solicitudCoordinadorZonal.Value, DateTimeKind.Utc);
                if(preparatoria.solicitudCoordinadorZonalReal != null)
                    preparatoria.solicitudCoordinadorZonalReal = DateTime.SpecifyKind(preparatoria.solicitudCoordinadorZonalReal.Value, DateTimeKind.Utc);
                preparatoria.resolucionInicio = DateTime.SpecifyKind(preparatoria.resolucionInicio.Value, DateTimeKind.Utc);
                if(preparatoria.resolucionInicioReal != null)
                    preparatoria.resolucionInicioReal = DateTime.SpecifyKind(preparatoria.resolucionInicioReal.Value, DateTimeKind.Utc);
                preparatoria.publicacionProceso = DateTime.SpecifyKind(preparatoria.publicacionProceso.Value, DateTimeKind.Utc);
                if(preparatoria.publicacionProcesoReal!= null)
                preparatoria.publicacionProcesoReal = DateTime.SpecifyKind(preparatoria.publicacionProcesoReal.Value, DateTimeKind.Utc);
                context.Preparatorias.Update(preparatoria);

                await context.SaveChangesAsync();
                await progress.ProgresoPreparatoria(preparatoria); 
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var preparatoria = await context.Preparatorias.FindAsync(id);
            context.Preparatorias.Remove(preparatoria);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Preparatorias.AnyAsync(p => p.PreparatoriaId == id);
        }
    }
}
