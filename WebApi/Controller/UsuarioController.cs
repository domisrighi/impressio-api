using AutoMapper;
using ImpressioApi_.Application.Commands;
using ImpressioApi_.Application.Commands.Usuario.Read;
using ImpressioApi_.Application.Commands.Usuario.Write;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImpressioApi_.WebApi.Controller;

public class UsuarioController : ImpressioController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger, IMapper mapper, IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    /// <summary>
    /// Realiza o cadastro de um usuário.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpPost("CadastrarUsuario")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarUsuario(CadastrarUsuarioCommand command)
    {
        return Response(await _mediator.Send(command));
    }

    /// <summary>
    /// Obtém todos os usuários. (É possível filtrar conforme campos informados, caso queira todos os usuários basta não passar nenhum filtro)
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpGet("ObterUsuario")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult<PaginacaoResposta<ObterUsuarioRespostaDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterUsuario([FromQuery] ObterUsuarioCommand command)
    {
        return Response(await _mediator.Send(command));
    }

    ///  <summary>
    ///  Realiza a edição de um usuário.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique errors.</response>
    [HttpPut("EditarUsuario")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditarUsuario(EditarUsuarioCommand command)
    {
        return Response(await _mediator.Send(command));
    }

    /// <summary>
    /// Realiza a exclusão de um usuário.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="400">Erro tratado, verifique messages.</response>
    [HttpDelete("ExcluirUsuario")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExcluirUsuario(ExcluirUsuarioCommand command)
    {
        return Response(await _mediator.Send(command));
    }
}