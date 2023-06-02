namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Presidente : BaseEntity
    {
        public Guid PersonaID { get; set; }
        public int NoPresidente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }

        // RELATIONS

        public Persona? Persona { get; set; }
    }
}
