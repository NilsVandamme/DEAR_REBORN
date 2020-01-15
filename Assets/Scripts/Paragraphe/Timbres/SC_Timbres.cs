using UnityEngine;

[System.Serializable]
public class SC_Timbres
{
    private string name;
    private Texture2D timbre;
    private bool visible;
    private string text;

    public SC_Timbres(string id, Texture2D image, bool visible, string text)
    {
        this.name = id;
        this.timbre = image;
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
