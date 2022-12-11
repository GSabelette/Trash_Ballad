using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Fader : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float fadeDuration = 1;

    private Image image;
    private bool creditsShown = false;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (!creditsShown && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            creditsShown = true;
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeIn()
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            image.color = new Color(0, 0, 0, 1 - timer / fadeDuration);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeOut()
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            image.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return new WaitForEndOfFrame();
        }

        GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }
}
