using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[DisallowMultipleComponent]
public class RectTransformSizePreserveImageAspectRatio : UIBehaviour, ILayoutSelfController
{
    private RectTransform m_Rect;
    
    private RectTransform rectTransform
    {
        get
        {
            if (m_Rect == null)
                m_Rect = GetComponent<RectTransform>();
            return m_Rect;
        }
    }
    
    private float m_AspectRatio;

    private float aspectRatio
    {
        get
        {
            if (m_AspectRatio == 0f)
            {
                var image = GetComponent<Image>();

                var width = image.sprite.rect.width;
                var height = image.sprite.rect.height;

                m_AspectRatio = width / height;
            }

            return m_AspectRatio;
        }
    }
    
    void PreserveObjectAspectRatio()
    {

        var height = rectTransform.rect.height;
        rectTransform.sizeDelta = new Vector2(height * aspectRatio, rectTransform.sizeDelta.y);
    }

    // This "delayed" mechanism is required for case 1014834.
    private bool m_DelayedSetDirty = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        
        SetDirty();
    }

    protected override void OnDisable()
    {
        LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        base.OnDisable();
    }

    protected override void OnTransformParentChanged()
    {
        base.OnTransformParentChanged();
        SetDirty();
    }

    /// <summary>
    /// Update the rect based on the delayed dirty.
    /// Got around issue of calling onValidate from OnEnable function.
    /// </summary>
    protected virtual void Update()
    {
        if (m_DelayedSetDirty)
        {
            m_DelayedSetDirty = false;
            SetDirty();
        }
    }

    /// <summary>
    /// Function called when this RectTransform or parent RectTransform has changed dimensions.
    /// </summary>
    protected override void OnRectTransformDimensionsChange()
    {
        PreserveObjectAspectRatio();
    }

    /// <summary>
    /// Method called by the layout system. Has no effect
    /// </summary>
    public virtual void SetLayoutHorizontal() { }

    /// <summary>
    /// Method called by the layout system. Has no effect
    /// </summary>
    public virtual void SetLayoutVertical() { }

    /// <summary>
    /// Mark the AspectRatioFitter as dirty.
    /// </summary>
    protected void SetDirty()
    {
        PreserveObjectAspectRatio();
    }


#if UNITY_EDITOR
    protected override void OnValidate()
    {
        m_DelayedSetDirty = true;
    }
#endif
}