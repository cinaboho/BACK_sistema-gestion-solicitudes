using Microsoft.IdentityModel.Tokens;
using PlantillaApiJWT.Models;
using Proyecto.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace wsFormularioSenescyt.Helper
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            IEnumerable<Claim> claims = new Claim[] {
                new Claim("Id", userAccounts.IdG.ToString()),
                    new Claim(ClaimTypes.Name, userAccounts.nombres),
                    new Claim(ClaimTypes.Email, userAccounts.EmailId),
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim("Creacion",  userAccounts.nbf.ToString()),
                    new Claim(ClaimTypes.Expiration,  userAccounts.exp.ToString())
            };
            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        public static UserTokens GenTokenkey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new UserTokens();
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = (DateTime)(model.FechaExpiracion == null ? DateTime.UtcNow.AddDays(1) : model.FechaExpiracion);
                UserToken.Validaty = expireTime.TimeOfDay;
                List<Claim> cl = GetClaims(model, out Id).ToList();
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),

                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256));
                JWToken.Payload["iat"] = model.iat;
                JWToken.Payload["id"] = model.id;
                JWToken.Payload["nombres"] = model.nombres;
                JWToken.Payload["tdoc"] = model.tdoc;
                JWToken.Payload["nbf"] = model.nbf;
                JWToken.Payload["exp"] = model.exp;
                JWToken.Payload["tipotoken"] = model.tipotoken;
                JWToken.Payload["EmailId"] = model.EmailId;
                JWToken.Payload["mensajeHabilitado"] = model.mensajeHabilitado;
                UserToken.token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.nombres = model.nombres;
                UserToken.id = model.id;
                UserToken.tdoc = model.tdoc;
                UserToken.iat = model.iat;
                UserToken.nbf = model.nbf;
                UserToken.EmailId = model.EmailId;
                UserToken.tipotoken = model.tipotoken;
                UserToken.exp = model.exp;
                UserToken.iss = model.iss;
                UserToken.aud = model.aud;
                UserToken.mensajeHabilitado = model.mensajeHabilitado;

                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string ValidateCurrentToken(string token, JwtSettings jwtSettings)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwToken = (JwtSecurityToken)validatedToken;
                var email = jwToken.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
                //var usuerName = jwToken.Claims.Single(x => x.Type == ClaimTypes.Email).Value;

                return email;
            }
            catch
            {
                return null;
            }
        }

        public static string GetCurrentToken(string token, JwtSettings jwtSettings)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwToken = (JwtSecurityToken)validatedToken;
                var email = jwToken.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
                var usuerName = jwToken.Claims.Single(x => x.Type == ClaimTypes.Email).Value;

                return email;
            }
            catch
            {
                return null;
            }
        }
    }
}