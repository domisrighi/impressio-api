using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using MediatR;

namespace ImpressioApi_.Application.Commands.ObraArteFavorita.Write;

public class ExcluirObraArteFavoritaHandler : IRequestHandler<ExcluirObraArteFavoritaCommand, CommandResult>
{
    private readonly IObraArteFavoritaRepository _obraArteFavoritaRepository;
    private readonly IMapper _mapper;
    private ExcluirObraArteFavoritaCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult _result = null!;

    public ExcluirObraArteFavoritaHandler(IMapper mapper, IObraArteFavoritaRepository obraArteFavoritaRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _obraArteFavoritaRepository = obraArteFavoritaRepository ?? throw new ArgumentNullException(nameof(obraArteFavoritaRepository));
    }

    public async Task<CommandResult> Handle(ExcluirObraArteFavoritaCommand request, CancellationToken cancellationToken)
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

            var obraArteFavoritada = await _obraArteFavoritaRepository.GetById(_request.IdObraFavorita);
            if (obraArteFavoritada is null)
            {
                return _result.AdicionarErro("Obra de arte favoritada não encontrada.");
            }

            _obraArteFavoritaRepository.Deletar(obraArteFavoritada);
            
            var sucessoAoExcluir = await _obraArteFavoritaRepository.UnitOfWork.Commit();
            if (!sucessoAoExcluir)
            {
                return _result.AdicionarErro("Falha ao excluir obra de arte.");
            }

            return _result.Sucesso("Sucesso ao excluir obra de arte!");
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