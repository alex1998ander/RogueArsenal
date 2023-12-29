using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairCursor;

    private Vector2 crosshairCursorHotspot;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        crosshairCursorHotspot = new Vector2(crosshairCursor.width / 2, crosshairCursor.height / 2);

        EventManager.OnStartGame.Subscribe(_SetCrosshairCursor);
        EventManager.OnPauseGame.Subscribe(_SetDefaultCursor);
    }

    private void _SetCrosshairCursor()
    {
        Cursor.SetCursor(crosshairCursor, crosshairCursorHotspot, CursorMode.Auto);
    }

    private void _SetDefaultCursor(bool paused)
    {
        if (paused)
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        else
            _SetCrosshairCursor();
    }
}