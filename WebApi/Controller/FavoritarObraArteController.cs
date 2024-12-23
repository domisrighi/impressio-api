using AutoMapper;
using ImpressioApi_.Application.Commands;
using ImpressioApi_.Application.Commands.ObraArteFavorita.Read;
using ImpressioApi_.Application.Commands.ObraArteFavorita.Write;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImpressioApi_.WebApi.Controller;

public class ObraArteFavoritaController : ImpressioController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<UsuarioController> _logger;

    public ObraArteFavoritaController(ILogger<UsuarioController> logger, IMapper mapper, IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    /// <summary>
    /// Adiciona uma obra de arte como favorita.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FavoritarObraDeArte(AdicionarObraArteFavoritaCommand command)
    {
        return Response(await _mediator.Send(command));
    }

    /// <summary>
    /// Remove uma obra de arte da lista de favoritas.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpDelete]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExcluirObraDeArte(ExcluirObraArteFavoritaCommand command)
    {
        return Response(await _mediator.Send(command));
    }

    /// <summary>
    /// Busca todas as obras de arte favoritas de um usuário.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult<PaginacaoResposta<ObterObraArteRespostaDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterObrasArteFavoritas([FromQuery] ObterObraArteFavoritaCommand command)
    {
        return Response(await _mediator.Send(command));
    }

    /// <summary>
    /// Busca uma obra de arte favoritada pelo ID do usuário e obra de arte.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpGet("GetByUsuarioEObra")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult<PaginacaoResposta<ObterObraArteRespostaDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterObrasArteFavoritasById([FromQuery] ObterObraArteFavoritaByIdCommand command)
    {
        return Response(await _mediator.Send(command));
    }
}