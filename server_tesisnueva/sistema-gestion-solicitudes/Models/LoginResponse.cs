using Proyecto.Data.Interfaces;

namespace PlantillaApiJWT.Models
{
    public class LoginResponse : IEntity
    {
        public string? UsuCC { get; set; }

        public LoginResponse() { }
        public LoginResponse(string usuario)
        {
            this.UsuCC = usuario;
        }
    }
}
