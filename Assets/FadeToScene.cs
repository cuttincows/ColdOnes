using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToScene : MonoBehaviour
{
    public Image fadeImage;
    public Ease.DelegateType delType;
    public Ease.EaseType easeType;

    public float startDelay = 1.0f;
    public float fadeDuration = 1.0f;

    public Color fromFadeColor;
    public Color toFadeColor;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeToBlack());
    }

    private IEnumerator fadeToBlack() {
        yield return new WaitForSeconds(startDelay);

        float i, cur = 0f;

        Color fadeImgColor = fadeImage.color;

        while (cur < fadeDuration) {
            yield return null;
            i = cur / fadeDuration;

            print(i);

            i = Ease.GetDelegate(delType, easeType)(i);
            fadeImgColor = Color.Lerp(fromFadeColor, toFadeColor, i);
            fadeImage.color = fadeImgColor;

            cur += Time.deltaTime;
        }
        SceneManager.LoadScene(1);
    }
}
