using Microsoft.Extensions.Caching.Memory;
using PlantillaApiJWT.WebApi;

namespace PlantillaApiJWT.Security
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils, //IUsuarioSistema usuarioSistema,
            IMemoryCache _cache = null)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            Console.Out.WriteLine("Validando Token..." + token);
            //var userId = await jwtUtils.ValidateToken(token, sistemaApi,usuarioSistema, _cache);
            var userId = await jwtUtils.ValidateToken(token);

            if (userId != null)
            {
                Console.Out.WriteLine(userId);
                context.Items["Sistema"] = userId;
            }

            await _next(context);
        }
    }
}
