using UnityEngine;

[System.Serializable]
public class SC_Timbres
{
    public string name;
    public bool visible;
    public string text;

    public SC_Timbres(string id, bool visible, string text)
    {
        this.name = id;
        this.visible = visible;
        this.text = text;
    }

    public string getName()
    {
        return this.name;
    }

    public bool IsVisible()
    {
        return this.visible;
    }

    public string getText()
    {
        return this.text;
    }

    public void setVisible(bool visible)
    {
        this.visible = visible;
    }
}
