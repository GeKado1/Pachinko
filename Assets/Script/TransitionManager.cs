using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [Header("Contenedores Principales")]
    [SerializeField] private GameObject gameContainer;
    [SerializeField] private GameObject shopContainer;

    [Header("Esconder UI en Shop")]
    [SerializeField] private List<GameObject> hideUIinShop = new List<GameObject>();

    [Header("Referencias de Juego")]
    [SerializeField] private BallAmmoManager ammoManager;
    [SerializeField] private int ammoPerRound = 20;

    [Header("Configuración de Cámara")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform gameCameraTarget;
    [SerializeField] private Transform shopCameraTarget;
    [SerializeField] private float cameraMoveDuration = 0.5f;

    private void Start()
    {
        // Colocar la cámara en la posición inicial del juego al arrancar
        if (cameraTransform != null && gameCameraTarget != null)
        {
            cameraTransform.position = gameCameraTarget.position;
            cameraTransform.rotation = gameCameraTarget.rotation;
        }
    }

    public void TriggerTransitionToShop()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionRoutine(gameContainer, shopContainer, shopCameraTarget));
    }

    public void TriggerTransitionToGame()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionRoutine(shopContainer, gameContainer, gameCameraTarget));
    }

    private IEnumerator TransitionRoutine(GameObject fromContainer, GameObject toContainer, Transform targetCameraTransform)
    {
        if (fromContainer == shopContainer)
        {
            BallManager.ResetBallsRemaining();
            UIManager.NextRoundUpdate = true;

            foreach (GameObject gO in hideUIinShop)
                gO.SetActive(true);
        }
        else
        {
            foreach (GameObject gO in hideUIinShop)
                gO.SetActive(false);
        }

        // Mover y rotar la cámara suavemente hacia el destino
        if (cameraTransform != null && targetCameraTransform != null)
        {
            float camTimer = 0f;
            Vector3 startPos = cameraTransform.position;
            Quaternion startRot = cameraTransform.rotation;

            while (camTimer < cameraMoveDuration)
            {
                camTimer += Time.deltaTime;
                float t = camTimer / cameraMoveDuration;

                // Suavizado
                t = Mathf.SmoothStep(0f, 1f, t);

                cameraTransform.position = Vector3.Lerp(startPos, targetCameraTransform.position, t);
                cameraTransform.rotation = Quaternion.Slerp(startRot, targetCameraTransform.rotation, t);

                yield return null;
            }

            // Asegurar posición y rotación final
            cameraTransform.position = targetCameraTransform.position;
            cameraTransform.rotation = targetCameraTransform.rotation;
        }
    }

    public void StartNextRound()
    {
        if (ammoManager != null)
            ammoManager.ResetAmmo(ammoPerRound);

        TriggerTransitionToGame();
    }
}