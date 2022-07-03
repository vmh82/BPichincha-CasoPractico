using DebitoCredito.Application.Mapper;
using DebitoCredito.Application.Services;
using DebitoCredito.Application.Services.Interfaces;
using DebitoCredito.Domain.Repository;
using DebitoCredito.Infraestructure.Data;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddDbContext<DebitoCreditoDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DebitoCreditoDb");
    options.UseSqlServer(connectionString);

});
TypeAdapterConfig configMapper = MapperConfig.ConfigurarMapper();
builder.Services.AddSingleton(configMapper);
builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();
builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();

builder.Services.AddCors();
builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        opt.SerializerSettings.DateFormatString = "dd/MM/yyyy";
    });
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(builder => builder
                  .AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }