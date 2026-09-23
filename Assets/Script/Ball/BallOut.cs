using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOut : MonoBehaviour
{
    [SerializeField] private BallAmmoManager ammoManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PachinkoBall>(out var ball))
        {
            ball.ReleaseToPool();

            if (ammoManager != null)
                ammoManager.NotifyBallDrained();
        }
    }
}
