using UnityEngine;

public class Pocket : MonoBehaviour {
    [SerializeField] private int PocketValue;
    [SerializeField] private float PointsMultiplier = 1f;
    [SerializeField] private BallAmmoManager ammoManager;

    [Header("Parametros de definición del Pocket")]
    [Tooltip("Palo")]
    public string Suit;
    public string Color;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            float ballValue = 1f;

            if (collision.TryGetComponent<PachinkoBall>(out var ball))
            {
                ballValue = ball.GetTotalBallValue();
                ball.ReleaseToPool();

                if (ammoManager != null)
                    ammoManager.NotifyBallDrained();
            }

            int value = Mathf.RoundToInt(PocketValue * PointsMultiplier * ballValue);

            PuntuactionManager.UpdatePuntuaction(value);
            AudioManager.Instance.PlaySFX(SFX.BallFalled);
        }
    }

    public void ModifyMult(float mult)
    {
        PointsMultiplier += mult;
    }

    public void ModifyMult(float mult, bool multiplyOrDivide)
    {
        if (multiplyOrDivide)
        {
            PointsMultiplier *= mult;
        }
        else
        {
            PointsMultiplier /= mult;
        }
    }

    public float GetPocketValue() {
        return PocketValue;
    }
}