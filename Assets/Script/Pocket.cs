using UnityEngine;

public class Pocket : MonoBehaviour {
    [SerializeField] private int PocketValue;
    [SerializeField] private float PointsMultiplier = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            float ballMultiplier = 1f;

            if (collision.TryGetComponent<PachinkoBall>(out var ball))
            {
                ball.ReleaseToPool();
                ballMultiplier = ball.ballMultiplier;
            }

            int value = (int)(PocketValue * PointsMultiplier * ballMultiplier);

            PuntuactionManager.UpdatePuntuaction(value);
            BallManager.UpdateBalls();
        }
    }
}