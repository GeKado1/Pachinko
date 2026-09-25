using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisorPin : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private BallPoolManager poolManager;

    [Header("Configuración Visual y Física")]
    [SerializeField] private Vector3 smallBallScale = new Vector3(0.5f, 0.5f, 1f);
    [SerializeField] private float explosionForce = 3f;

    [Tooltip("Distancia desde el centro del pin donde aparecerán las bolas nuevas. Ajusta esto para que sea un poco mayor que el radio del collider del pin.")]
    [SerializeField] private float spawnRadiusOffset = 0.35f;

    [Header("Rango de Dirección del Spawn")]
    [Tooltip("Ángulo mínimo en grados. 0° = Derecha, 90° = Arriba, 180° = Izquierda, 270° = Abajo.")]
    [SerializeField] private float minAngle = 20f;  // Ligeramente inclinado hacia la derecha arriba

    [Tooltip("Ángulo máximo en grados. 180° es izquierda completa, 160° evita salir demasiado plano.")]
    [SerializeField] private float maxAngle = 160f; // Ligeramente inclinado hacia la izquierda arriba

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PachinkoBall>(out var originalBall))
        {
            int valor = originalBall.Multiplier;

            if (valor == 1)
            {
                // x1: Solo se hace pequeña
                originalBall.transform.localScale = smallBallScale;
            }
            else
            {
                Vector3 centerPosition = transform.position;

                // Se guarda la bola grande original
                originalBall.ReleaseToPool();

                // Se genera X bolas nuevas
                for (int i = 0; i < valor; i++)
                {
                    // Angulo aleatorio estrictamente entre minAngle y maxAngle
                    float randomAngleDeg = Random.Range(minAngle, maxAngle);
                    float angleRad = randomAngleDeg * Mathf.Deg2Rad;

                    // Angulo a un vector de dirección 2D (X, Y)
                    Vector2 customDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

                    // Posición de spawn alrededor del pin pero solo en la parte superior/lateral
                    Vector3 safeSpawnPosition = centerPosition + (Vector3)(customDirection * spawnRadiusOffset);
                    Vector2 force = customDirection * explosionForce;

                    // Se pide la bola al pool
                    GameObject newBall = poolManager.SpawnBall(safeSpawnPosition, force);

                    if (newBall != null && newBall.TryGetComponent<PachinkoBall>(out var newBallComponent))
                    {
                        newBallComponent.Multiplier = 1; // Las nuevas valen 1
                        newBall.transform.localScale = smallBallScale; // Las nuevas son pequeñas
                    }
                }
            }
        }
    }
}
