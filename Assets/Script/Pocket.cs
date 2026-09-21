using UnityEngine;

public class Pocket : MonoBehaviour {
    [SerializeField] private int PocketValue;
    [SerializeField] private float PointsMultiplier = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            float ballValue = 1f;

            if (collision.TryGetComponent<PachinkoBall>(out var ball))
            {
                ball.ReleaseToPool();
                ballValue = ball.GetTotalBallValue();
            }

            int value = (int)(PocketValue * PointsMultiplier * ballValue);

            PuntuactionManager.UpdatePuntuaction(value);
            BallManager.UpdateBalls();
        }
    }
}