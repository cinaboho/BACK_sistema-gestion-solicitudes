namespace Proyecto.Models
{
    public class JwtSettings
    {

        public bool ValidateIssuerSigningKey
        {
            get;
            set;
        }
        public string IssuerSigningKey
        {
            get;
            set;
        }
        public bool ValidateIssuer
        {
            get;
            set;
        } = true;
        public string ValidIssuer
        {
            get;
            set;
        }
        public bool ValidateAudience
        {
            get;
            set;
        } = true;
        public string ValidAudience
        {
            get;
            set;
        }
        public bool RequireExpirationTime
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

        public string? tipotoken
        {
            get;
            set;
        }

        public string? EmailId
        {
            get;
            set;
        }
        public string? tdoc
        {
            get;
            set;
        }


        public bool ValidateLifetime
        {
            get;
            set;
        } = true;
    }
}
