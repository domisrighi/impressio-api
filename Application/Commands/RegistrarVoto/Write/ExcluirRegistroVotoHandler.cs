using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using MediatR;

namespace ImpressioApi_.Application.Commands.RegistrarVoto.Write;

public class ExcluirRegistroVotoHandler : IRequestHandler<ExcluirRegistroVotoCommand, CommandResult>
{
    private readonly IRegistroVotoRepository _registroVotoRepository;
    private readonly IMapper _mapper;
    private ExcluirRegistroVotoCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult _result = null!;

    public ExcluirRegistroVotoHandler(IMapper mapper, IRegistroVotoRepository registroVotoRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _registroVotoRepository = registroVotoRepository ?? throw new ArgumentNullException(nameof(registroVotoRepository));
    }

    public async Task<CommandResult> Handle(ExcluirRegistroVotoCommand request, CancellationToken cancellationToken)
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

            var votoRegistrado = await _registroVotoRepository.GetById(_request.IdObraVoto);
            if (votoRegistrado is null)
            {
                return _result.AdicionarErro("Registro de voto não encontrado.");
            }

            _registroVotoRepository.Deletar(votoRegistrado);
            
            var sucessoAoExcluir = await _registroVotoRepository.UnitOfWork.Commit();
            if (!sucessoAoExcluir)
            {
                return _result.AdicionarErro("Falha ao excluir registro de voto.");
            }

            return _result.Sucesso("Sucesso ao excluir registro de voto!");
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