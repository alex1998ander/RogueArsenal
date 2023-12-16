using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterTipOver : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve easeCurveRotationBodyAxis;
    [SerializeField] private AnimationCurve easeCurveRotationTipAxis;
    
    public void StartTippingOver()
    {
        StartCoroutine(TipOverCharacter());
    }

    private IEnumerator TipOverCharacter()
    {
        Quaternion targetRotationTipAxis = Quaternion.AngleAxis(90, Vector3.right);
        float bodyAxisRotationAngle = 720f;
        var startRotation= transform.localRotation; //TODO: fix ausgangswinkel beeinflusst, ob 1 oder 2 umdrehungen
        float elapsedTime = 0;

        yield return new WaitForSeconds(0.5f);
        
        while (elapsedTime < 1f)
        {
            elapsedTime += 2f / duration * Time.deltaTime;

            var easedValueBodyAxis = easeCurveRotationBodyAxis.Evaluate(elapsedTime);
            
            float yRotation = Mathf.Lerp(startRotation.eulerAngles.y, bodyAxisRotationAngle, easedValueBodyAxis) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
            
        }

        startRotation= transform.rotation;
        elapsedTime = 0;
        
        while (elapsedTime < 1f)
        {
            elapsedTime += 2f / duration * Time.deltaTime;

            var easedValueTipAxis = easeCurveRotationTipAxis.Evaluate(elapsedTime);
            
            transform.rotation = Quaternion.LerpUnclamped(startRotation, targetRotationTipAxis, easedValueTipAxis);
            yield return null;
            
        }
    }
}
