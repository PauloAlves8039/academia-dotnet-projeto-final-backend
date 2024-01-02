using AutoMapper;
using Estacionamento.Service.Dtos;
using Estacionamento.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.WebAPI.Controllers
{
    // [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;

        public EnderecoController(IEnderecoService enderecoService, IMapper mapper)
        {
            _enderecoService = enderecoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoDto>>> ObterTodosOsEnderecos()
        {
            try
            {
                var enderecos = await _enderecoService.PesquisarListaDeEnderecos();

                var enderecoDto = _mapper.Map<IEnumerable<EnderecoDto>>(enderecos);

                return Ok(enderecoDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter lista de endereços: {ex.Message}");
            }
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<EnderecoDto>> ObterEnderecoPeloCodigo(int codigo)
        {
            try
            {
                var endereco = await _enderecoService.PesquisarEnderecoPorCodigo(codigo);

                if (endereco == null)
                {
                    return NotFound($"Endereço com o código {codigo} não encontrado.");
                }

                var enderecoDto = _mapper.Map<EnderecoDto>(endereco);

                return Ok(enderecoDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter endereço por código: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EnderecoDto>> AdicionarNovoEndereco([FromBody] EnderecoDto enderecoDto)
        {
            try
            {
                var novoEnderecoDto = await _enderecoService.AdicionarEndereco(enderecoDto);

                return CreatedAtAction(nameof(ObterEnderecoPeloCodigo), new { codigo = novoEnderecoDto.CodigoEndereco }, novoEnderecoDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar endereço: {ex.Message}");
            }
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult<EnderecoDto>> AtualizarEnderecoExistente(int codigo, [FromBody] EnderecoDto enderecoDto)
        {
            try
            {
                if (codigo != enderecoDto.CodigoEndereco)
                {
                    return BadRequest($"O Código {codigo} do endereço na URL não corresponde!");
                }

                var enderecoExistente = await _enderecoService.PesquisarEnderecoPorCodigo(codigo);

                if (enderecoExistente == null)
                {
                    return NotFound($"Cliente com código {codigo} não encontrado");
                }

                _mapper.Map(enderecoDto, enderecoExistente);

                var enderecoAtualizado = await _enderecoService.AltualizarEndereco(enderecoDto);

                return Ok(enderecoAtualizado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar endereço: {ex.Message}");
            }
        }

        [HttpDelete("{codigo}")]
        public async Task<ActionResult> RemoverEndereçoExistente(int codigo)
        {
            try
            {
                await _enderecoService.RemoverEndereco(codigo);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover endereço: {ex.Message}");
            }
        }
    }
}
