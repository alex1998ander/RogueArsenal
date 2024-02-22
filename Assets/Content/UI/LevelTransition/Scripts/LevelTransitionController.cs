using System;
using System.Collections;
using UnityEngine;

public class LevelTransitionController : MonoBehaviour
{
    private const float MaxTime = 3f;
    
    [SerializeField] private AnimationCurve cameraMovementY;
    [SerializeField] private AnimationCurve cameraZoom;
    [SerializeField] private Animator topElevatorAnimator;
    [SerializeField] private Animator bottomElevatorAnimator;
    
    private Camera _camera;        
    private float _elapsedTime;
    private Vector3 _defaultPosition;
    private float _defaultZoom;
    private Sprite _defaultTopElevatorSprite;
    private Sprite _defaultBottomElevatorSprite;

    public static LevelTransitionController Singleton;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.enabled = false;
        _defaultPosition = transform.position;
        _defaultZoom = _camera.orthographicSize;
        Singleton = this;
    }

    public void TriggerLevelTransition(Action loadLevel)
    {
        _camera.enabled = true;
        StartCoroutine(MoveCamera(loadLevel));
    }

    private IEnumerator MoveCamera(Action loadLevel)
    {
        var position = transform.position;
        _elapsedTime = 0f;
        
        topElevatorAnimator.Play("Open", -1,0f);
        bottomElevatorAnimator.Play("Close", -1,0f);
        
        StartCoroutine(PlayAnimations(loadLevel));
        
        while (_elapsedTime <= MaxTime)
        {
            _elapsedTime += Time.unscaledDeltaTime;

            var currentTime = _elapsedTime / MaxTime;
            position = new Vector3(position.x, cameraMovementY.Evaluate(currentTime), position.z);
            transform.position = position;

            _camera.orthographicSize = cameraZoom.Evaluate(currentTime);

            yield return null;
        }
        
        _camera.enabled = false;
        
        transform.position = _defaultPosition;
        _camera.orthographicSize = _defaultZoom;
    }

    private IEnumerator PlayAnimations(Action loadLevel)
    {
        while (_elapsedTime <= MaxTime / 2f)
        {
            yield return null;
        }
        
        loadLevel.Invoke();
    }
}
