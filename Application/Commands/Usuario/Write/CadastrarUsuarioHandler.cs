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
    private readonly TokenService _tokenService;

    public CadastrarUsuarioHandler(IMapper mapper, IUsuarioRepository usuarioRepository, TokenService tokenService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<CommandResult> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var result = new CommandResult();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            if (!await request.Valida())
            {
                return result.AdicionarErros(request.ObterErros());
            }

            var usuario = _mapper.Map<UsuarioModel>(request);
            if (usuario == null)
            {
                return result.AdicionarErro("Erro ao mapear os dados do usuário.");
            }

            usuario.Senha = CriptografarSenha(request.Senha);

            _usuarioRepository.Add(usuario);
            var sucesso = await _usuarioRepository.UnitOfWork.Commit();

            if (!sucesso)
            {
                return result.AdicionarErro("Falha ao salvar os dados no banco de dados.");
            }

            var token = _tokenService.GenerateToken(usuario.EmailUsuario);

            scope.Complete();

            return result.Sucesso("Usuário cadastrado com sucesso!").AdicionarToken(token);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao cadastrar usuário: {ex.Message}");
            return result.AdicionarErro("Erro interno ao cadastrar usuário.");
        }
    }

    private static string CriptografarSenha(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }
}