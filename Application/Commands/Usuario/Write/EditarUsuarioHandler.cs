using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Write;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Interfaces.Repositories;
using ImpressioApi_.Domain.Model;
using MediatR;

namespace ImpressioApi_.Application.Commands.Usuario.Write;

public class EditarUsuarioHandler: IRequestHandler<EditarUsuarioCommand, CommandResult<EditarUsuarioRespostaDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IObterUsuarioQuery _obterUsuarioQuery;
    private EditarUsuarioCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult<EditarUsuarioRespostaDTO> _result = null!;

    public EditarUsuarioHandler(IMapper mapper, IUsuarioRepository usuarioRepository, IObterUsuarioQuery obterUsuarioQuery)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        _obterUsuarioQuery = obterUsuarioQuery ?? throw new ArgumentNullException(nameof(obterUsuarioQuery));
    }

    public async Task<CommandResult<EditarUsuarioRespostaDTO>> Handle(EditarUsuarioCommand request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 2, 0), TransactionScopeAsyncFlowOption.Enabled);
        
        try
        {
            _request = request;
            var valida = await _request.Valida();
            _cancellationToken = cancellationToken;
            _result = new CommandResult<EditarUsuarioRespostaDTO>();

            if (!valida)
            {
                return _result.AdicionarErros(_request.ObterErros());
            }

            var usuario = await _obterUsuarioQuery.ObterUsuarioById(_request.IdUsuario);

            if (usuario == null)
            {
                return _result.AdicionarErro("Usuário não encontrado.");
            }

            var usuarioModel = _mapper.Map<UsuarioModel>(usuario);

            usuarioModel.NomeUsuario = _request.NomeUsuario ?? usuarioModel.NomeUsuario;
            usuarioModel.EmailUsuario = _request.EmailUsuario ?? usuarioModel.EmailUsuario;
            usuarioModel.Apelido = _request.Apelido ?? usuarioModel.Apelido;
            usuarioModel.BiografiaUsuario = _request.BiografiaUsuario ?? usuarioModel.BiografiaUsuario;
            usuarioModel.ImagemUsuario = _request.ImagemUsuario ?? usuarioModel.ImagemUsuario;
            
            if (!string.IsNullOrWhiteSpace(_request.Senha))
            {
                usuarioModel.Senha = _request.Senha;
            }

            if (_request.Publico.HasValue)
            {
                usuarioModel.Publico = _request.Publico.Value;
            }

            _usuarioRepository.Update(usuarioModel);
            var sucesso = await _usuarioRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                return _result.AdicionarErro("Falha ao atualizar usuário.");
            }
            return _result.Sucesso("Usuário atualizado com sucesso!");
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