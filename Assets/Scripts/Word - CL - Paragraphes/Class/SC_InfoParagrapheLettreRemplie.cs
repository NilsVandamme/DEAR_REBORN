
[System.Serializable]
public class SC_InfoParagrapheLettreRemplie
{
    public SC_Word word;
    public float scoreParagraphe;
    public string textParagraphe;

    public SC_InfoParagrapheLettreRemplie (SC_Word word, float scoreParagraphe, string textParagraphe)
    {
        this.word = word;
        this.scoreParagraphe = scoreParagraphe;
        this.textParagraphe = textParagraphe;
    }
}
