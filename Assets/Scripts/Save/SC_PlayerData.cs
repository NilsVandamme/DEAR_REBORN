using System;
using System.Collections.Generic;

[Serializable]
public class SC_PlayerData
{
    public string namePlayer;
    public List<SC_CLInPull> wordsInPull;
    public List<SC_InfoParagrapheLettreRemplie>[] infoParagrapheLettre;
    public List<string>[] infoPerso;

    public SC_PlayerData(string namePlayer, List<SC_CLInPull> wordsInPull, List<SC_InfoParagrapheLettreRemplie>[] infoParagrapheLettre, List<string>[] infoPerso)
    {
        this.namePlayer = namePlayer;
        this.wordsInPull = wordsInPull;
        this.infoParagrapheLettre = infoParagrapheLettre;
        this.infoPerso = infoPerso;
    }
}
