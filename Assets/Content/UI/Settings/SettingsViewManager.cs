using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingsViewManager : MonoBehaviour
{
    [SerializeField] private ToggleView fullscreenToggle;
    [SerializeField] private ToggleView vSyncToggle;
    [SerializeField] private SelectionView resolutionSelection;
    [SerializeField] private StringButtonView cancelButton;
    [SerializeField] private StringButtonView applyButton;
    
    private List<Resolution> _resolutions;

    private void Start()
    {
        fullscreenToggle.Initialize(null, Screen.fullScreen);
        vSyncToggle.Initialize(null, QualitySettings.vSyncCount != 0);
        _resolutions = new List<Resolution>(Screen.resolutions);
        
        var foundRes = false;
        var selectedResolution = 0;
        
        for (var i = 0; i < _resolutions.Count; i++)
        {
            if (Screen.width == _resolutions[i].width && Screen.height == _resolutions[i].height)
            {
                foundRes = true;
                selectedResolution = i;
                
                break;
            }
        }

        if (!foundRes)
        {
            _resolutions.Add(Screen.currentResolution);
            _resolutions.Sort((x, y) => x.width.CompareTo(y.width));

            selectedResolution = _resolutions.IndexOf(Screen.currentResolution);
        }
        
        resolutionSelection.Initialize(_resolutions.Select(res => $"{res.width} x {res.height}").ToList(), selectedResolution, null);
        
        cancelButton.Initialize(() => LevelManager.ShowSettingsMenu(false));
        applyButton.Initialize(() =>
        {
            ApplyGraphics();
            LevelManager.ShowSettingsMenu(false);
        });
    }
    
    public void ApplyGraphics()
    {
        QualitySettings.vSyncCount = vSyncToggle.Enabled ? 1 : 0;

        var res = _resolutions[resolutionSelection.SelectedOptionIndex];
        
        Screen.SetResolution(res.width, res.height, fullscreenToggle.Enabled, res.refreshRate);
    }
}