public class SC_ClickObject
{
    private string id;
    private int posStartText;
    private string mot;

    public SC_ClickObject(int pos, string mot, string id)
    {
        this.posStartText = pos;
        this.mot = mot;
    }

    public int getPosStartText()
    {
        return this.posStartText;
    }

    public string getMot()
    {
        return this.mot;
    }

    public string getId()
    {
        return this.id;
    }

    public void setPosStartText(int val)
    {
        this.posStartText = val;
    }

    public void setMot(string val)
    {
        this.mot = val;
    }

    public void setId (string id)
    {
        this.id = id;
    }
}
