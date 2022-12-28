using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace MatrizPlanificacion.Services
{
    public class ProgressService
    {
        private readonly DatabaseContext context;
        DefaultProgressValues progressValues = new DefaultProgressValues();

        public ProgressService(DatabaseContext context)
        {
            this.context = context;
        }
        
        public async Task ProgresoPreparatoria(Preparatoria preparatoria)
        {
            int countAvance = 0;
            decimal avance = 0;

            if (preparatoria.informeNecesidadReal != null) countAvance++;
            if (preparatoria.terminosReferenciaReal != null) countAvance++;
            if (preparatoria.solicitudPublicacionReal != null) countAvance++;
            if (preparatoria.publicacionNecesidadReal != null) countAvance++;
            if (preparatoria.recepcionCotizacionesReal != null) countAvance++;
            if (preparatoria.elaboracionEstudioMercadoReal != null) countAvance++;
            if (preparatoria.solicitudPAPPReal != null) countAvance++;
            if (preparatoria.emisionPAPPReal != null) countAvance++;
            if (preparatoria.solicitudPresupReal != null) countAvance++;
            if (preparatoria.emisionPresupReal != null) countAvance++;
            if (preparatoria.solicitudPACReal != null) countAvance++;
            if (preparatoria.emisionPACReal != null) countAvance++;
            if (preparatoria.solicitudCoordinadorZonalReal != null) countAvance++;
            if (preparatoria.resolucionInicioReal != null) countAvance++;
            if (preparatoria.publicacionProcesoReal != null) countAvance++;

            avance = (countAvance * 100) / progressValues.TotalPasos;
            avance = Math.Round(avance, 2);

            ProcesoCompra proceso = await context.ProcesoCompras.FindAsync(preparatoria.IdProcesoCompra);
            proceso.Avance = avance;
            context.ProcesoCompras.Update(proceso);
            await context.SaveChangesAsync();
        }

        public async Task ProgresoPreContractual(Precontractual precontractual)
        {
            int countAvance = 0;
            decimal avance = 0;


            if (precontractual.preguntasRespuestasReal != null) countAvance++;
            if (precontractual.recepcionOfertasReal != null) countAvance++;
            if (precontractual.calificacionOfertasReal != null) countAvance++;
            if (precontractual.pujaNegociacionReal != null) countAvance++;
            if (precontractual.adjudicacionReal != null) countAvance++;

            avance = (countAvance * 100) / progressValues.TotalPasos;
            avance = Math.Round(avance, 2);

            Preparatoria preparatoria = await context.Preparatorias.FindAsync(precontractual.IdPreparatoria);
            ProcesoCompra proceso = await context.ProcesoCompras.FindAsync(preparatoria.IdProcesoCompra);

            proceso.Avance = avance + progressValues.TotalAvancePreparatoria;

            context.ProcesoCompras.Update(proceso);

            await context.SaveChangesAsync();
        }

        public async Task ProgresoContractual(Contractual contractual)
        {
            int countAvance = 0;
            decimal avance = 0;

            if (contractual.fechaSuscripcionReal != null) countAvance++;
            if (contractual.fechaFinalizacionReal != null) countAvance++;

            avance = (countAvance * 100) / progressValues.TotalPasos;
            avance = Math.Round(avance, 2);

            Precontractual precontractual = await context.Precontractuales.FindAsync(contractual.IdPrecontractual);
            Preparatoria preparatoria = await context.Preparatorias.FindAsync(precontractual.IdPreparatoria);
            ProcesoCompra proceso = await context.ProcesoCompras.FindAsync(preparatoria.IdProcesoCompra);

            proceso.Avance = avance + progressValues.TotalAvancePrecontractual;

            context.ProcesoCompras.Update(proceso);

            await context.SaveChangesAsync();
        }
    }
}
