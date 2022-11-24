namespace MatrizPlanificacion
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Base = Root ;

        public static class Identity
        {
            public const string Login = Base + "/Identity/login";
            public const string Register = Base + "/Identity/register";
            public const string Refresh = Base + "/Identity/refresh";
        }

        public static class Rol
        {
            public const string Grant = "grant";
            public const string Revoke = "revoke";
        }

        public static class Area
        {
            public const string Tipo = "tipo";
            public const string Padre = "padre";
        }
    }
}
