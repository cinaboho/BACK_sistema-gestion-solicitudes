namespace sistema_gestion_solicitudes.Models
{
    public partial class AnexoField
    {

        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;
        public int AnexoId { get; set; }



    }
}
