using SistemaEleicao.Console.Domain;

namespace SistemaEleicao.Console.Services;

public sealed class EleicaoManager
{
    private readonly Dictionary<int, Candidato> _candidatos = [];

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
}
