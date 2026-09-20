using UnityEngine;
using UnityEngine.Pool;

public class PachinkoBall : MonoBehaviour
{
    private IObjectPool<GameObject> ballPool;

    public void SetPool(IObjectPool<GameObject> pool)
    {
        ballPool = pool;
    }

    // Método para devolver la bola al pool
    public void ReleaseToPool()
    {
        if (ballPool != null)
        {
            ballPool.Release(gameObject);
        }
        else
        {
            Destroy(gameObject); // Fallback por si la bola no viene del pool
        }
    }
}
