using AutoMapper;
using Estacionamento.Service.Dtos;
using Estacionamento.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IMapper _mapper;

        public VeiculoController(IVeiculoService veiculoService, IMapper mapper)
        {
            _veiculoService = veiculoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoDto>>> ObterTodosOsVeiculos()
        {
            try
            {
                var veiculos = await _veiculoService.PesquisarListaDeVeiculos();

                var veiculoDto = _mapper.Map<IEnumerable<VeiculoDto>>(veiculos);

                return Ok(veiculoDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter lista de veículos: {ex.Message}");
            }
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<VeiculoDto>> ObterVeiculoPeloCodigo(int codigo)
        {
            try
            {
                var veiculo = await _veiculoService.PesquisarVeiculoPorCodigo(codigo);

                if (veiculo == null)
                {
                    return NotFound($"Veículo com o código {codigo} não encontrado");
                }

                var veiculoDto = _mapper.Map<VeiculoDto>(veiculo);

                return Ok(veiculoDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter veículo por código: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<VeiculoDto>> AdicionarNovoVeiculo([FromBody] VeiculoDto veiculoDto)
        {
            try
            {
                var novoVeiculoDto = await _veiculoService.AdicionarVeiculo(veiculoDto);

                return CreatedAtAction(nameof(ObterVeiculoPeloCodigo), new { codigo = novoVeiculoDto.CodigoVeiculo }, novoVeiculoDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar veículo: {ex.Message}");
            }
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult<VeiculoDto>> AtualizarVeiculoExistente(int codigo, [FromBody] VeiculoDto veiculoDto)
        {
            try
            {
                if (codigo != veiculoDto.CodigoVeiculo)
                {
                    return BadRequest($"O Código {codigo} do veículo na URL não corresponde!");
                }

                var veiculoExistente = await _veiculoService.PesquisarVeiculoPorCodigo(codigo);

                if (veiculoExistente == null)
                {
                    return NotFound($"Veículo com código {codigo} não encontrado");
                }

                _mapper.Map(veiculoDto, veiculoExistente);

                var veiculoAtualizado = await _veiculoService.AtualizarVeiculo(veiculoDto);

                return Ok(veiculoAtualizado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar veículo: {ex.Message}");
            }
        }

        [HttpDelete("{codigo}")]
        [Authorize(Policy = "DeletePermission")]
        public async Task<ActionResult> RemoverVeiculoExistente(int codigo)
        {
            try
            {
                await _veiculoService.RemoverVeiculo(codigo);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover veículo: {ex.Message}");
            }
        }
    }
}
