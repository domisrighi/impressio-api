using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using MediatR;
using ImpressioApi_.Domain.Model;
using ImpressioApi_.Domain.Interfaces.Queries;

namespace ImpressioApi_.Application.Commands.ObraArteFavorita.Handlers;

public class AdicionarObraArteFavoritaHandler : IRequestHandler<AdicionarObraArteFavoritaCommand, CommandResult>
{
    private readonly IObterUsuarioQuery _obterUsuarioQuery;
    private readonly IObraArteRepository _obraArteRepository;
    private readonly IObraArteFavoritaRepository _obraArteFavoritaRepository;
    private readonly IObterObraArteFavoritaQuery _obterObraArteFavoritaQuery;
    private readonly IMapper _mapper;
    private AdicionarObraArteFavoritaCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult _result = null!;

    public AdicionarObraArteFavoritaHandler(IMapper mapper, IObraArteRepository obraArteRepository, IObterUsuarioQuery obterUsuarioQuery, IObraArteFavoritaRepository obraArteFavoritaRepository, IObterObraArteFavoritaQuery obterObraArteFavoritaQuery)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _obraArteRepository = obraArteRepository ?? throw new ArgumentNullException(nameof(obraArteRepository));
        _obterUsuarioQuery = obterUsuarioQuery ?? throw new ArgumentNullException(nameof(obterUsuarioQuery));
        _obraArteFavoritaRepository = obraArteFavoritaRepository ?? throw new ArgumentNullException(nameof(obraArteFavoritaRepository));
        _obterObraArteFavoritaQuery = obterObraArteFavoritaQuery ?? throw new ArgumentNullException(nameof(obterObraArteFavoritaQuery));
    }

    public async Task<CommandResult> Handle(AdicionarObraArteFavoritaCommand request, CancellationToken cancellationToken)
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

            var obraArte = await _obraArteRepository.GetById(request.IdObraArte);
            if (obraArte == null)
            {
                return _result.AdicionarErro("Obra de arte não encontrada.");
            }

            var obraArteExiste = await _obterObraArteFavoritaQuery.ObterObraArteFavoritaByUsuarioEObraArte(request.IdObraArte, request.IdUsuario);
            if (obraArteExiste != null)
            {
                return _result.AdicionarErro("Obra de arte já foi favoritada.");
            }

            var obraArteFavoritada = _mapper.Map<ObraFavoritadaModel>(request);

            obraArteFavoritada.IdObraArte = request.IdObraArte;
            obraArteFavoritada.IdUsuario = request.IdUsuario;

            _obraArteFavoritaRepository.Add(obraArteFavoritada);

            var sucesso = await _obraArteRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                return _result.AdicionarErro("Falha ao favoritar obra de arte.");
            }

            _result.Sucesso("Obra de arte favoritada com sucesso!");
            
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
