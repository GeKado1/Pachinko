using UnityEngine;

public class Pocket : MonoBehaviour {
    [SerializeField] private int PocketValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            PuntuactionManager.UpdatePuntuaction(PocketValue);
            BallManager.UpdateBalls();
        }
    }
}