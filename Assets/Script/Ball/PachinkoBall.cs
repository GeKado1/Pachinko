using UnityEngine;
using UnityEngine.Pool;

public class PachinkoBall : MonoBehaviour
{
    private IObjectPool<GameObject> ballPool;
    [SerializeField] private float ballValue = 1f;
    [SerializeField] private float ballMultiplier = 1f;

    public void SetPool(IObjectPool<GameObject> pool)
    {
        ballPool = pool;
    }

    // Método para devolver la bola al pool
    public void ReleaseToPool()
    {
        if (!gameObject.activeInHierarchy) return;

        if (ballPool != null)
            ballPool.Release(gameObject);
        else
            Destroy(gameObject); // Fallback por si la bola no viene del pool
    }

    //Función para modificar el multiplicador de la bola
    public void ModifyMultiplier(float modifierMult)
    {
        ballMultiplier += modifierMult;
    }

    public float GetTotalBallValue()
    {
        return ballValue * ballMultiplier;
    }
}
