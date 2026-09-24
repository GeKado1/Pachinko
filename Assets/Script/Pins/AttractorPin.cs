using UnityEngine;

public class AttractorPin : MonoBehaviour
{
    [Tooltip("Intensidad de la fuerza de atracción o de repulsión")]
    [SerializeField] private float Force;

    [Tooltip("Si está activo, la fuerza de atracción aumenta a medida que la bola está más cerca del centro")]
    [SerializeField] private bool StrongerWhenCloser;

    [Tooltip("Si está activo, el pin será repulsor en vez de atractor")]
    [SerializeField] private bool IsRejectorPin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Rigidbody2D ballRigidbody = collision.GetComponent<Rigidbody2D>();

            if (ballRigidbody != null)
            {
                Vector2 direction = transform.position - collision.transform.position;
                float distance = direction.magnitude;

                Vector2 pullDirection = direction.normalized;

                float finalForce = Force;

                if (StrongerWhenCloser)
                {
                    finalForce = Force / Mathf.Max(distance, 0.2f);
                }

                ballRigidbody.AddForce(pullDirection * finalForce, ForceMode2D.Force);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsRejectorPin) {
            if (collision.CompareTag("Ball"))
            {
                Rigidbody2D ballRigidbody = collision.GetComponent<Rigidbody2D>();

                if (ballRigidbody != null)
                {
                    Vector2 direction = collision.transform.position - transform.position;
                    float distance = direction.magnitude;

                    Vector2 pushDirection = direction.normalized;

                    ballRigidbody.AddForce(pushDirection * Force, ForceMode2D.Force);
                }
            }
        }
    }
}