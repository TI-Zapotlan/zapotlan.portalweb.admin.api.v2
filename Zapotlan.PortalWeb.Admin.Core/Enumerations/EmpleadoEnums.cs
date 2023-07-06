namespace Zapotlan.PortalWeb.Admin.Core.Enumerations
{
    public enum EmpleadoStatusType
    {
        Ninguno,
        Activo,
        Licencia,
        Baja,
        Suspendido,
        Eliminado
    }

    public enum EmpleadoFechaType
    { 
        Ninguno,
        Ingreso,
        Termino,
        Actualizacion
    }

    public enum EmpleadoOrderFilterType
    { 
        Ninguno,
        Codigo,
        Nombre,
        FechaIngreso,
        CodigoDesc,
        NombreDesc,
        FechaIngresoDesc
    }
}
