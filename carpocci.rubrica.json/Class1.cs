using System.Text.Json;

namespace carpocci.rubrica.json;

public class Class1
{
    public static void Aggiorna(List<Contatto> rubrica)
    {
        using (StreamWriter w = new("rubrica.json"))
            foreach (Contatto contatto in rubrica)
                w.WriteLine(JsonSerializer.Serialize(contatto));
    }

    public static string Format(Contatto contatto) => $"{contatto.Nome} {contatto.Cognome}: {contatto.Numero}";

    public static string Lista(List<Contatto> rubrica)
    {
        return string.Join("\n", rubrica.Select(c => Format(c)));
    }

    public static IEnumerable<Contatto> Cerca(List<Contatto> rubrica, string q)
    {
        return rubrica.Where(c => c.Nome.Contains(q) || c.Cognome.Contains(q) || c.Numero.Contains(q));
    }

    public static void Aggiungi(List<Contatto> rubrica, Contatto nuovo)
    {
        rubrica.Add(nuovo);
        Aggiorna(rubrica);
    }
    public static void Cancella(List<Contatto> rubrica, Contatto vecchio)
    {
        rubrica.Remove(vecchio);
        Aggiorna(rubrica);
    }
}