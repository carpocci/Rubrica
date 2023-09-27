using System.Text.Json;


StreamReader r = new("rubrica.json");
string? json = r.ReadLine();
if (json == null)
{
    Console.WriteLine("Error");
    return;
}

Contatto? contatto = JsonSerializer.Deserialize<Contatto>(json);

if (contatto == null)
{
    Console.WriteLine("Error");
    return;
}

Console.WriteLine($"{contatto.Nome} {contatto.Cognome}: {contatto.Numero}");

public class Contatto
{
    public string Nome { get; set; } = "";
    public string Cognome { get; set; } = "";
    public string Numero { get; set; } = "";
}