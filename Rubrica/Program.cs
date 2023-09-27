using System.Text.Json;


List<Contatto> rubrica = new();

using (StreamReader r = new("rubrica.json"))
{
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
    parametri = args;
    operazione = args[0];
}
else
{
    List<string> operazioni_disponibili = new() { "lista", "cerca", "nuovo", "cancella", };
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
            stampa(contatto);
        break;

    case "cerca":
        string? q = parametri.ElementAtOrDefault(1);
        if (q == null)
        {
            Console.Write("Cosa vuoi cercare? ");
            q = Console.ReadLine() ?? throw new OperationCanceledException();
        }
        foreach (var contatto in rubrica.Where(c => c.Nome.Contains(q) || c.Cognome.Contains(q) || c.Numero.Contains(q)))
            Console.WriteLine($"{contatto.Nome} {contatto.Cognome}: {contatto.Numero}");
        break;

    case "nuovo":
        string? nome = parametri.ElementAtOrDefault(1);
        if (nome == null)
        {
            Console.Write("Nome: ");
            nome = Console.ReadLine() ?? throw new OperationCanceledException();
        }

        string? cognome = parametri.ElementAtOrDefault(2);
        if (cognome == null)
        {
            Console.Write("Cognome: ");
            cognome = Console.ReadLine() ?? throw new OperationCanceledException();
        }

        string? numero = parametri.ElementAtOrDefault(3);
        if (numero == null)
        {
            Console.Write("Numero: ");
            numero = Console.ReadLine() ?? throw new OperationCanceledException();
        }

        rubrica.Add(new Contatto(nome, cognome, numero));
        aggiorna(rubrica);

        Console.WriteLine("Contatto aggiunto!");

        break;

    case "cancella":
        string? search = parametri.ElementAtOrDefault(1);
        if (search == null)
        {
            Console.Write("Chi vuoi cancellare? ");
            search = Console.ReadLine() ?? throw new OperationCanceledException();
        }
        IEnumerable<Contatto> delete_list = rubrica.Where(c => c.Nome.Contains(search) || c.Cognome.Contains(search) || c.Numero.Contains(search)).ToList();
        switch (delete_list.Count())
        {
            case 0:
                Console.WriteLine($"{search} non trovato.");
                break;
            case 1:
                Contatto delete = delete_list.First();
                rubrica.Remove(delete);
                Console.Write($"Cancellato ");
                stampa(delete);
                aggiorna(rubrica);
                break;
            default:
                Console.WriteLine("Nome ambiguo:");
                foreach (Contatto contatto in delete_list)
                    stampa(contatto);
                break;
        }
        break;


    default:
        Console.WriteLine("Comando non riconosciuto");
        break;

}

static void aggiorna(List<Contatto> rubrica)
{
    using (StreamWriter w = new("rubrica.json"))
    {
        foreach (Contatto contatto in rubrica)
        {
            w.WriteLine(JsonSerializer.Serialize<Contatto>(contatto));
        }
    }
}

static void stampa(Contatto contatto)
{
    Console.WriteLine($"{contatto.Nome} {contatto.Cognome}: {contatto.Numero}");
}