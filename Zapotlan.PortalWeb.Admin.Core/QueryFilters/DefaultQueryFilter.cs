namespace Zapotlan.PortalWeb.Admin.Core.QueryFilters
{
    public abstract class DefaultQueryFilter
    {
        public bool? IncludeEliminados { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
