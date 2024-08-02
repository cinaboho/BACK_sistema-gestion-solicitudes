using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;
using PlantillaApiJWT.Models;
using Proyecto.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Transactions;
using wsFormularioSenescyt.Helper;

namespace PlantillaApiJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings jwtSettings;
        private readonly IHttpClientFactory clientFactory;

        public AccountController(IConfiguration configuration, JwtSettings jwtSettings, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            this.jwtSettings = jwtSettings;
            this.clientFactory = clientFactory; 
        }
        //[AllowAnonymous]
        //[HttpPost("LoginJWT")]
        private async Task<IActionResult> LoginJWT(LoginResponse segUsuario)
        {
            try
            {
                string mensaje = "";
                //VALIDO SI EL USUARIO ES VALIDO
                var Token = new UserTokens();

                
                var Issuer = _configuration["JsonWebTokenKeys:ValidIssuer"];
                var Audience = _configuration["JsonWebTokenKeys:ValidAudience"];

                long date = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var tiempomaximo = _configuration.GetValue<double>("TIEMPO_MAX_MIN_SESION");
                double minutos = tiempomaximo > 0 ? tiempomaximo :
                    (double)30;
                long datexpira = DateTimeOffset.UtcNow.AddMinutes(minutos).ToUnixTimeSeconds();
                var url = "https://wsarchivos.espol.edu.ec/";
                var transaccion = new List<DatosUsuario>();
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/Consultas/Persona/"+segUsuario.UsuCC + "@espol.edu.ec");
                        request.Content = new StringContent("{}", Encoding.UTF8, "application/json");
                        var postTask = client.GetAsync("api/Consultas/Persona/" + segUsuario.UsuCC + "@espol.edu.ec");
                        postTask.Wait();
                        var resultado = postTask.Result;
                        if (resultado.IsSuccessStatusCode)
                        {
                            transaccion = JsonConvert.DeserializeObject<List<DatosUsuario>>(
                                await resultado.Content.ReadAsStringAsync());
                            if(transaccion.Count>0 )
                            {
                                Token = JwtHelpers.GenTokenkey(new UserTokens()
                                {
                                    nombres = transaccion[0].apellidos + " " + transaccion[0].nombres,
                                    id = transaccion[0].correo,
                                    tdoc = null,
                                    iat = date,
                                    nbf = date,
                                    exp = datexpira,
                                    EmailId = transaccion[0].correo,
                                    FechaExpiracion = DateTime.Now.AddMinutes(minutos),
                                    mensajeHabilitado = mensaje,
                                    iss = Issuer,
                                    aud = Audience,
                                    tipotoken = "login"
                                }, jwtSettings); ;
                                // Log.Information("Usuario autorizado: " + transaccion[0].correo);
                                // Log.Information("Login - Usuario: " + transaccion[0].correo + "Fecha:" + DateTime.Now);
                            }
                            else
                            {
                                return NotFound(new JsonToken());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return NotFound(new JsonToken());
                }
                

                return Ok(Token);
            }
            catch (Exception ex)
            {
                // Log.Error("Login - " + ex.Message);
                throw;
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CasTicket userinfo)
        {
            Console.WriteLine(userinfo.ticket);
            var cas = this._configuration.GetValue<string>("CasBaseUrl");
            var front = this._configuration.GetValue<string>("RutaRootFrontEnd");
            var request = new HttpRequestMessage(HttpMethod.Get,
            cas + "/serviceValidate?service=" + front + "/&ticket=" + userinfo.ticket);


            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                try
                {
                    var text = (await response.Content.ReadAsStringAsync());
                    var begin = text.IndexOf("<cas:user>");
                    var end = text.IndexOf("</cas:user>");
                    var username = text.Substring(begin + 10, end - 10 - begin);
                    Console.WriteLine(username);
                    return await this.LoginJWT(new LoginResponse(username));
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }        
    }
}
