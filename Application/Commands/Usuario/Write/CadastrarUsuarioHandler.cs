using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using MediatR;
using ImpressioApi_.Domain.Model;
using System.Security.Cryptography;
using System.Text;

namespace ImpressioApi_.Application.Commands.Usuario.Write;

public class CadastrarUsuarioHandler : IRequestHandler<CadastrarUsuarioCommand, CommandResult>
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly TokenService _tokenService;
    CadastrarUsuarioCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult _result = null!;
    
    public CadastrarUsuarioHandler(IMapper mapper, IUsuarioRepository usuarioRepository, TokenService tokenService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
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
            
            var senhaCriptografada = CriptografarSenha(_request.Senha);
            var usuario = _mapper.Map<UsuarioModel>(_request);
            usuario.Senha = senhaCriptografada;
            
            _usuarioRepository.Add(usuario);
            
            var sucesso = await _usuarioRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                return _result.AdicionarErro("Falha ao cadastrar usuário");
            }

            var token = _tokenService.GenerateToken(usuario.Senha);
            _result.Sucesso("Usuário cadastrado com sucesso!");
            _result.Token = token;
            
            return _result;
        }
        finally
        {
            if (_result.Success)
            {
                scope.Complete();
            }
        }
    }

    private string CriptografarSenha(string senha)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}