using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ValidationZone : MonoBehaviour
{
    public GameObject validationStamp;
    private bool done;

    private void OnMouseDown()
    {
        if (SC_ValidationStamp.instance.ValidationState && !done)
        {
            Debug.Log("stamp applied");

            // Do the stamp thing
            validationStamp.SetActive(true);
            validationStamp.transform.position = GetMouseWorldPos();
            StartCoroutine("LaunchSendAnim");
            SC_GM_Cursor.gm.changeToNormalCursor();
            done = true;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = 8.8f;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // <<<<<<<<<<<<<<<<<< ADD ANIM HERE  >>>>>>>>>>>>
    // LAUNCH THE SEND ANIM
    IEnumerator LaunchSendAnim()
    {
        yield return new WaitForSeconds(1);
        // Do whatever anim you want
    }
}
