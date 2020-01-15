public class SC_ClickObject
{
    private int index;
    private int posStartText;
    private SC_Word mot;

    public SC_ClickObject(int pos, SC_Word mot, int id)
    {
        this.posStartText = pos;
        this.mot = mot;
        this.index = id;
    }

    public int getPosStartText()
    {
        return this.posStartText;
    }

    public SC_Word getMot()
    {
        return this.mot;
    }

    public int getIndex()
    {
        return this.index;
    }
}
