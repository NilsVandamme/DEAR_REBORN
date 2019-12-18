[System.Serializable]
public class SC_Word
{
    public int[] scorePerso;
    public string titre;
    public string[] grammarCritere;

    public SC_Word (int[] score, string titre, string[] critere)
    {
        this.scorePerso = score;
        this.titre = titre;
        this.grammarCritere = critere;
    }
}
