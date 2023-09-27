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

if (args.Length < 1)
{
    Console.WriteLine("Nessun comando");
    return;
}

switch (args[0])
{
    case "lista":
        foreach (Contatto contatto in rubrica)
            Console.WriteLine($"{contatto.Nome} {contatto.Cognome}: {contatto.Numero}");
        break;

    case "cerca":
        if (args.Length < 2)
        {
            Console.WriteLine("Error: no query");
            return;
        }
        string q = args[1];
        foreach (var contatto in rubrica.Where(c => c.Nome.Contains(q) || c.Cognome.Contains(q) || c.Numero.Contains(q)))
            Console.WriteLine($"{contatto.Nome} {contatto.Cognome}: {contatto.Numero}");
        break;

    default:
        Console.WriteLine("Comando non riconosciuto");
        break;

}


public class Contatto
{
    public string Nome { get; set; } = "";
    public string Cognome { get; set; } = "";
    public string Numero { get; set; } = "";
}