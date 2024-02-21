using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Content.Core.ControlHintSystem
{
    public class ControlHintManager : MonoBehaviour
    {
        private const int ReminderDuration = 3;
        public const float FadeDuration = 0.5f;

        [SerializeField] public GameObject movementPrompt;
        [SerializeField] public GameObject shootPrompt;
        [SerializeField] public GameObject reloadPrompt;
        [SerializeField] public GameObject dashPrompt;
        [SerializeField] public GameObject abilityPrompt;

        private void Start()
        {
            ControlHintSystem.RegisterManager(this);

            movementPrompt.SetActive(false);
            shootPrompt.SetActive(false);
            reloadPrompt.SetActive(false);
            dashPrompt.SetActive(false);
            abilityPrompt.SetActive(false);
        }

        public void DisplayPrompt(GameObject prompt, float delay = 0f)
        {
            StartCoroutine(PromptCoroutine(prompt, delay));
        }

        public void ShowPrompt(GameObject prompt)
        {
            var image = prompt.GetComponent<Image>();
            StartCoroutine(ShowPrompt(image));
        }

        public void HidePrompt(GameObject prompt)
        {
            var image = prompt.GetComponent<Image>();
            StartCoroutine(HidePrompt(image));
        }

        private IEnumerator PromptCoroutine(GameObject prompt, float delay)
        {
            var image = prompt.GetComponent<Image>();

            yield return new WaitForSeconds(delay);


            StartCoroutine(ShowPrompt(image));

            yield return new WaitForSeconds(ReminderDuration + FadeDuration);

            StartCoroutine(HidePrompt(image));
        }

        private IEnumerator ShowPrompt(Image prompt)
        {
            var elapsedTime = 0f;
            prompt.gameObject.SetActive(true);

            while (elapsedTime < FadeDuration)
            {
                elapsedTime += Time.unscaledDeltaTime;

                var imageColor = prompt.color;
                imageColor.a = elapsedTime / FadeDuration;
                prompt.color = imageColor;

                yield return null;
            }
        }

        private IEnumerator HidePrompt(Image prompt)
        {
            var elapsedTime = 0f;

            while (elapsedTime < FadeDuration)
            {
                elapsedTime += Time.unscaledDeltaTime;

                var imageColor = prompt.color;
                imageColor.a = 1 - elapsedTime / FadeDuration;
                prompt.color = imageColor;

                yield return null;
            }

            prompt.gameObject.SetActive(false);
        }
    }
}