using System.Collections;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] private Transform cameraTargetPosition;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve easeCurvePosition;
    [SerializeField] private AnimationCurve easeCurveRotation;
    
    public void StartOrbiting()
    {
        StartCoroutine(OrbitCameraToTopDown());
    }

    private IEnumerator OrbitCameraToTopDown()
    {
        Quaternion targetRotation = Quaternion.AngleAxis(90, Vector3.right);
        var startPosition= transform.position;
        var startRotation= transform.rotation;
        float elapsedTime = 0;
        
        while (elapsedTime < 1f)
        {
            elapsedTime += 1f / duration * Time.deltaTime;
            
            var easedPositionValue = easeCurvePosition.Evaluate(elapsedTime);
            var easedRotationValue = easeCurveRotation.Evaluate(elapsedTime);
            
            transform.position = Vector3.LerpUnclamped(startPosition, cameraTargetPosition.transform.position, easedPositionValue);
            transform.rotation = Quaternion.LerpUnclamped(startRotation, targetRotation, easedRotationValue);
            yield return null;
            
        }

        transform.position = cameraTargetPosition.position;
        transform.rotation = targetRotation;
    }
}