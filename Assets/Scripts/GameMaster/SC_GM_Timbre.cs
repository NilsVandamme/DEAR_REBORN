using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manage the timbres window & enveloppes

public class SC_GM_Timbre : MonoBehaviour
{
    public List<GameObject> stampDecs;
    public List<Image> timbreButton;
    public List<Image> stampImage;
    public List<SpriteRenderer> timbreEnvelope;

    private List<TextMeshProUGUI> texte;

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
        texte = new List<TextMeshProUGUI>();
        foreach (GameObject elem in stampDecs)
            texte.Add(elem.GetComponent<TextMeshProUGUI>());

        foreach (SC_Timbres timbres in SC_GM_Master.gm.timbres.timbres)
            for (int i = 0; i < stampImage.Count; i++)
                if (timbres.getName() == stampImage[i].sprite.name && timbres.IsVisible())
                {
                    timbreButton[i].gameObject.SetActive(true);
                    texte[i].text = timbres.getText();
                }
    }

    public void Affiche(SC_Timbres timbres)
    {
        for (int i = 0; i < stampImage.Count; i++)
            if (timbres.getName() == stampImage[i].sprite.name)
            {
                timbreButton[i].gameObject.SetActive(true);
                texte[i].text = timbres.getText();
            }
    }

    public void LoadTimbreEnvelope ()
    {
        foreach (SC_Timbres timbres in SC_GM_Master.gm.timbres.timbres)
            for (int i = 0; i < timbreEnvelope.Count; i++)
                if (timbres.getName() == timbreEnvelope[i].sprite.name && timbres.IsVisible())
                    timbreEnvelope[i].enabled = true;
    }
}
