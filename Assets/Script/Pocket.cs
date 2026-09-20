using UnityEngine;

public class Pocket : MonoBehaviour {
    [SerializeField] private int PocketValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (collision.TryGetComponent<PachinkoBall>(out var ball))
            {
                ball.ReleaseToPool();
            }

            PuntuactionManager.UpdatePuntuaction(PocketValue);
            BallManager.UpdateBalls();
        }
    }
}