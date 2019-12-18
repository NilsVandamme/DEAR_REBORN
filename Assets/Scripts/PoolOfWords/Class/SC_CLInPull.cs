using System.Collections.Generic;

public class SC_CLInPull
{
    private string champLexical;
    private List<SC_Word> word;
    private Dictionary<string,bool[]> usedPerPerso;

    public SC_CLInPull(string cl, SC_Word word, bool[] use)
    {
        this.champLexical = cl;

        this.word = new List<SC_Word>();
        this.word.Add(word);

        this.usedPerPerso = new Dictionary<string, bool[]>();
        this.usedPerPerso.Add(word.titre, use);
    }

    public string GetCL()
    {
        return this.champLexical;
    }

    public List<SC_Word> GetListWord()
    {
        return this.word;
    }

    public Dictionary<string, bool[]> GetUsed()
    {
        return this.usedPerPerso;
    }
}
