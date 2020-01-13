using System.Collections.Generic;

[System.Serializable]
public class SC_CLInPull
{
    private string champLexical;
    private List<SC_Word> word;

    public SC_CLInPull(string cl, SC_Word word)
    {
        this.champLexical = cl;

        this.word = new List<SC_Word>();
        this.word.Add(word);
    }

    public string GetCL()
    {
        return this.champLexical;
    }

    public List<SC_Word> GetListWord()
    {
        return this.word;
    }
}
