using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Klasse für eine Auflösung von Horizontal- und Vertikal-Wert
/// </summary>
[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}

/// <summary>
/// Steuert die Optionen vom Optionenbildschirm
/// </summary>
public class OptionsScreen : MonoBehaviour
{
    #region Variables

    // Schalter für Vollbild und VSync
    public Toggle fullscreenTog, vsyncTog;

    // Liste für die Auflösungen
    public List<ResItem> resolutions = new List<ResItem>();

    // Ausgewählte Auflösung der Liste
    private int selectedResolution = 0;

    // Text der ausgewählten Auflösung
    public TMP_Text resolutionLabel;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        bool foundRes = false;
        // schaut, ob die aktuelle Auflösung in der Liste ist
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;

                selectedResolution = i;

                UpdateResLabel();
            }
        }

        // Wenn aktuelle Auflösung nicht in der Liste, erstelle neue Auflösung in der Liste
        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            int i = 0;
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            resolutions.Sort((x, y) => x.horizontal.CompareTo(y.horizontal));

            while (!foundRes)
            {
                if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
                {
                    foundRes = true;

                    selectedResolution = i;

                    UpdateResLabel();
                }
                i++;
            }
        }

    }

    #region Button Functions

    /// <summary>
    /// Setzt die Auflösung auf die nächst kleinere in der Liste
    /// </summary>
    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdateResLabel();
    }

    /// <summary>
    /// Setzt dir Auflösung auf die nächst größere in der Liste
    /// </summary>
    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }

        UpdateResLabel();
    }

    /// <summary>
    /// Aktualisiert die Anzeige auf die aktuell Ausgewählte
    /// </summary>
    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }

    /// <summary>
    /// Wendet die Einstellungen, welche der Nutzer aktuell ausgewählt hat, an.
    /// </summary>
    public void ApplyGraphics()
    {
        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }

    #endregion
}
