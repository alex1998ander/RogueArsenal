using System.Collections;
using UnityEngine;

public class SandboxDetailPanelIconView : DetailPanelIconView
{
    public bool active;
    private SandboxDetailPanelIconGridView _manager;

    private void Awake()
    {
        upgradeIcon.alphaHitTestMinimumThreshold = 0.1f;
    }

    public void Initialize(Sprite sprite, string name, UpgradePanelView upgradePanelDetailsView, UpgradeIdentification upgradeIdentification, bool active, SandboxDetailPanelIconGridView manager)
    {
        base.Initialize(sprite, name, upgradePanelDetailsView, upgradeIdentification);
        _manager = manager;
        this.active = active;

        if (this.active)
        {
            SetIconEnabled(true);
        }
        else
        {
            SetIconDisabled(true);
        }
    }

    public void OnUpgradeHoverEnter()
    {
        _upgradePanelDetailsView.InitializeUpgradePanelView(UpgradeManager.GetUpgradeFromIdentifier(_upgradeIdentification));
        SetIconHover();
    }

    public void OnUpgradeHoverExit()
    {
        if (active)
        {
            SetIconEnabled();
        }
        else
        {
            SetIconDisabled();
        }
    }

    public void OnUpgradeClick()
    {
        active = !active;

        if (active)
        {
            SetIconEnabled();
            _manager.ActivateUpgrade(_upgradeIdentification);
        }
        else
        {
            SetIconDisabled();
            _manager.DeactivateUpgrade(_upgradeIdentification);
        }
    }
    

    public void SetIconEnabled(bool instant = false)
    {
        if (instant)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            upgradeIcon.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            StartCoroutine(Fade(1f, Vector4.one, 0.1f));
        }
    }

    public void SetIconDisabled(bool instant = false)
    {
        if (instant)
        {
            transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
            upgradeIcon.color = new Color(0.8f, 0.7f, 0.7f, 0.5f);
        }
        else
        {
            StartCoroutine(Fade(0.85f, new Vector4(0.8f, 0.7f, 0.7f, 0.5f), 0.1f));
        }
        
    }

    private void SetIconHover()
    {
        upgradeIcon.color = new Color(1f, 1f, 1f, 0.7f);
    }

    private IEnumerator Fade(float targetScale, Vector4 targetColor, float duration)
    {
        var time = 0f;
        var initialScale = transform.localScale.x;
        var initialColor = new Vector4(upgradeIcon.color.r, upgradeIcon.color.g, upgradeIcon.color.b, upgradeIcon.color.a);

        while (time < duration)
        {
            var scale = Mathf.Lerp(initialScale, targetScale, time / duration);
            var color = Vector4.Lerp(initialColor, targetColor, time / duration);

            transform.localScale = new Vector3(scale, scale, scale);
            upgradeIcon.color = new Color(color.x, color.y, color.z, color.w);

            time += Time.deltaTime;
            yield return null;
        }
    }
}