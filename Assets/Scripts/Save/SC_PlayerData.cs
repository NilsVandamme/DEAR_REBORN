using System;
using System.Collections.Generic;

[Serializable]
public class SC_PlayerData
{
    public string namePlayer;
    public List<SC_CLInPull> wordsInPull;

    public SC_PlayerData(string namePlayer, List<SC_CLInPull> wordsInPull)
    {
        this.namePlayer = namePlayer;
        this.wordsInPull = wordsInPull;
    }
}
