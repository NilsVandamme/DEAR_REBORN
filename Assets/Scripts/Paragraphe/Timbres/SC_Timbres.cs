using UnityEngine;

[System.Serializable]
public class SC_Timbres
{
    private string name;
    private Texture2D timbre;
    private bool visible;

    public SC_Timbres(string id, Texture2D image, bool visible)
    {
        this.name = id;
        this.timbre = image;
        this.visible = visible;
    }

    public string getName()
    {
        return this.name;
    }

    public Texture2D getImage()
    {
        return this.timbre;
    }

    public bool IsVisible()
    {
        return this.visible;
    }

    public void setName(string id)
    {
        this.name = id;
    }

    public void setImage(Texture2D image)
    {
        this.timbre = image;
    }

    public void setVisible(bool visible)
    {
        this.visible = visible;
    }
}
