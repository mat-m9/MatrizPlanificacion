using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace MatrizPlanificacion.Services
{
    public class ProgressService
    {
        private readonly DatabaseContext context;

        public ProgressService(DatabaseContext context)
        {
            this.context = context;
        }

        public async void ProgresoPreparatoria(Preparatoria preparatoria)
        {
            int countAvance = 0;
            decimal avance = 0;

            if(preparatoria.fechaAutorizacionReal != null)countAvance++;
            if(preparatoria.fechaEmisionReal != null)countAvance++;
            if(preparatoria.fechaMesaReal != null)countAvance++;
            if(preparatoria.fechaProgramadaReal != null) countAvance++;
            if(preparatoria.fechaPublicacionReal!= null) countAvance++;
            if(preparatoria.fechaRealReal != null) countAvance++;
            if(preparatoria.fechaRespuestaReal!= null) countAvance++;
            if(preparatoria.fechaSolicitudReal != null) countAvance++;

            avance = (countAvance * 50) / 8;
            avance = Math.Round(avance,2);
           
            ProcesoCompra proceso = await context.ProcesoCompras.FindAsync(preparatoria.IdProcesoCompra);
            proceso.Avance = avance;
            context.ProcesoCompras.Update(proceso);
            await context.SaveChangesAsync();
        }

        public async void ProgresoPreContractual(Precontractual precontractual)
        {
            int countAvance = 0;
            decimal avance = 0;


            if (precontractual.fechaAdjudicacionReal != null) countAvance++;

            if (countAvance == 1)
                avance = 20;

            Preparatoria preparatoria = await context.Preparatorias.FindAsync(precontractual.IdPreparatoria);
            ProcesoCompra proceso = await context.ProcesoCompras.FindAsync(preparatoria.IdProcesoCompra);

            proceso.Avance = avance + proceso.Avance;

            context.ProcesoCompras.Update(proceso);

            await context.SaveChangesAsync();
        }

        public async void ProgresoContractual(Contractual contractual)
        {
            int countAvance = 0;
            decimal avance = 0;

            if(contractual.fechaFinalizacionReal!= null) countAvance++;
            if(contractual.fechaSuscripcionReal!= null) countAvance++;

            avance = countAvance * 15;

            Precontractual precontractual = await context.Precontractuales.FindAsync(contractual.IdPrecontractual);
            Preparatoria preparatoria = await context.Preparatorias.FindAsync(precontractual.IdPreparatoria);
            ProcesoCompra proceso = await context.ProcesoCompras.FindAsync(preparatoria.IdProcesoCompra);

            proceso.Avance = avance + proceso.Avance;

            context.ProcesoCompras.Update(proceso);

            await context.SaveChangesAsync();
        }
    }
}
