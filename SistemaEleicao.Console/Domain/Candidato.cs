namespace SistemaEleicao.Console.Domain;

public sealed class Candidato
{
    public Candidato(string nome, int numero)
    {
        Nome = nome;
        Numero = numero;
    }

    public string Nome { get; }

    public int Numero { get; }

    public int TotalVotos { get; private set; }

    internal void ReceberVoto()
    {
        TotalVotos++;
    }
}
