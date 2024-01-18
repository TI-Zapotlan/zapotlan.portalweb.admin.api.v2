namespace Zapotlan.PortalWeb.Admin.Core.Enumerations
{
    public enum EmpleadoStatusType
    {
        Ninguno,
        Activo,
        Licencia,
        Baja,
        Suspendido,
        Eliminado,
        EnSincronizacion
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

    public enum EmpleadoFileType
    { 
        Ninguno,
        FotoPerfil,
        CurriculumVitae
    }

    public enum EmpleadoSincronizableType
    {
        Ninguno,
        Sincronizar,
        NoSincronizar
    }
}
