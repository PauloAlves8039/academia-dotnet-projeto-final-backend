using AutoMapper;
using Estacionamento.Service.Dtos;
using Estacionamento.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteVeiculoController : ControllerBase
    {
        private readonly IClienteVeiculoService _clienteVeiculoService;
        private readonly IMapper _mapper;

        public ClienteVeiculoController(IClienteVeiculoService clienteVeiculoService, IMapper mapper)
        {
            _clienteVeiculoService = clienteVeiculoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteVeiculoDto>>> ObterItensClientesVeiculos()
        {
            try
            {
                var clientesVeiculos = await _clienteVeiculoService.PesquisarItensClienteVeiculo();

                var clienteVeiculoDto = _mapper.Map<IEnumerable<ClienteVeiculoDto>>(clientesVeiculos);

                return Ok(clienteVeiculoDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter lista de itens clientes e veículos: {ex.Message}");
            }
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<ClienteVeiculoDto>> ObterClienteVeiculoPeloCodigo(int codigo)
        {
            try
            {
                var clienteVeiculo = await _clienteVeiculoService.PesquisarClienteVeiculoPorCodigo(codigo);

                if (clienteVeiculo == null)
                {
                    return NotFound($"Cliente veículo com o código {codigo} não encontrado.");
                }

                var clienteVeiculoDto = _mapper.Map<ClienteVeiculoDto>(clienteVeiculo);

                return Ok(clienteVeiculoDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter cliente veículo por código: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClienteVeiculoDto>> AdicionarNovoCliente([FromBody] ClienteVeiculoDto clienteVeiculoDto)
        {
            try
            {
                var novoClienteVeiculoDto = await _clienteVeiculoService.AdicionarClienteVeiculo(clienteVeiculoDto);

                return CreatedAtAction(nameof(ObterClienteVeiculoPeloCodigo), new { codigo = novoClienteVeiculoDto.CodigoClienteVeiculo }, novoClienteVeiculoDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar cliente veículo: {ex.Message}");
            }
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult<ClienteDto>> AtualizarClienteVeiculoExistente(int codigo, [FromBody] ClienteVeiculoDto clienteVeiculoDto)
        {
            try
            {
                if (codigo != clienteVeiculoDto.CodigoClienteVeiculo)
                {
                    return BadRequest($"O Código {codigo} do cliente e veículo associados na URL não corresponde!");
                }

                var clienteVeiculoExistente = await _clienteVeiculoService.PesquisarClienteVeiculoPorCodigo(codigo);

                if (clienteVeiculoExistente == null)
                {
                    return NotFound($"Cliente e veículo associados com código {codigo} não encontrado");
                }

                _mapper.Map(clienteVeiculoDto, clienteVeiculoExistente);

                var clienteVeiculoAtualizado = await _clienteVeiculoService.AtualizarClienteVeiculo(clienteVeiculoDto);

                return Ok(clienteVeiculoAtualizado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar cliente e veículo associados: {ex.Message}");
            }
        }

        [HttpDelete("{codigo}")]
        public async Task<ActionResult> RemoverClienteVeiculoExistente(int codigo)
        {
            try
            {
                await _clienteVeiculoService.RemoverClienteVeiculo(codigo);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover cliente e veículo associados: {ex.Message}");
            }
        }
    }
}
