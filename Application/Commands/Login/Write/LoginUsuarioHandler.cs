using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using ImpressioApi_.Application.Commands;
using ImpressioApi_.Application.Commands.Usuario.Write;
using ImpressioApi_.Domain.Interfaces.Repositories;
using MediatR;
using BCrypt.Net; // Certifique-se de ter a biblioteca BCrypt.Net instalada

public class LoginUsuarioHandler : IRequestHandler<LoginUsuarioCommand, CommandResult>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly TokenService _tokenService;

    public LoginUsuarioHandler(IUsuarioRepository usuarioRepository, TokenService tokenService)
    {
        _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<CommandResult> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = new CommandResult();

        try
        {
            var valida = await request.Valida();
            if (!valida)
            {
                return result.AdicionarErros(request.ObterErros());
            }

            var usuario = await _usuarioRepository.ObterPorEmail(request.EmailUsuario);
            if (usuario == null)
            {
                return result.AdicionarErro("Usuário não encontrado.");
            }

            if (!VerificarSenha(request.Senha, usuario.Senha))
            {
                return result.AdicionarErro("Senha incorreta.");
            }

            var token = _tokenService.GenerateToken(usuario.Senha ?? usuario.EmailUsuario);
            result.Sucesso("Login realizado com sucesso!");
            result.Token = token;

            return result;
        }
        finally
        {
            if (result.Success)
            {
                scope.Complete();
            }
        }
    }

    private bool VerificarSenha(string senha, string hashArmazenado)
    {
        return BCrypt.Net.BCrypt.Verify(senha, hashArmazenado);
    }
}
