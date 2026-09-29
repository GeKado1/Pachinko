using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private BallPoolManager poolManager;
    [SerializeField] private BallAmmoManager ammoManager;
    [SerializeField] private Transform spawnPoint; 

    [Header("Configuración de Tecla y Fuerza")]
    [SerializeField] private KeyCode launchKey = KeyCode.Space;
    [SerializeField] private Vector2 launchForce = new Vector2(0f, -2f); // Fuerza inicial (ej: impulso hacia abajo o diagonal)

    [Header("Variación Aleatoria (Jitter)")]
    [Tooltip("Grados de desviación hacia la izquierda o derecha.")]
    [SerializeField] private float angleSpreadDegrees = 3f;

    [Tooltip("Porcentaje de variación en la intensidad")]
    [SerializeField] private float forceVariationPercent = 0.1f;

    [Tooltip("Desplazamiento en la posición de salida")]
    [SerializeField] private float positionJitter = 0.02f;

    [Header("Control de Cadencia (Cooldown)")]
    [SerializeField] private float cooldownTime = 0.1f; 
    [SerializeField] private bool allowContinuousAutoFire = false; // ¿Permitir mantener pulsado?

    [Header("Animaciones Boton")]
    [SerializeField] private Animation buttonAnimation;


    private int currentMultiplier = 1;
    private float nextSpawnTime;

    private void Update()
    {
        bool isKeyPressed = allowContinuousAutoFire
            ? Input.GetKey(launchKey)
            : Input.GetKeyDown(launchKey);

        if (isKeyPressed && Time.time >= nextSpawnTime)
        {
            LaunchBall();
            nextSpawnTime = Time.time + cooldownTime;
        }
    }

    public void BottonLaunchBall()
    {
        if (Time.time >= nextSpawnTime)
        {
            LaunchBall();
            nextSpawnTime = Time.time + cooldownTime;
            buttonAnimation.Rewind();
            buttonAnimation.Play();
        }
    }

    public void SetMultiplier(int multiplierValue)
    {
        currentMultiplier = multiplierValue;
        Debug.Log($"Modo de disparo cambiado a: x{currentMultiplier} (Coste: {currentMultiplier} bolas)");
    }

    private void LaunchBall()
    {
        if (poolManager == null)
            return;

        if (PuntuactionManager.GetPunctuaction() < currentMultiplier)
            return;

        if (ammoManager != null && ammoManager.CurrentAmmo <= 0)
            return;

        if (BallManager.GetBalls() <= 0)
            return;

        if (!ammoManager.TryConsumeAmmo(1))
            return;

        PuntuactionManager.UpdatePuntuaction(-currentMultiplier);

        // 1. Variación en la Posición
        Vector3 basePosition = spawnPoint != null ? spawnPoint.position : transform.position;
        Vector3 randomPositionOffset = new Vector3(
            Random.Range(-positionJitter, positionJitter),
            Random.Range(-positionJitter, positionJitter),
            0f
        );

        Vector3 finalPosition = basePosition + randomPositionOffset;

        // 2. Variación en la Dirección
        float randomAngle = Random.Range(-angleSpreadDegrees, angleSpreadDegrees);
        Vector2 randomizedDirection = Quaternion.Euler(0, 0, randomAngle) * launchForce;

        // 3. Variación en la Magnitud de la Fuerza
        float randomForceMultiplier = Random.Range(1f - forceVariationPercent, 1f + forceVariationPercent);
        Vector2 finalForce = randomizedDirection * randomForceMultiplier;

        GameObject spawnedBall = poolManager.SpawnBall(finalPosition, finalForce);

        if (spawnedBall != null && spawnedBall.TryGetComponent<PachinkoBall>(out var ballComponent))
        {
            ballComponent.Multiplier = currentMultiplier;

            BallManager.UpdateBalls(1, false);
            // Cambiar el color o tamaño de la bola según su valor para feedback visual
        }
        else if (spawnedBall == null)
        {
            // Si el disparo falló (límite de bolas en pantalla), devolvemos la munición exacta
            ammoManager.AddAmmo(1);

            PuntuactionManager.UpdatePuntuaction(currentMultiplier);
        }

    }
}