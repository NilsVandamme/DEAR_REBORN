using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manage the timbres window & enveloppes

public class SC_GM_Timbre : MonoBehaviour
{
    public List<GameObject> timbreText;
    public List<Image> timbre;
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
        foreach (GameObject elem in timbreText)
            texte.Add(elem.GetComponent<TextMeshProUGUI>());

        foreach (SC_Timbres timbres in SC_GM_Master.gm.timbres.timbres)
            for (int i = 0; i < timbre.Count; i++)
                if (timbres.getName() == timbre[i].sprite.name && timbres.IsVisible())
                {
                    timbre[i].gameObject.SetActive(true);
                    texte[i].gameObject.SetActive(true);
                    texte[i].text = timbres.getText();
                }
    }

    public void Affiche(SC_Timbres timbres)
    {
        for (int i = 0; i < timbre.Count; i++)
            if (timbres.getName() == timbre[i].sprite.name)
            {
                timbre[i].gameObject.SetActive(true);
                texte[i].gameObject.SetActive(true);
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
