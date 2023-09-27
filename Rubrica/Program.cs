using System.Runtime.CompilerServices;
using System.Text.Json;


StreamReader r = new("rubrica.json");

List<Contatto> rubrica = new();

while (true)
{
    string? line = r.ReadLine();
    if (line == null)
        break;

    Contatto? contatto = JsonSerializer.Deserialize<Contatto>(line);
    if (contatto == null)
    {
        Console.WriteLine("Error");
        return;
    }

        rubrica.Add(contatto);
}


if (rubrica == null)
{
    Console.WriteLine("Error");
    return;
}
foreach (Contatto contatto in rubrica)
{
    Console.WriteLine($"{contatto.Nome} {contatto.Cognome}: {contatto.Numero}");
}

public class Contatto
{
    public string Nome { get; set; } = "";
    public string Cognome { get; set; } = "";
    public string Numero { get; set; } = "";
}