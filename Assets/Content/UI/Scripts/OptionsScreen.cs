using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Klasse fuer eine Aufl�sung von Horizontal- und Vertikal-Wert
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

    // Schalter f�r Vollbild und VSync
    [SerializeField] private Toggle fullscreenTog, vsyncTog;

    // Liste f�r die Aufl�sungen
    [SerializeField] private List<ResItem> resolutions = new List<ResItem>();

    // Text der ausgew�hlten Aufl�sung
    [SerializeField] private TMP_Text resolutionLabel;

    // Volume Slider in options screen
    [SerializeField] private Slider volumeSlider;

    // Ausgew�hlte Aufl�sung der Liste
    private int selectedResolution = 0;

    #endregion

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
        // schaut, ob die aktuelle Aufl�sung in der Liste ist
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;

                selectedResolution = i;

                UpdateResLabel();
            }
        }

        // Wenn aktuelle Aufl�sung nicht in der Liste, erstelle neue Aufl�sung in der Liste
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
        
        SetMasterVolume();
    }

    #region Button Functions

    /// <summary>
    /// Setzt die Aufl�sung auf die n�chst kleinere in der Liste
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
    /// Setzt dir Aufl�sung auf die n�chst gr��ere in der Liste
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
    /// Aktualisiert die Anzeige auf die aktuell Ausgew�hlte
    /// </summary>
    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " +
                               resolutions[selectedResolution].vertical.ToString();
    }

    /// <summary>
    /// Wendet die Einstellungen, welche der Nutzer aktuell ausgew�hlt hat, an.
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

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical,
            fullscreenTog.isOn);
    }

    #endregion

    #region Slider Functions

    public void SetMasterVolume()
    {
        AudioController.SetMasterVolume(volumeSlider.value);
    }

    #endregion
}