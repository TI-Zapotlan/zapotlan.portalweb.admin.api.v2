using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Sesion : BaseEntity
    {
        public Guid PresidenteID { get; set; }

        public DateTime FechaSesion { get; set; }
        public string Resumen { get; set; } = string.Empty;
        public string ArchivoPDF { get; set; } = string.Empty;
        public SesionType Tipo { get; set; } = SesionType.Ninguno;
        public int Numero { get; set; }

        // Nuevos campos
        public Guid? ComisionID { get; set; }
        public string Asunto { get; set; } = string.Empty;
        public string Lugar { get; set; } = string.Empty;
        public Guid? ArchivoConvocatoriaID { get; set; }
        public Guid? ArchivoOrdenDiaID { get; set; }
        public Guid? ArchivoAsistenciaID { get; set; }
        public Guid? ArchivoEstenograficoID { get; set; }
        public Guid? ArchivoSentidoVotacionID { get; set; }
        public string VideoURL { get; set; } = string.Empty;

        // RELATIONS

        public virtual Presidente? Presidente { get; set; }
        public virtual ICollection<Nota>? Notas { get; set; }
    }
}
