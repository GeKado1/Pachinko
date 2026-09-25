using TMPro;
using UnityEngine;

public class Pocket : MonoBehaviour {
    [SerializeField] private int PocketValue;
    [SerializeField] private float PointsMultiplier = 1f;
    [SerializeField] private BallAmmoManager ammoManager;

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
}