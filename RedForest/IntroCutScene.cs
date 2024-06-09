using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public AudioSource scarySoundtrack; // Reference to the AudioSource component holding the scary soundtrack

    public float displayTime = 2.0f;
    public float fadeDuration = 1.0f;
    public float sceneTransitionDelay = 10.0f;

    private void Start()
    {
        // Hide text1, text2, and text3 at the start
        text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, 0);
        text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, 0);
        text3.color = new Color(text3.color.r, text3.color.g, text3.color.b, 0);

        // Start playing the scary soundtrack
        scarySoundtrack.Play();

        // Start the cutscene coroutine
        StartCoroutine(PlayCutscene());

        // Start the coroutine for scene transition
        StartCoroutine(TransitionToNextScene());
    }

    private IEnumerator PlayCutscene()
    {
        yield return StartCoroutine(ShowText(text1));
        yield return StartCoroutine(ShowText(text2));
        yield return StartCoroutine(ShowText(text3));
    }

    private IEnumerator ShowText(TMP_Text text)
    {
        // Fade in
        yield return StartCoroutine(FadeTextToFullAlpha(text));

        // Display the text for the specified duration
        yield return new WaitForSeconds(displayTime);

        // Fade out
        yield return StartCoroutine(FadeTextToZeroAlpha(text));
    }

    private IEnumerator FadeTextToFullAlpha(TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / fadeDuration));
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1); // Ensure fully opaque at the end
    }

    private IEnumerator FadeTextToZeroAlpha(TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / fadeDuration));
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0); // Ensure fully transparent at the end
    }

    private IEnumerator TransitionToNextScene()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(sceneTransitionDelay);

        // Load the next scene
        SceneManager.LoadScene(2);
    }
}
