using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Repositories;
using Estacionamento.Service.Interfaces;
using Estacionamento.Service.Mappings;
using Estacionamento.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(EstacionamentoProfile));

            builder.Services.AddDbContext<EstacionamentoContext>((services, options) =>
            {
                var configuration = services.GetRequiredService<IConfiguration>();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            builder.Services.AddScoped<IClienteService, ClienteService>();

            builder.Services.AddScoped<IClienteVeiculoRepository, ClienteVeiculoRepository>();
            builder.Services.AddScoped<IClienteVeiculoService, ClienteVeiculoService>();

            builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            builder.Services.AddScoped<IEnderecoService, EnderecoService>();

            builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            builder.Services.AddScoped<IVeiculoService, VeiculoService>();

            builder.Services.AddScoped<IPermanenciaService, PermanenciaService>();
            builder.Services.AddScoped<IPermanenciaRepository, PermanenciaRepository>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
            {
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
