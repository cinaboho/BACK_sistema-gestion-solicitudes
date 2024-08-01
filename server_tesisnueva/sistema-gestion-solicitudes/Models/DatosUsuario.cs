namespace PlantillaApiJWT.Models
{
    public class DatosUsuario
    {
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string estado { get; set; }
        public bool esAdministrativo { get; set; }
        public bool esDocente { get; set; }
    }
}
