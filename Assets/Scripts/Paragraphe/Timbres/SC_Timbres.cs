using UnityEngine;

public class SC_Timbres
{
    private int id;
    private Texture2D timbre;
    private bool visible;

    public SC_Timbres(int id, Texture2D image, bool visible)
    {
        this.id = id;
        this.timbre = image;
        this.visible = visible;
    }

    public int getID()
    {
        return this.id;
    }

    public Texture2D getImage()
    {
        return this.timbre;
    }

    public bool IsVisible()
    {
        return this.visible;
    }

    public void setID(int id)
    {
        this.id = id;
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
