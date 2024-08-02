using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using PlantillaApiJWT.Helper;
using Proyecto.Models;
using sistema_gestion_solicitude;
using sistema_gestion_solicitudes;
using sistema_gestion_solicitudes.Models;

var builder = WebApplication.CreateBuilder(args);
AddJWTTokenServicesExtensions.AddJWTTokenServices(builder.Services, builder.Configuration);



builder.Services.AddEntityFrameworkMySQL().AddDbContext<GestionContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddHostedService<TareaAutomatica>();


builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                        {
                            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                    }
                                },
                                new string[] {}
                }
            });
});

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddHttpClient();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JsonWebTokenKeys"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
    _ = endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
});

//app.MapControllers();

app.Run();
