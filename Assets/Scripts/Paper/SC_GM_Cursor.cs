using UnityEngine;

// Manage the cursor and its image

public class SC_GM_Cursor : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D loadCursor;
    public Texture2D hoverCursor;
    public Texture2D validationCursor;

    public static SC_GM_Cursor gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);
    }
    void Start()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void changeToNormalCursor()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void changeToLoadCursor()
    {
        Cursor.SetCursor(loadCursor, Vector2.zero, CursorMode.Auto);
    }

    public void changeToHoverCursor()
    {
        Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    public void changeToValidationCursor()
    {
        Cursor.SetCursor(validationCursor, Vector2.zero, CursorMode.Auto);
    }
}
