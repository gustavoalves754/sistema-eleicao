using SistemaEleicao.Console.Domain;

namespace SistemaEleicao.Console.Services;

public sealed class EleicaoManager
{
    private readonly Dictionary<int, Candidato> _candidatos = [];
    private readonly Dictionary<string, Eleitor> _eleitores =
        new(StringComparer.OrdinalIgnoreCase);

    public int TotalVotosBrancos { get; private set; }

    public int TotalVotosNulos { get; private set; }

    public Candidato CadastrarCandidato(string? nome, int numero)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome do candidato é obrigatório.", nameof(nome));
        }

        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero), "O número do candidato deve ser positivo.");
        }

        if (_candidatos.ContainsKey(numero))
        {
            throw new InvalidOperationException(
                $"Já existe um candidato cadastrado com o número {numero}.");
        }

        var candidato = new Candidato(nome.Trim(), numero);
        _candidatos.Add(numero, candidato);
        return candidato;
    }

    public IReadOnlyCollection<Candidato> ListarCandidatos()
    {
        return _candidatos.Values.ToArray();
    }

    public Candidato? BuscarCandidato(int numero)
    {
        return _candidatos.GetValueOrDefault(numero);
    }

    public Eleitor RegistrarEleitor(string? identificador)
    {
        if (string.IsNullOrWhiteSpace(identificador))
        {
            throw new ArgumentException("O título ou CPF do eleitor é obrigatório.", nameof(identificador));
        }

        var identificadorNormalizado = identificador.Trim();

        if (_eleitores.ContainsKey(identificadorNormalizado))
        {
            throw new InvalidOperationException("Este eleitor já está cadastrado.");
        }

        var eleitor = new Eleitor(identificadorNormalizado);
        _eleitores.Add(identificadorNormalizado, eleitor);
        return eleitor;
    }

    public Eleitor? BuscarEleitor(string identificador)
    {
        return _eleitores.GetValueOrDefault(identificador.Trim());
    }

    public TipoVoto RegistrarVoto(string identificadorEleitor, int? numeroCandidato)
    {
        var eleitor = BuscarEleitor(identificadorEleitor)
            ?? throw new KeyNotFoundException("Eleitor não cadastrado.");

        if (eleitor.JaVotou)
        {
            throw new InvalidOperationException("Este eleitor já votou.");
        }

        var tipoVoto = ContabilizarVoto(numeroCandidato);
        eleitor.ConfirmarVoto();
        return tipoVoto;
    }

    private TipoVoto ContabilizarVoto(int? numeroCandidato)
    {
        if (numeroCandidato is null)
        {
            TotalVotosBrancos++;
            return TipoVoto.Branco;
        }

        var candidato = BuscarCandidato(numeroCandidato.Value);

        if (candidato is null)
        {
            TotalVotosNulos++;
            return TipoVoto.Nulo;
        }

        candidato.ReceberVoto();
        return TipoVoto.Nominal;
    }
}
