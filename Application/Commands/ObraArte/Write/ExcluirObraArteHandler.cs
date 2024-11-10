using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using MediatR;

namespace ImpressioApi_.Application.Commands.ObraArte.Write;

public class ExcluirObraArteHandler : IRequestHandler<ExcluirObraArteCommand, CommandResult>
{
    private readonly IMapper _mapper;
    private readonly IObraArteRepository _obraArteRepository;
    private ExcluirObraArteCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult _result = null!;

    public ExcluirObraArteHandler(IMapper mapper, IObraArteRepository obraArteRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _obraArteRepository = obraArteRepository ?? throw new ArgumentNullException(nameof(obraArteRepository));
    }

    public async Task<CommandResult> Handle(ExcluirObraArteCommand request, CancellationToken cancellationToken)
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

            var obraArte = await _obraArteRepository.GetById((int)_request.IdObraArte!);
            if (obraArte is null)
            {
                return _result.AdicionarErro("Obra de arte não encontrada.");
            }

            _obraArteRepository.Deletar(obraArte!);
            
            var sucessoAoExcluir = await _obraArteRepository.UnitOfWork.Commit();
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