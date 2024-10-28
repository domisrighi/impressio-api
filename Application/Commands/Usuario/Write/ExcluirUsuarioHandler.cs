using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using MediatR;

namespace ImpressioApi_.Application.Commands.Usuario.Write;

public class ExcluirUsuarioHandler : IRequestHandler<ExcluirUsuarioCommand, CommandResult>
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;
    private ExcluirUsuarioCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult _result = null!;

    public ExcluirUsuarioHandler(IMapper mapper, IUsuarioRepository usuarioRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
    }

    public async Task<CommandResult> Handle(ExcluirUsuarioCommand request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 2, 0), TransactionScopeAsyncFlowOption.Enabled);
        
        try
        {
            _request = request;
            var valida = await _request.Valida();
            _cancellationToken = cancellationToken;
            _result = new CommandResult();

            if (!valida)
            {
                return _result.AdicionarErros(_request.ObterErros());
            }

            var usuario = await _usuarioRepository.GetById((int)_request.IdUsuario!);
            if (usuario is null)
            {
                return _result.AdicionarErro("Usuário não encontrado.");
            }

            _usuarioRepository.Deletar(usuario!);
            
            var sucessoAoExcluir = await _usuarioRepository.UnitOfWork.Commit();
            if (!sucessoAoExcluir)
            {
                return _result.AdicionarErro("Falha ao excluir o usuário.");
            }

            return _result.Sucesso("Sucesso ao excluir o usuário!");
        }
        finally
        {
            if (_result.Success)
            {
                scope.Complete();
            }
        }
    }
}