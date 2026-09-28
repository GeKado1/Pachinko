using UnityEngine;

public class GoldBouncePin : BouncePin
{
    [Tooltip("Referencia para sumar bolas puntuación cuando una bola rebota en el Gold Bounce Pin")]
    [SerializeField] private PuntuactionManager PuntuactionManager;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        AddBall();
    }

    private void AddBall()
    {
        PuntuactionManager.UpdatePuntuaction(1);
    }
}