using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using MediatR;
using ImpressioApi_.Domain.Model;

namespace ImpressioApi_.Application.Commands.Usuario.Write;

public class CadastrarUsuarioHandler : IRequestHandler<CadastrarUsuarioCommand, CommandResult>
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;
    CadastrarUsuarioCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult _result = null!;
    
    public CadastrarUsuarioHandler(IMapper mapper, IUsuarioRepository usuarioRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
    }

    public async Task<CommandResult> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
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
            
            var usuario = _mapper.Map<UsuarioModel>(_request);
            _usuarioRepository.Add(usuario);
            
            var sucesso = await _usuarioRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                return _result.AdicionarErro("Falha ao cadastrar usuário");
            }

            return _result.Sucesso("Usuário cadastrado com sucesso!");
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