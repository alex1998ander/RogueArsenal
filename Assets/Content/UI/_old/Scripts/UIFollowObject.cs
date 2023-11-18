using UnityEngine;

public class UIFollowObject : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (objectToFollow != null)
            _rectTransform.anchoredPosition = objectToFollow.localPosition;
    }
}