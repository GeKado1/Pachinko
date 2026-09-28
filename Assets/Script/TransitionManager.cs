using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [Header("UI de Transición")]
    [Tooltip("El CanvasGroup que contiene la imagen de fondo (ej: pantalla negra).")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Contenedores Principales")]
    [SerializeField] private GameObject gameContainer; 
    [SerializeField] private GameObject shopContainer;

    [Header("Esconder UI en Shop")]
    [SerializeField] private List<GameObject> hideUIinShop = new List<GameObject>();

    [Header("Referencias de Juego")]
    [SerializeField] private BallAmmoManager ammoManager; 
    [SerializeField] private int ammoPerRound = 20;

    private void Start()
    {
        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.blocksRaycasts = false;

        gameContainer.SetActive(true);
        shopContainer.SetActive(false);
    }

    public void TriggerTransitionToShop()
    {
        StartCoroutine(TransitionRoutine(gameContainer, shopContainer));
    }

    public void TriggerTransitionToGame()
    {
        StartCoroutine(TransitionRoutine(shopContainer, gameContainer));
    }

    private IEnumerator TransitionRoutine(GameObject fromContainer, GameObject toContainer)
    {
        // Bloquear clics durante la transición para evitar bugs
        fadeCanvasGroup.blocksRaycasts = true;

        // Fade Out
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null; 
        }
        fadeCanvasGroup.alpha = 1f;

        fromContainer.SetActive(false);
        toContainer.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        if(fromContainer == shopContainer)
        {
            BallManager.ResetBallsRemaining();
            UIManager.NextRoundUpdate = true;

            foreach (GameObject gO in hideUIinShop)
                gO.SetActive(true);
        }
        else
        {
            foreach(GameObject gO in hideUIinShop)
                gO.SetActive(false);
        }

            //  Fade In
            timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.blocksRaycasts = false;
    }

    public void StartNextRound()
    {
        if (ammoManager != null)
            ammoManager.ResetAmmo(ammoPerRound);

        TriggerTransitionToGame();
    }
}
