using OficinaSystem.Domain.Interfaces;
using OficinaSystem.Domain.Services;
using OficinaSystem.Domain.Services.Interfaces;
using OficinaSystema.Infra;
using OficinaSystema.Infra.Repositories;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

Func<IServiceProvider, SqlConnection> Connect =
       a => new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Oficina;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
builder.Services.AddScoped(Connect);
builder.Services.AddScoped(typeof(IConnection), typeof(Connection));
// Adicionar Repositorio
builder.Services.AddScoped(typeof(IProdutoRepositorie), typeof(ProdutoRepositorie));
//Retirar e Ler o Erro depois -- NÃO ESQUECER!!!
builder.Services.AddScoped(typeof(IFuncionarioRepositorie), typeof(FuncionarioRepositorie));
builder.Services.AddScoped(typeof(IClienteRepositorie), typeof(ClienteRepositorie));
builder.Services.AddScoped(typeof(IServicoRepositorie), typeof(ServicoRepositorie));
builder.Services.AddScoped(typeof(IPedidoRepositorie), typeof(PedidoRepositorie));
builder.Services.AddScoped(typeof(IPedidoService), typeof(PedidoService));

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
