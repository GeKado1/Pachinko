using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        // 1. Bloqueamos interacción
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        float timer = 0f;
        while (timer < 1.5f)
        {
            timer += Time.deltaTime;
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1f, 0f, timer / 1.5f);
            yield return null;
        }
        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
