using UnityEngine;

public class BouncePin : MonoBehaviour
{
    [Header("Configuración del Rebote")]
    [Tooltip("La fuerza que se aplicará a la bola hacia arriba")]
    [SerializeField] private float bounceForce;

    [Tooltip("Si está activo, ignora la velocidad actual de la bola al rebotar. Si está desactivado, añade la fuerza a la velocidad actual")]
    public bool resetVelocityOnBounce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ApplyForce(collision);
        }
    }

    private void ApplyForce(Collision2D collision)
    {
        Rigidbody2D ballRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        if (ballRigidbody != null)
        {
            ContactPoint2D contact = collision.contacts[0];

            Vector2 hitNormal = contact.normal;

            //Resetamos la velocidad actual para asegurar un rebote limpio, conservando un poco de la velocidad lateral original
            if (resetVelocityOnBounce)
            {
                ballRigidbody.velocity = Vector2.zero;
            }

            ballRigidbody.AddForce(hitNormal * bounceForce, ForceMode2D.Impulse);
        }
    }
}