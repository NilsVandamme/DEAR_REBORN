using System;
using System.Collections.Generic;

[Serializable]
public class SC_PlayerData
{
    public string namePlayer;
    public List<SC_CLInPull> wordsInPull;
    public List<SC_InfoParagrapheLettre> infoParagrapheLettre;
    public List<SC_InfoPerso> infoPerso;

    public SC_PlayerData(string namePlayer, List<SC_CLInPull> wordsInPull, SC_InfoParagrapheLettreRemplie[][] infoParagrapheLettre, List<string>[] infoPerso)
    {
        this.namePlayer = namePlayer;
        this.wordsInPull = wordsInPull;

        this.infoParagrapheLettre = new List<SC_InfoParagrapheLettre>();
        foreach (SC_InfoParagrapheLettreRemplie[] elem in infoParagrapheLettre)
            this.infoParagrapheLettre.Add(new SC_InfoParagrapheLettre(elem));

        this.infoPerso = new List<SC_InfoPerso>();
        foreach (List<string> elem in infoPerso)
            this.infoPerso.Add(new SC_InfoPerso(elem));
    }

    [Serializable]
    public class SC_InfoParagrapheLettre
    {
        public SC_InfoParagrapheLettreRemplie[] lettre;

        public SC_InfoParagrapheLettre(SC_InfoParagrapheLettreRemplie[] lettre)
        {
            this.lettre = lettre;
        }
    }

    [Serializable]
    public class SC_InfoPerso
    {
        public List<string> infoPerso;

        public SC_InfoPerso(List<string> infoPerso)
        {
            this.infoPerso = infoPerso;
        }
    }
}
