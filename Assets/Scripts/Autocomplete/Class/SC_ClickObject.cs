public class SC_ClickObject
{
    private int posStartText;
    private int lenOldWorld;
    private SC_Word oldWord;
    private string newWord;

    public SC_ClickObject(int pos, SC_Word oldWord, string newWord, int lenOldWorld)
    {
        this.posStartText = pos;
        this.oldWord = oldWord;
        this.newWord = newWord;
        this.lenOldWorld = lenOldWorld;
    }

    public int getPosStartText()
    {
        return this.posStartText;
    }

    public string getNewMot()
    {
        return this.newWord;
    }

    public SC_Word getOldWord()
    {
        return this.oldWord;
    }

    public int getLenOldWord()
    {
        return this.lenOldWorld;
    }
}
