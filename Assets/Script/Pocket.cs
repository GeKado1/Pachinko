using TMPro;
using UnityEngine;

public class Pocket : MonoBehaviour {
    [SerializeField] private int PocketValue;
    [SerializeField] private float PointsMultiplier = 1f;
    [SerializeField] private BallAmmoManager ammoManager;

    [Header("Parametros de definición del Pocket")]
    [Tooltip("Palo")]
    [SerializeField] private string Suit;
    [SerializeField] private string Color;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            float ballValue = 1f;

            if (collision.TryGetComponent<PachinkoBall>(out var ball))
            {
                ballValue = ball.GetTotalBallValue();
                ball.ReleaseToPool();

                if (ammoManager != null)
                    ammoManager.NotifyBallDrained();
            }

            int value = (int)(PocketValue * PointsMultiplier * ballValue);

            PuntuactionManager.UpdatePuntuaction(value);
        }
    }

    public void ModifyMult(float mult, bool AddOrSubstract)
    {
        if (AddOrSubstract)
        {
            if (mult < 1)
            {
                PointsMultiplier += mult;
            }
            else
            {
                PointsMultiplier *= mult;
            }
        }
        else
        {
            PointsMultiplier /= mult;
        }
    }
}