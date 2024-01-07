using AutoMapper;
using Estacionamento.Service.Dtos;
using Estacionamento.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> ObterTodosOsClientes()
        {
            try
            {
                var clientes = await _clienteService.PesquisarListaDeClientes();

                var clienteDto = _mapper.Map<List<ClienteDto>>(clientes);

                return Ok(clienteDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter lista de clientes: {ex.Message}");
            }
        }

        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<ClienteDto>> ObterClientePeloCodigo(int codigo)
        {
            try
            {
                var cliente = await _clienteService.PesquisarClientePorCodigo(codigo);

                if (cliente == null)
                {
                    return NotFound($"Cliente com o código {codigo} não encontrado.");
                }

                var clienteDto = _mapper.Map<ClienteDto>(cliente);

                return Ok(clienteDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter cliente por código: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> AdicionarNovoCliente([FromBody] ClienteDto clienteDto)
        {
            try
            {
                var novoClienteDto = await _clienteService.AdicionarCliente(clienteDto);

                return CreatedAtAction(nameof(ObterClientePeloCodigo), new { codigo = novoClienteDto.CodigoCliente }, novoClienteDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar cliente: {ex.Message}");
            }
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult<ClienteDto>> AtualizarClienteExistente(int codigo, [FromBody] ClienteDto clienteDto)
        {
            try
            {
                if (codigo != clienteDto.CodigoCliente)
                {
                    return BadRequest($"O Código {codigo} do cliente na URL não corresponde!");
                }

                var clienteExistente = await _clienteService.PesquisarClientePorCodigo(codigo);

                if (clienteExistente == null)
                {
                    return NotFound($"Cliente com código {codigo} não encontrado");
                }

                _mapper.Map(clienteDto, clienteExistente);

                var clienteAtualizado = await _clienteService.AtualizarCliente(clienteDto);

                return Ok(clienteAtualizado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar cliente: {ex.Message}");
            }
        }

        [HttpDelete("{codigo}")]
        [Authorize(Policy = "DeletePermission")]
        public async Task<ActionResult> RemoverClienteExistente(int codigo)
        {
            try
            {
                await _clienteService.RemoverCliente(codigo);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover cliente: {ex.Message}");
            }
        }

    }
}
