using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using MediatR;
using ImpressioApi_.Domain.Model;
using ImpressioApi_.Application.Commands.ObraArte.Write;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.DTO.Queries;

namespace ImpressioApi_.Application.Commands.ObraArte.Handlers;

public class CadastrarObraArteHandler : IRequestHandler<CadastrarObraArteCommand, CommandResult>
{
    private readonly IMapper _mapper;
    private readonly IObraArteRepository _obraArteRepository;
    private readonly IObterUsuarioQuery _obterUsuarioQuery;
    private CadastrarObraArteCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult _result = null!;

    public CadastrarObraArteHandler(IMapper mapper, IObraArteRepository obraArteRepository, IObterUsuarioQuery obterUsuarioQuery)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _obraArteRepository = obraArteRepository ?? throw new ArgumentNullException(nameof(obraArteRepository));
        _obterUsuarioQuery = obterUsuarioQuery ?? throw new ArgumentNullException(nameof(obterUsuarioQuery));
    }

    public async Task<CommandResult> Handle(CadastrarObraArteCommand request, CancellationToken cancellationToken)
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

            var usuario = await _obterUsuarioQuery.ObterUsuarioById(request.IdUsuario);

            if (usuario == null)
            {
                return _result.AdicionarErro("Usuário não encontrado.");
            }

            var obraArte = _mapper.Map<ObraArteModel>(request);

            obraArte.ImagemObraArte = request.ImagemObraArte;
            obraArte.DescricaoObraArte = request.DescricaoObraArte;
            obraArte.Publico = request.Publico;
            obraArte.IdUsuario = request.IdUsuario;

            _obraArteRepository.Add(obraArte);

            var sucesso = await _obraArteRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                return _result.AdicionarErro("Falha ao cadastrar a obra de arte");
            }

            _result.Sucesso("Obra de arte cadastrada com sucesso!");
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
}
