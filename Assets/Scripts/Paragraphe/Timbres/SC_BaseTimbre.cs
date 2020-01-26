using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseTimbre.asset", menuName = "Custom/BaseTimbre", order = 1)]
public class SC_BaseTimbre : ScriptableObject
{
    public TextAsset fileCSVBaseTimbre;
    public List<SC_Timbres> timbres;
    public List<Sprite> images;
}