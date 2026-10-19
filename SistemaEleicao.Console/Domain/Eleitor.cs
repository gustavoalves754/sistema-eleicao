namespace SistemaEleicao.Console.Domain;

public sealed class Eleitor
{
    public Eleitor(string identificador)
    {
        Identificador = identificador;
    }

    public string Identificador { get; }

    public bool JaVotou { get; private set; }

    internal void ConfirmarVoto()
    {
        JaVotou = true;
    }
}
