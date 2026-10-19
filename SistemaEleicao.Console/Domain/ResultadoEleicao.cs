namespace SistemaEleicao.Console.Domain;

public sealed record ResultadoEleicao(
    IReadOnlyList<Candidato> Candidatos,
    int VotosBrancos,
    int VotosNulos,
    IReadOnlyList<Candidato> Vencedores)
{
    public int TotalVotos =>
        Candidatos.Sum(candidato => candidato.TotalVotos) + VotosBrancos + VotosNulos;
}
