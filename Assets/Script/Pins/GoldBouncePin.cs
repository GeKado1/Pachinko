using UnityEngine;

public class GoldBouncePin : BouncePin
{
    [Tooltip("Referencia para poder añadir más bolas cuando una bola rebota en el Gold Bounce Pin")]
    [SerializeField] private BallAmmoManager AmmoManager;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        AddBall();
    }

    private void AddBall()
    {
        AmmoManager.AddAmmo(1);
        BallManager.UpdateBalls(1, true);
    }
}