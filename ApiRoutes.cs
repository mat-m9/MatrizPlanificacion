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
            public const string Change = Base + "/Identity/changePassword";
        }

        public static class Rol
        {
            public const string Grant = "grant";
            public const string Revoke = "revoke";
        }

        public static class Proceso
        {
            public const string Area = "area";
            public const string Estado = "estado";
            public const string Etapa = "etapa";
        }
        public static class Unidad
        {
            public const string Usuario = "Unidad";
        }
    }
}
