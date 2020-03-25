using System;
using System.Collections.Generic;

[Serializable]
public class SC_PlayerData
{
    public string namePlayer;
    public List<SC_CLInPull> wordsInPull;
    public List<SC_InfoParagrapheLettre> infoParagrapheLettre;
    public List<SC_InfoPerso> infoPerso;

    public SC_PlayerData(string namePlayer, List<SC_CLInPull> wordsInPull, SC_InfoParagrapheLettreRemplie[][] infoParagrapheLettre,
                            List<string>[] infoRecoltPerso, string[] infoPerso, string[] descriptionPerso)
    {
        this.namePlayer = namePlayer;
        this.wordsInPull = wordsInPull;

        this.infoParagrapheLettre = new List<SC_InfoParagrapheLettre>();
        foreach (SC_InfoParagrapheLettreRemplie[] elem in infoParagrapheLettre)
            this.infoParagrapheLettre.Add(new SC_InfoParagrapheLettre(elem));

        this.infoPerso = new List<SC_InfoPerso>();
        for (int i = 0; i < infoRecoltPerso.Length; i++)
        {
            this.infoPerso.Add(new SC_InfoPerso(infoRecoltPerso[i], infoPerso[i], descriptionPerso[i]));
        }
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
        public string descPerso;
        public string infoPerso;
        public List<string> infoRecoltPerso;

        public SC_InfoPerso(List<string> infoRecoltPerso, string infoPerso, string descriptionPerso)
        {
            this.infoPerso = infoPerso;
            this.infoRecoltPerso = infoRecoltPerso;
            this.descPerso = descriptionPerso;
        }
    }
}
