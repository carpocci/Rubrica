public class Contatto
{
    public Contatto(string nome, string cognome, string numero)
    {
        Nome = nome;
        Cognome = cognome;
        Numero = numero;
    }

    public string Nome { get; set; } = "";
    public string Cognome { get; set; } = "";
    public string Numero { get; set; } = "";
}