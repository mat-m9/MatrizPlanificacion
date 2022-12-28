namespace MatrizPlanificacion.Services
{
    public class DefaultProgressValues
    {
        public int TotalPasosPreparatoria = 15;
        public int TotalPasosPrecontractual = 5;
        public int TotalPasosContractual = 2;
        public int TotalPasos;
        public int TotalAvancePreparatoria = 68;
        public int TotalAvancePrecontractual = 91;

        public DefaultProgressValues()
        {
            TotalPasos = TotalPasosPreparatoria + TotalPasosPrecontractual + TotalPasosContractual;
        }
    }
}
