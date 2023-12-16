using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] character;
    [SerializeField] private MainCharacterTipOver mainCharacter;
    [SerializeField] private CameraTransition camera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (var spriteRenderer in character)
            {
                StartCoroutine(FadeOut(spriteRenderer));
            }

            mainCharacter.GetComponent<TiltSprite>().enabled = false;

            mainCharacter.StartTippingOver();
            camera.StartOrbiting();
        }
    }

    private IEnumerator FadeOut(SpriteRenderer sr)
    {
        var alpha = 1f;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            yield return null;
        }
        
        sr.gameObject.SetActive(false);
    }
}