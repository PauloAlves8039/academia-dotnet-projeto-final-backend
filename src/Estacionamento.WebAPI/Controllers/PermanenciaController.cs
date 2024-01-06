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
    public class PermanenciaController : ControllerBase
    {
        private readonly IPermanenciaService _permanenciaService;
        private readonly IMapper _mapper;

        public PermanenciaController(IPermanenciaService permanenciaService, IMapper mapper)
        {
            _permanenciaService = permanenciaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermanenciaDto>>> ObterTodasAsPermanencias()
        {
            try
            {
                var permanencias = await _permanenciaService.PesquisarListaDePermanencias();

                var permanenciasDto = _mapper.Map<List<PermanenciaDto>>(permanencias);

                return Ok(permanenciasDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter permanências: {ex.Message}");
            }
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<PermanenciaDto>> ObterPermanenciaPorCodigo(int codigo)
        {
            try
            {
                var permanencia = await _permanenciaService.PesquisarPermanenciaPorCodigo(codigo);

                if (permanencia == null)
                {
                    return NotFound($"Permanência com código {codigo} não encontrada");
                }

                var permanenciaDto = _mapper.Map<PermanenciaDto>(permanencia);

                return Ok(permanenciaDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter permanência: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PermanenciaDto>> AdicionarPermanencia([FromBody] PermanenciaDto permanenciaDto)
        {
            try
            {
                var novaPermanenciaDto = await _permanenciaService.AdicionarPermanencia(permanenciaDto);

                return CreatedAtAction(nameof(ObterPermanenciaPorCodigo), new { codigo = novaPermanenciaDto.CodigoPermanencia }, novaPermanenciaDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar permanência: {ex.Message}");
            }
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult<PermanenciaDto>> AtualizarPermanencia(int codigo, [FromBody] PermanenciaDto permanenciaDto)
        {
            try
            {
                if (codigo != permanenciaDto.CodigoPermanencia)
                {
                    return BadRequest($"O Código {codigo} da permanência na URL não corresponde!");
                }

                var permanenciaExistente = await _permanenciaService.PesquisarPermanenciaPorCodigo(codigo);

                if (permanenciaExistente == null)
                {
                    return NotFound($"Permanência com código {codigo} não encontrada");
                }

                _mapper.Map(permanenciaDto, permanenciaExistente);

                var permanenciaAtualizada = await _permanenciaService.AtualizarPermanencia(permanenciaDto);

                return Ok(permanenciaAtualizada);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar permanência: {ex.Message}");
            }
        }

        [HttpDelete("{codigo}")]
        public async Task<ActionResult> RemoverPermanenciaExistente(int codigo)
        {
            try
            {
                await _permanenciaService.RemoverPermanencia(codigo);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover permanência: {ex.Message}");
            }
        }
    }
}
