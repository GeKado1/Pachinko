using System.Collections.Generic;
using UnityEngine;

public class MultPin : MonoBehaviour
{
    [Tooltip("Valor que se sumará o restará en el multiplicador de la bola")]
    [SerializeField] float MultValue;

    [Tooltip("Si está activo, el pin será un Nerf Pin")]
    [SerializeField] bool isNerfPin;

    private HashSet<GameObject> AppliedBalls = new HashSet<GameObject>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (collision.gameObject.TryGetComponent<PachinkoBall>(out var ball))
            {
                if (!AppliedBalls.Contains(collision.gameObject)) {
                    if (isNerfPin)
                    {
                        ball.ModifyMultiplier(-MultValue);
                    }
                    else
                    {
                        ball.ModifyMultiplier(MultValue);
                    }

                    AppliedBalls.Add(collision.gameObject);
                }
            }
        }
    }
}