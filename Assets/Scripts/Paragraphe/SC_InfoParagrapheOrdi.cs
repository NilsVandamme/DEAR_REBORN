using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ParagrapheOrdi paragrapheOrdi;
    public Color CLRecoltColor;
    public Color timbresRecoltColor;
    public Color textNonRecoltableColor;
    public Camera cam;
    public float wait;

    private TextMeshProUGUI text;
    private Color normalTextColor;
    private bool firstClick;

    public GameObject fxGood;
    public GameObject fxbad;

    private void Start()
    {
        text = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        firstClick = true;
    }


    public void OnClickNonRecoltable()
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


    public void OnClickCLRecolt()
    {
        if (paragrapheOrdi == null)
        {
            Debug.LogError("Le paragraphe ordi : \n" + text.text + "\n n'a pas des CL assigné");
            return;
        }

        if (firstClick)
        {
            //firstClick = false;

            if (SC_GM_Local.gm.numberOfCLRecover < SC_GM_Local.gm.numberOfCLRecoverable)
            {
                List<int> elemToAdd = new List<int>();
                
                for (int i = 0; i < paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical].listOfWords.Count; i++)
                    if (paragrapheOrdi.motAccepterInCL[i])
                        elemToAdd.Add(i);

                AddWordInCollect(elemToAdd);
                SC_GM_Local.gm.numberOfCLRecover++;
                text.color = CLRecoltColor;

                //SC_CollectedCLFeedback.instance.text.text = paragrapheOrdi.listChampLexicaux.listNameChampLexical[paragrapheOrdi.champLexical];
                //SC_CollectedCLFeedback.instance.StartFeedback(SC_CollectedCLFeedback.instance.GetMouseWorldPos());

                SC_CollectedCLFeedbackUI.instance.text.text = paragrapheOrdi.listChampLexicaux.listNameChampLexical[paragrapheOrdi.champLexical];
                SC_CollectedCLFeedbackUI.instance.StartFeedback();

                Instantiate(fxGood, new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -4f), Quaternion.identity);
                SC_GM_SoundManager.instance.PlaySound("WordGet");

            }
        }
    }

    public void OnClickTimbreRecoltable(string timbre)
    {
        if (firstClick)
        {
            firstClick = false;

            for (int i = 0; i < SC_GM_Master.gm.timbres.timbres.Count; i++)
                if (SC_GM_Master.gm.timbres.timbres[i].getName() == timbre)
                {
                    SC_GM_Master.gm.timbres.timbres[i].setVisible(true);
                    text.color = timbresRecoltColor;

                    //SC_CollectedTimbresFeedback.instance.image.sprite = SC_GM_Master.gm.timbres.images[i];
                    //SC_CollectedTimbresFeedback.instance.StartFeedback(SC_CollectedTimbresFeedback.instance.GetMouseWorldPos());

                    SC_CollectedStampsFeedbackUI.instance.img.sprite = SC_GM_Master.gm.timbres.images[i];
                    SC_CollectedStampsFeedbackUI.instance.StartFeedback();

                    Instantiate(fxGood, new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -4f), Quaternion.identity);

                    SC_GM_SoundManager.instance.PlaySound("TimbreGet");

                }
        }
    }
    /*
     * Ajoute le CL a la collect
     */
    private void AddWordInCollect(List<int> elemToAdd)
    {
        string cl = paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical].fileCSVChampLexical.name;
        SC_CLInPull elem = new SC_CLInPull(cl, paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical].listOfWords[elemToAdd[0]]);

        for (int i = 1; i < elemToAdd.Count; i++)
            elem.word.Add(paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical].listOfWords[elemToAdd[i]]);

        SC_GM_Local.gm.wordsInCollect.Add(elem);

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
