using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LogsController : ControllerBase
    {
        private readonly DatabaseContextLogs context;

        public LogsController(DatabaseContextLogs context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Logs>>> GetLogs()
        {
            var Logs = await context.Logs.ToListAsync();
            if (!Logs.Any())
                return NotFound();
            return Logs;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(LogPost log)
        {
            Logs newLog = new Logs();
            newLog.userName = log.UserName;
            newLog.action= log.Action;
            newLog.date = DateTime.Now;
            newLog.date = DateTime.SpecifyKind(newLog.date.Value, DateTimeKind.Utc);
            var created = context.Logs.Add(newLog);
            await context.SaveChangesAsync();
            return Ok(created);
        }
    }
}
