using Microsoft.EntityFrameworkCore;
using Servidor.Context;
using Servidor.Interfaces;
using Servidor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connection
var conn = builder.Configuration.GetConnectionString("AppConnection");
//context
builder.Services.AddDbContext<CuentaContext>(x => x.UseSqlServer(conn));
builder.Services.AddDbContext<DbFinalProjectContext>(x => x.UseSqlServer(conn));
builder.Services.AddDbContext<RegistroVistaAdminContext>(x => x.UseSqlServer(conn));
builder.Services.AddDbContext<VistaDisponibilidadContext>(x => x.UseSqlServer(conn));
builder.Services.AddDbContext<SesionIniciadaContext>(x => x.UseSqlServer(conn));

//services
builder.Services.AddScoped<ISesion, SesionIniciadaService>();
builder.Services.AddScoped<IVistaAdmin, RegistroVistaAdminService>();
builder.Services.AddScoped<ITarifa, CalcularTarifaService>();
builder.Services.AddScoped<IVistaDisponibilidad, VistaDisponibilidadService>();
builder.Services.AddScoped<IAdmin, AdminService>();
builder.Services.AddScoped(typeof(Irepository<>), typeof(RepositoryService<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
});

app.MapControllers();

app.Run();
