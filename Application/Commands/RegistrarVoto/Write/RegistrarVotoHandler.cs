using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using MediatR;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Model;

namespace ImpressioApi_.Application.Commands.RegistrarVoto.Handlers;

public class RegistrarVotoHandler : IRequestHandler<RegistrarVotoCommand, CommandResult>
{
    private readonly IMapper _mapper;
    private readonly IRegistroVotoRepository _registroVotoRepository;
    private readonly IObterRegistroVotoQuery _registroVotoQuery;
    private readonly IObraArteRepository _obraArteRepository;
    private readonly IObterUsuarioQuery _obterUsuarioQuery;

    public RegistrarVotoHandler(IMapper mapper, IRegistroVotoRepository registroVotoRepository, IObterRegistroVotoQuery registroVotoQuery, IObraArteRepository obraArteRepository, IObterUsuarioQuery obterUsuarioQuery)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _registroVotoRepository = registroVotoRepository ?? throw new ArgumentNullException(nameof(registroVotoRepository));
        _registroVotoQuery = registroVotoQuery ?? throw new ArgumentNullException(nameof(registroVotoQuery));
        _obraArteRepository = obraArteRepository ?? throw new ArgumentNullException(nameof(obraArteRepository));
        _obterUsuarioQuery = obterUsuarioQuery ?? throw new ArgumentNullException(nameof(obterUsuarioQuery));
    }

    public async Task<CommandResult> Handle(RegistrarVotoCommand request, CancellationToken cancellationToken)
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

            var usuario = await _obterUsuarioQuery.ObterUsuarioById(request.IdUsuario);
            if (usuario == null)
            {
                return result.AdicionarErro("Usuário não encontrado.");
            }

            var obraArte = await _obraArteRepository.GetById(request.IdObraArte);
            if (obraArte == null)
            {
                return result.AdicionarErro("Obra de arte não encontrada.");
            }

            var votoExistente = await _registroVotoQuery.ObterRegistroVotoByObraArteEUsuario(request.IdObraArte, request.IdUsuario);
            if (votoExistente != null)
            {
                return result.AdicionarErro("O usuário já registrou um voto para esta obra.");
            }

            var novoVoto = new ObraVotoModel
            {
                IdUsuario = request.IdUsuario,
                IdObraArte = request.IdObraArte,
                Voto = (int)request.Voto,
            };

            _registroVotoRepository.Add(novoVoto);

            var sucesso = await _registroVotoRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                return result.AdicionarErro("Falha ao registrar o voto.");
            }

            result.Sucesso("Voto registrado com sucesso!");
            scope.Complete();
            return result;
        }
        catch (Exception ex)
        {
            return result.AdicionarErro($"Erro ao registrar o voto: {ex.Message}");
        }
    }
}