using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairCursor;
    [SerializeField] private Texture2D[] reloadCursor;

    private Vector2 crosshairCursorHotspot;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        crosshairCursorHotspot = new Vector2(crosshairCursor.width / 2, crosshairCursor.height / 2);

        EventManager.OnLevelEnter.Subscribe(SetCrosshairCursor);
        EventManager.OnPauseGame.Subscribe(OnPauseGame);
        EventManager.OnWeaponReloadStart.Subscribe(OnWeaponReload);
        EventManager.OnMainMenuEnter.Subscribe(SetDefaultCursor);
    }

    private void SetCrosshairCursor()
    {
        Cursor.SetCursor(crosshairCursor, crosshairCursorHotspot, CursorMode.Auto);
    }

    private void SetDefaultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    
    private void OnPauseGame(bool paused)
    {
        if (paused)
            SetDefaultCursor();
        else
            SetCrosshairCursor();
    }
    
    private void OnWeaponReload()
    {
        StartCoroutine(ReloadCursor());
    }

    private IEnumerator ReloadCursor()
    {
        var elapsedTime = 0f;
        var reloadTime = PlayerData.reloadTime;

        var reloadCursorTextureLength = reloadCursor.Length;
        
        while (elapsedTime < reloadTime)
        {
            elapsedTime += Time.unscaledDeltaTime;

            var index = Mathf.Min((int)(elapsedTime / reloadTime * reloadCursorTextureLength), reloadCursorTextureLength - 1);
            
            Cursor.SetCursor(reloadCursor[index], crosshairCursorHotspot, CursorMode.Auto);

            yield return null;
        }
        
        SetCrosshairCursor();
    }
}