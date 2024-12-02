using MediatR;
using ImpressioApi_.Application.Commands.Usuario.Write;
using ImpressioApi_.Application.Commands;
using System.Transactions;
using ImpressioApi_.Domain.Interfaces.Queries;

public class LoginUsuarioHandler : IRequestHandler<LoginUsuarioCommand, CommandResult>
{
    private readonly IObterUsuarioQuery _obterUsuarioQuery;
    private readonly TokenService _tokenService;

    public LoginUsuarioHandler(IObterUsuarioQuery obterUsuarioQuery, TokenService tokenService)
    {
        _obterUsuarioQuery = obterUsuarioQuery ?? throw new ArgumentNullException(nameof(obterUsuarioQuery));
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

            var usuario = await _obterUsuarioQuery.ObterPorEmail(request.EmailUsuario);
            if (usuario == null)
            {
                return result.AdicionarErro("Usuário não encontrado.");
            }

            if (string.IsNullOrEmpty(usuario.Senha))
            {
                return result.AdicionarErro("Senha não registrada para o usuário.");
            }

            if (!VerificarSenha(request.Senha, usuario.Senha))
            {
                return result.AdicionarErro("Senha incorreta.");
            }

            if (usuario.EmailUsuario == null)
            {
                return result.AdicionarErro("Usuário não encontrado.");
            }

            var token = _tokenService.GenerateToken(usuario.EmailUsuario);
            result.Sucesso("Login realizado com sucesso!");
            result.Token = token;

            result.Dados = new
            {
                usuario.IdUsuario,
                usuario.NomeUsuario,
                usuario.EmailUsuario
            };

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
