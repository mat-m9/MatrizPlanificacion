namespace MatrizPlanificacion.Services
{
    public class DefaultRolesServices
    {
        public static readonly Dictionary<string, string> Roles = new Dictionary<string, string>
        {
            {"superadmin","SUPERADMINISTRADOR" },
            {"admin","ADMINISTRADOR" },
            {"usuario","USUARIO" },
            {"superuser", "SUPERUSUARIO" }
        };

        public Task<string> GetDefaultRole(string key)
        {
            return Task.FromResult(Roles.ContainsKey(key) ? Roles[key] : null);
        }

        public List<string> GetRolesList()
        {
            return Roles.Values.ToList();
        }
    }
}
