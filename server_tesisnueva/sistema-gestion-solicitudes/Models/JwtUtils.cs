using Microsoft.Extensions.Caching.Memory;
using PlantillaApiJWT.WebApi;
using Proyecto.Models;
using wsFormularioSenescyt.Helper;

namespace PlantillaApiJWT.Models
{
    public class JwtUtils : IJwtUtils
    {
        private readonly JwtSettings _appSettings;

        public JwtUtils(JwtSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string GenTokenkey(UserTokens model)
        {
            var tokenSistema = JwtHelpers.GenTokenkey(model, _appSettings);
            return tokenSistema.Ambiente;
        }


        public async Task<string> ValidateToken(string token)
        //public async Task<UserTokens?> ValidateToken(string token, ISistemaApi sistemaApi, IUsuarioSistema usuarioSistema, IMemoryCache _cache = null)
        {
            Console.Out.WriteLine(String.IsNullOrEmpty(token) ? "Token vacío..." : token);
            if (token == null)
                return null;

            try
            {
                Console.Out.WriteLine("Entrando a validar...");
                //var validacion = await JwtHelpers.ValidateToken(token, _appSettings, sistemaApi, usuarioSistema, _cache);
                var validacion = JwtHelpers.ValidateCurrentToken(token, _appSettings);
                Console.Out.WriteLine(validacion);
                // return user id from JWT token if validation successful
                return validacion;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                // return null if validation fails
                return null;
            }
        }
    }
}
