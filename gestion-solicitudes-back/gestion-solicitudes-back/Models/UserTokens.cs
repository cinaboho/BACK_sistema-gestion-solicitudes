using System.ComponentModel.DataAnnotations;

namespace PlantillaApiJWT.Models
{
    public class CasTicket
    {
        [Required]
        public string ticket { get; set; }
    }
    public class UserTokens
    {

        public string? token
        {
            get;
            set;
        }
        public string? nombres
        {
            get;
            set;
        }
        public string? id
        {
            get;
            set;
        }
        public string? tdoc
        {
            get;
            set;
        }

        public TimeSpan Validaty
        {
            get;
            set;
        }
        public string RefreshToken
        {
            get;
            set;
        }
        public Guid IdG
        {
            get;
            set;
        }
        public string? EmailId
        {
            get;
            set;
        }
        public Guid GuidId
        {
            get;
            set;
        }
        public DateTime? FechaExpiracion
        {
            get;
            set;
        }
        public long iat
        {
            get;
            set;
        }

        public long nbf
        {
            get;
            set;
        }

        public long exp
        {
            get;
            set;
        }
        public string Ambiente
        {
            get;
            set;
        }
        public string iss
        {
            get;
            set;
        }
        public string aud
        {
            get;
            set;
        }
        public string? tipotoken
        {
            get;
            set;
        }
        

        public string? mensajeHabilitado
        {
            get;
            set;
        }
        

        public UserTokens()
        {
            this.IdG = Guid.NewGuid();
            this.GuidId = Guid.NewGuid();
#if DEBUG
            this.Ambiente = "Desarrollo";
#else
    this.Ambiente = "Produccion";
#endif
        }
    }
}
