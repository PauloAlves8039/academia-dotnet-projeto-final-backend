using AutoMapper;
using Estacionamento.Model.Models;
using Estacionamento.Service.Dtos;

namespace Estacionamento.Service.Mappings
{
    public class EstacionamentoProfile : Profile
    {
        public EstacionamentoProfile()
        {
            CreateMap<Endereco, EnderecoDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Veiculo, VeiculoDto>().ReverseMap();

            CreateMap<ClienteVeiculo, ClienteVeiculoDto>()
                .ForMember(dest => dest.CodigoClienteVeiculo, opt => opt.MapFrom(src => src.CodigoClienteVeiculo))
                 .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
                 .ForMember(dest => dest.VeiculoId, opt => opt.MapFrom(src => src.VeiculoId))
                 .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente))
                 .ForMember(dest => dest.Veiculo, opt => opt.MapFrom(src => src.Veiculo))
                 .ReverseMap();

            CreateMap<Permanencia, PermanenciaDto>()
                .ForMember(dest => dest.CodigoPermanencia, opt => opt.MapFrom(src => src.CodigoPermanencia))
                .ForMember(dest => dest.ClienteVeiculoId, opt => opt.MapFrom(src => src.ClienteVeiculoId))
                .ForMember(dest => dest.Placa, opt => opt.MapFrom(src => src.Placa))
                .ForMember(dest => dest.DataEntrada, opt => opt.MapFrom(src => src.DataEntrada))
                .ForMember(dest => dest.DataSaida, opt => opt.MapFrom(src => src.DataSaida))
                .ForMember(dest => dest.TaxaPorHora, opt => opt.MapFrom(src => src.TaxaPorHora))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal))
                .ForMember(dest => dest.EstadoPermanencia, opt => opt.MapFrom(src => src.EstadoPermanencia))
                .ReverseMap();
        }
    }
}
