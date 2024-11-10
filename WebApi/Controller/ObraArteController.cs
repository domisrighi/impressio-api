using AutoMapper;
using ImpressioApi_.Application.Commands;
using ImpressioApi_.Application.Commands.ObraArte.Read;
using ImpressioApi_.Application.Commands.ObraArte.Write;
using ImpressioApi_.Application.Commands.Usuario.Read;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImpressioApi_.WebApi.Controller;

public class ObraArteController : ImpressioController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<UsuarioController> _logger;

    public ObraArteController(ILogger<UsuarioController> logger, IMapper mapper, IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    /// <summary>
    /// Realiza o cadastro de uma obra de arte.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpPost("CadastrarObraArte")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarObraDeArte(CadastrarObraArteCommand command)
    {
        return Response(await _mediator.Send(command));
    }

    /// <summary>
    /// Obtém todos as obras de arte. (É possível filtrar conforme campos informados, caso queira todas as obras basta não passar nenhum filtro)
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpGet("ObterObrasDeArte")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult<PaginacaoResposta<ObterObraArteRespostaDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterObraArte([FromQuery] ObterObraArteCommand command)
    {
        return Response(await _mediator.Send(command));
    }

    /// <summary>
    /// Obtém uma obra de arte. (Buscar enviando o id da obra de arte)
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpGet("ObterObrasDeArteById")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult<PaginacaoResposta<ObterObraArteRespostaDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterObraArteById([FromQuery] ObterObraArteByIdCommand command)
    {
        return Response(await _mediator.Send(command));
    }
}