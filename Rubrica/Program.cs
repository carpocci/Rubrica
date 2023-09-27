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

string[]? parametri = null;
string operazione = "";

if (args.Length > 0)
{
    operazione = args[0];
}
else
{
    List<string> operazioni_disponibili = new() { "lista", "cerca", };
    Console.WriteLine($"""Operazioni disponibili: {String.Join(", ", operazioni_disponibili)}""");
    while (true)
    {
        Console.Write("Cosa vuoi fare? ");
        string? line = Console.ReadLine();
        if (line == null)
            return;
        parametri = line.Split();
        operazione = parametri[0];
        if (operazione == null)
        {
            return;
        }
        if (!operazioni_disponibili.Contains(operazione))
        {
            Console.WriteLine($"{operazione}: operazione non riconosciuta!");
            continue;
        }
        break;
    }
}

switch (operazione)
{
    case "lista":
        foreach (Contatto contatto in rubrica)
            Console.WriteLine($"{contatto.Nome} {contatto.Cognome}: {contatto.Numero}");
        break;

    case "cerca":
        if (parametri is null || parametri.Length < 2)
        {
            Console.WriteLine("Error: no query");
            return;
        }
        string q = parametri[1];
        foreach (var contatto in rubrica.Where(c => c.Nome.Contains(q) || c.Cognome.Contains(q) || c.Numero.Contains(q)))
            Console.WriteLine($"{contatto.Nome} {contatto.Cognome}: {contatto.Numero}");
        break;

    default:
        Console.WriteLine("Comando non riconosciuto");
        break;

}