using System;
using System.Collections.Generic;

[Serializable]
public class SC_PlayerData
{
    public List<SC_CLInPull> wordsInPull;
    public List<SC_Timbres> timbre;

    public SC_PlayerData(List<SC_CLInPull> wordsInPull, List<SC_Timbres> timbre)
    {
        this.wordsInPull = wordsInPull;
        this.timbre = timbre;
    }
}
