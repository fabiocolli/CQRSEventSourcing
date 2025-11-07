using Aplicacao.ManipuladoresDeComandos;
using Aplicacao.Servicos;
using Dominio.Interfaces;
using InfraEstrutura.ContextoContexto;
using InfraEstrutura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Contexto>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios e serviços
builder.Services.AddScoped<IPais, PaisRepositorio>();
builder.Services.AddScoped<IEventStore, EventStoreRepositorio>();

// Command handler (pode receber via DI)
builder.Services.AddScoped<PaisManipuladorDeComando>();

builder.Services.AddScoped<EventStoreServico>();// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(sg =>
{
	sg.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRSEventSourcing", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.DefaultModelsExpandDepth(0);
		c.DocExpansion(DocExpansion.None);
	});
}

app.UseAuthorization();

app.MapControllers();

app.Run();
