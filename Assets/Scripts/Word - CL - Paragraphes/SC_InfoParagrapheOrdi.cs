using TMPro;
using UnityEngine;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ListChampLexicaux listCL;
    public SimpleTooltip tooltip;
    public Color CLRecoltColor;
    public Color textNonRecoltableColor;
    public float wait;
    public bool recoltable;

    private TextMeshProUGUI text;
    private Color normalTextColor;
    private bool firstClick;

    public GameObject fxGood;
    public GameObject fxbad;

    [HideInInspector]
    public int cl;
    [HideInInspector]
    public int word;

    private void Start()
    {
        text = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        firstClick = true;
        tooltip.infoLeft = listCL.listNameChampLexical[cl];
    }

    public void OnClick()
    {
        if (recoltable)
            OnClickCLRecolt();
        else
            OnClickNonRecoltable();
    }


    private void OnClickNonRecoltable()
    {
        if (firstClick)
        {
            firstClick = false;
            text.color = textNonRecoltableColor;

            Instantiate(fxbad, new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -4f), Quaternion.identity);
            SC_GM_SoundManager.instance.PlaySound("ClickPhraseFail_2");

            //TO-DO --> feedback de refus de collect
        }
    }


    private void OnClickCLRecolt()
    {
        if (firstClick)
        {
            //firstClick = false;

            if (SC_GM_Local.gm.numberOfCLRecover < SC_GM_Local.gm.numberOfCLRecoverable)
            {
                SC_GM_Local.gm.wordsInCollect.Add(new SC_CLInPull(listCL.listNameChampLexical[cl], listCL.listChampLexical[cl].listOfWords[word]));
                SC_GM_Local.gm.numberOfCLRecover++;
                text.color = CLRecoltColor;

                SC_CollectedCLFeedbackUI.instance.text.text = listCL.listChampLexical[cl].listOfWords[word].titre;
                SC_CollectedCLFeedbackUI.instance.StartFeedback();
                Instantiate(fxGood, new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -4f), Quaternion.identity);
                SC_GM_SoundManager.instance.PlaySound("WordGet");

            }
        }
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
