using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MatrizPlanificacion.Modelos;

namespace MatrizPlanificacion.Services
{
    public class PopulateDB
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                DefaultRolesServices rolesServices = scope.ServiceProvider.GetRequiredService<DefaultRolesServices>();

                List<string> itemsPresupuestarios = new()
                {
                    "510105","510106","510203","510204","510304","510306","510401","510408","510509","510510",
                    "510512","510513","510601","510602","510704","510707","530101","530104","530105","530106",
                    "530201","530202","530203","530204","530207","530208","530209","530224","530228","530241",
                    "530246","530301","530303","530402","530403","530404","530405","530418","530502","530505",
                    "530601","530604","530701","530702","530703","530704","530801","530802","530803","530804",
                    "530805","530807","530808","530809","530810","530811","530812","530813","530820","530824",
                    "530826","530829","531403","531404","531406","531407","531408","531411","570102","570104",
                    "570199","570201","570206","570216","570218","840103","840104","840107","840111","990101","990102"

                };
                List<string> procedimientosContratacion = new()
                {
                    "CATÁLOGO ELECTRÓNICO","SUBASTA INVERSA","RÉGIMEN ESPECIAL","CONSULTORÍA CONTRATACIÓN DIRECTA",
                    "LICITACIÓN SEGUROS","POLÍTICA BID","ÍNFIMA CUANTÍA","OTROS"
                };
                List<string> estados = new()
                {
                    "CERTIFICADO","COMPROMETIDO","DEVENGADO TOTAL","DEVENGADO PARCIAL","NINGUNO"

                };
                List<string> etapas = new()
                {
                    "SIN TDRS O ESPECIFICACIONES TÉCNICAS","ETAPA PREPARATORIA","ETAPA PRECONTRACTUAL","ETAPA CONTRACTUAL","FINALIZADO","CANCELADO"

                };
                List<string> unidades = new()
                {
                    "CENTRO LOCAL BABAHOYO","CENTRO LOCAL ESMERALDAS","CENTRO LOCAL LOJA","CENTRO LOCAL MACAS",
                    "CENTRO LOCAL NUEVA LOJA","CENTRO LOCAL RIOBAMBA","CENTRO LOCAL SAN CRISTÓBAL","CENTRO LOCAL SANTO DOMINGO",
                    "CENTRO LOCAL TULCÁN","CENTRO ZONAL AMBATO","CENTRO ZONAL CUENCA","CENTRO ZONAL IBARRA","CENTRO ZONAL MACHALA",
                    "CENTRO ZONAL PORTOVIEJO","CENTRO ZONAL QUITO","CENTRO ZONAL SAMBORONDON","DIRECCIÓN ADMINISTRATIVA",
                    "DIRECCIÓN DE ADMINISTRACIÓN DE RECURSOS HUMANOS","DIRECCIÓN FINANCIERA","DIRECCIÓN DE COMUNICACIÓN SOCIAL",
                    "DIRECCIÓN NACIONAL ACADÉMICO PARA EMERGENCIAS","DIRECCIÓN NACIONAL REGULATORIO EN EMERGENCIAS ",
                    "DIRECCIÓN DE GESTIÓN DOCUMENTAL Y ARCHIVO","DIRECCIÓN NACIONAL DE ANALISIS DE DATOS","DIRECCIÓN DE OPERACIONES",
                    "DIRECCIÓN DE PROYECTOS E INNOVACIÓN","DIRECCIÓN NACIONAL DE GESTION DE INFRAESTRUCTURA TECNOLÓGICA PARA EMERGENCIAS",
                    "DIRECCIÓN DE ASESORÍA JURÍDICA","PROYECTO SAT"

                };
                List<string> roles = rolesServices.GetRolesList();
                var roleStore = new RoleStore<IdentityRole>(context);
                foreach (string role in roles)
                {
                    if (!context.Roles.Any(r => r.NormalizedName == role.ToUpper()))
                    {
                        await roleStore.CreateAsync(new()
                        {
                            Name = role,
                            NormalizedName = role.ToUpper(),
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });
                    }
                }

                foreach (string item in itemsPresupuestarios)
                {
                    var itemEncontrado = await context.ItemsPresup.Where(i => i.nunItem.Equals(item)).FirstOrDefaultAsync();
                    if (itemEncontrado == null)
                    {
                        context.ItemsPresup.Add(new ItemPresupuestario
                        {
                            nunItem= item,
                        });
                        await context.SaveChangesAsync();
                    }
                }
                foreach (string procedimiento in procedimientosContratacion)
                {
                    var proc = await context.ProcedimientoContrataciones.Where(i => i.tipoProcedimiento.Equals(procedimiento)).FirstOrDefaultAsync();
                    if (proc == null)
                    {
                        context.ProcedimientoContrataciones.Add(new ProcedimientoContratacion
                        {
                            tipoProcedimiento = procedimiento,
                        });
                        await context.SaveChangesAsync();
                    }
                }
                foreach (string estado in estados)
                {
                    var est = await context.Estados.Where(i => i.tipoEstado.Equals(estado)).FirstOrDefaultAsync();
                    if (est == null)
                    {
                        context.Estados.Add(new Estado
                        {
                            tipoEstado = estado,
                        });
                        await context.SaveChangesAsync();
                    }
                }
                foreach (string etapa in etapas)
                {
                    var etapaEncontrada = await context.Etapas.Where(i => i.tipoEtapa.Equals(etapa)).FirstOrDefaultAsync();
                    if (etapaEncontrada == null)
                    {
                        context.Etapas.Add(new Etapa
                        {
                            tipoEtapa = etapa,
                        });
                        await context.SaveChangesAsync();
                    }
                }
                foreach (string unidad in unidades)
                {
                    var sector = await context.PlantaUnidadAreas.Where(i => i.nombre.Equals(unidad)).FirstOrDefaultAsync();
                    if (sector == null)
                    {
                        context.PlantaUnidadAreas.Add(new()
                        {
                             nombre = unidad,
                        });
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
