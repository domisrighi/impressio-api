using AutoMapper;
using ImpressioApi_.Application.Commands;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImpressioApi_.WebApi.Controller;

public class FavoritarObraArteController : ImpressioController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<UsuarioController> _logger;

    public FavoritarObraArteController(ILogger<UsuarioController> logger, IMapper mapper, IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    /// <summary>
    /// Favorita uma obra de arte.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpPost("FavoritarObraDeArte")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FavoritarObraDeArte(AdicionarObraArteFavoritaCommand command)
    {
        return Response(await _mediator.Send(command));
    }
}