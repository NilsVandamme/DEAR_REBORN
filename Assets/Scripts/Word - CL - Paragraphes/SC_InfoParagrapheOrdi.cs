using TMPro;
using UnityEngine;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ListChampLexicaux listCL;
    public SimpleTooltip tooltip;
    public Color CLRecoltColor;
    public Color infoRecoltColor;
    public Color textNonRecoltableColor;
    public float wait;

    public GameObject fxGood;
    public GameObject fxbad;

    public bool affichable = true;
    public bool recoltable;

    private TextMeshProUGUI text;
    private Color normalTextColor;
    private bool firstClick;

    [HideInInspector]
    public int cl;
    [HideInInspector]
    public int word;
    [HideInInspector]
    public int choix;
    [HideInInspector]
    public string textInfos;

    private void Start()
    {
        text = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        firstClick = true;
        tooltip.infoLeft = listCL.listNameChampLexical[cl];

        ReplaceName();
    }

    private void ReplaceName()
    {
        int startIndex = text.text.IndexOf("$$$$$$$");

        if (startIndex < 0) return;

        for (int i = startIndex; i < text.text.Length; i++)
            if (text.text[i].Equals(' '))
            {
                text.text = text.text.Substring(0, startIndex) + " " +
                            SC_GM_Master.gm.namePlayer + " " +
                            text.text.Substring(i, (text.text.Length - i));
            }


    }

    /*
     * Au click sur un paragraphe
     */
    public void OnClick()
    {
        if (firstClick)
        {
            firstClick = false;

            if (recoltable)
                OnClickRecolt();
            else
                OnClickNonRecoltable();
        }
    }


    /*
     * Si rien a recolter
     */
    private void OnClickNonRecoltable()
    {
        text.color = textNonRecoltableColor;

        Instantiate(fxbad, new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -4f), Quaternion.identity);
        SC_GM_SoundManager.instance.PlaySound("ClickPhraseFail_2");

        //TO-DO --> feedback de refus de collect
    }

    /*
     * Si on recolt
     */
    private void OnClickRecolt()
    {
        if (choix == 0) // CL;
            OnClickCLRecolt();
        else
            OnClickInfoRecolt();
    }


    /*
     * Si on recolt un CL
     */
    private void OnClickCLRecolt()
    {
        if (SC_GM_Local.gm.numberOfCLRecover < SC_GM_Local.gm.numberOfCLRecoverable)
        {
            SC_GM_Local.gm.wordsInCollect.Add(new SC_CLInPull(listCL.listNameChampLexical[cl], listCL.listChampLexical[cl].listOfWords[word]));
            SC_GM_Local.gm.numberOfCLRecover++;
            text.color = CLRecoltColor;

            SC_PoolFeedback.instance.LaunchFeedbackForCL();
            Instantiate(fxGood, new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -4f), Quaternion.identity);
            SC_GM_SoundManager.instance.PlaySound("WordGet");

        }
    }

    /*
     * Si on recolt des infos
     */
    private void OnClickInfoRecolt()
    {
        SC_GM_Master.gm.infoPerso[SC_GM_Local.gm.persoOfCurrentScene].Add(textInfos);
        text.color = infoRecoltColor;

        // TO-DO --> feedback
    }

    /*
     * Gere quand on entre en hover du text
     */
    public void TextHover()
    {
        if (firstClick)
            SC_GM_Cursor.gm.changeToHoverCursor();
    }

    /*
     * Gere quand on sort du hover du text
     */
    public void TextHoverExit()
    {
        SC_GM_Cursor.gm.changeToNormalCursor();
    }

    /*
     * Get la position de la souris sur le canvas
     */
    public Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePoint) * -15.545f;
    }
}
