using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_GM_Timbre : MonoBehaviour
{
    public List<Image> timbre;

    public static SC_GM_Timbre gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (SC_Timbres timbres in SC_GM_Master.gm.timbres.timbres)
            foreach (Image elem in timbre)
                if (timbres.getName() == elem.sprite.name && timbres.IsVisible())
                    elem.gameObject.SetActive(true);
    }

    public void Affiche(SC_Timbres timbres)
    {
        foreach (Image elem in timbre)
            if (timbres.getName() == elem.sprite.name)
                elem.gameObject.SetActive(true);
    }
}
