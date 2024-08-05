namespace gestion_solicitudes_back.Models
{
    public class CreateUserDto
    {
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Cedula { get; set; } = null!;  // Usando string para evitar problemas de conversión
        public bool Estado { get; set; }
        public List<EspecialidadDto> Especialidades { get; set; } = new List<EspecialidadDto>();
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }

    public class EspecialidadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class RoleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
