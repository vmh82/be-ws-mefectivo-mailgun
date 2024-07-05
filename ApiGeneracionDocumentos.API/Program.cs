using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Domain.Services;
using ApiGeneracionDocumentos.Infraestructure.Services;
using ApiGeneracionDocumentos.Repository.Context;
using ApiGeneracionDocumentos.Repository.Services;
using Microsoft.EntityFrameworkCore;
using VacancyAnnouncements.Infraestructure.Cypher;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAnfInfraestructure, AnfInfraestructure>();
builder.Services.AddScoped<IAnfRepository, AnfRepository>();
builder.Services.AddScoped<ITramiteRepository, TramiteRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IDocumentoRepository, DocumentoRepository>();
builder.Services.AddScoped<ILogGeneracionDocumentoRepository, LogGeneracionDocumentoRepository>();
builder.Services.AddScoped<IPictoreRepository, PictoreRepository>();
builder.Services.AddScoped<IConfiguracionRepository, ConfiguracionRepository>();
builder.Services.AddScoped<IVersionDocumentoFlujoWebRepository, VersionDocumentoFlujoWebRepository>();
builder.Services.AddScoped<IVersionDetalleDocumentoFlujoWebRepository, VersionDetalleDocumentoFlujoWebRepository>();
builder.Services.AddScoped<ICampaniaRepository, CampaniaRepository>();
builder.Services.AddScoped<IReconocimientoFacialRepository, ReconocimientoFacialRepository>();

builder.Services.AddScoped<ITramiteInfraestructure, TramiteInfraestructure>();
builder.Services.AddScoped<IClienteInfraestructure, ClienteInfraestructure>();
builder.Services.AddScoped<IDocumentoInfraestructure, DocumentoInfraestructure>();
builder.Services.AddScoped<ILogGeneracionDocumentoInfraestructure, LogGeneracionDocumentoInfraestructure>();
builder.Services.AddScoped<IPictoreInfraestructure, PictoreInfraestructure>();
builder.Services.AddScoped<IConfiguracionInfraestructure, ConfiguracionInfraestructure>();
builder.Services.AddScoped<ICampaniaInfraestructure, CampaniaInfraestructure>();
builder.Services.AddScoped<IReconocimientoFacialInfraestructure, ReconocimientoFacialInfraestructure>();

builder.Services.AddDbContextFactory<CreditoWebContext>(opt => opt.UseSqlServer(Encriptador.Desencriptar(builder.Configuration.GetConnectionString("CadenaConectividad")!)), ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<FormularioContext>(opt => opt.UseSqlServer(Encriptador.Desencriptar(builder.Configuration.GetConnectionString("CadenaConectividad")!).Replace("CreditoWeb", "Formulario")), ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<CreditoContext>(opt => opt.UseSqlServer(Encriptador.Desencriptar(builder.Configuration.GetConnectionString("CadenaConectividad")!).Replace("CreditoWeb", "Credito")), ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<SolicitudContext>(opt => opt.UseSqlServer(Encriptador.Desencriptar(builder.Configuration.GetConnectionString("CadenaConectividad")!).Replace("CreditoWeb", "Solicitud")), ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<ConfiguracionContext>(opt => opt.UseSqlServer(Encriptador.Desencriptar(builder.Configuration.GetConnectionString("CadenaConectividad")!).Replace("CreditoWeb", "Configuracion")), ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<ClienteContext>(opt => opt.UseSqlServer(Encriptador.Desencriptar(builder.Configuration.GetConnectionString("CadenaConectividad")!).Replace("CreditoWeb", "Cliente")), ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<PictoreContext>(opt => opt.UseSqlServer(Encriptador.Desencriptar(builder.Configuration.GetConnectionString("CadenaConectividad")!).Replace("CreditoWeb", "XtPictore")), ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<GestionContext>(opt => opt.UseSqlServer(Encriptador.Desencriptar(builder.Configuration.GetConnectionString("CadenaConectividad")!).Replace("CreditoWeb", "Gestion")), ServiceLifetime.Transient);
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
