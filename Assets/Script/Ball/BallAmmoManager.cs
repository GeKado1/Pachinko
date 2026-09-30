using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.U2D;

public class BallAmmoManager : MonoBehaviour
{
    [Header("Configuración de Reserva")]
    [SerializeField] private int initialAmmo = 50;
    [SerializeField] private int maxAmmoCap = 999;

    [Header("Referencias")]
    [SerializeField] private BallPoolManager poolManager;

    [Header("Referencias de Transición")]
    [SerializeField] private TransitionManager transitionManager;

    [Header("Referencias de Quota")]
    [SerializeField] private QuoteManager quoteManager;

    public int CurrentAmmo { get; private set; }

    // Eventos C# para actualizar la UI y notificar el fin de partida
    public event Action<int> OnAmmoChanged;
    //public event Action OnGameOver;

    private void Start()
    {
        CurrentAmmo = initialAmmo;
        OnAmmoChanged?.Invoke(CurrentAmmo);
    }

    public bool TryConsumeAmmo(int amount)
    {
        if (CurrentAmmo < amount)
        {
            CheckRoundEndCondition();
            return false;
        }

        CurrentAmmo -= amount;
        OnAmmoChanged?.Invoke(CurrentAmmo);
        return true;
    }

    public void AddAmmo(int amount)
    {
        if (amount <= 0) return;

        CurrentAmmo = Mathf.Min(CurrentAmmo + amount, maxAmmoCap);
        OnAmmoChanged?.Invoke(CurrentAmmo);
    }

    public void NotifyBallDrained()
    {
        StartCoroutine(CheckRoundEndCondition());
    }

    private IEnumerator CheckRoundEndCondition()
    {
        // Esperamos al siguiente frame para que el ObjectPool actualice ActiveBallsCount a 0
        yield return null;

        // Si no quedan bolas en reserva Y tampoco hay bolas rebotando en pantalla
        if (CurrentAmmo <= 0 && poolManager != null && poolManager.GetCountActiveBalls() == 0)
        {
            if (PuntuactionManager.GetPunctuaction() > 0)
            {
                if(quoteManager.GetActualRound() == quoteManager.GetMaxRounds() && quoteManager.GetQuote() > PuntuactionManager.GetPunctuaction())
                {
                    transitionManager.TriggerGameOver();
                    yield break;
                }

                if (transitionManager != null)
                    transitionManager.TriggerTransitionToShop();
            }
            else
            {
                transitionManager.TriggerGameOver();
                yield break;
            }

            if (quoteManager.GetActualRound() >= quoteManager.GetMaxRounds() && PuntuactionManager.GetPunctuaction() < quoteManager.GetQuote())
            {
                transitionManager.TriggerGameOver();
                yield break;
            }
        }

        if(poolManager != null && poolManager.GetCountActiveBalls() == 0 && PuntuactionManager.GetPunctuaction() == 0)
            transitionManager.TriggerGameOver();
    }

    public void ResetAmmo()
    {
        CurrentAmmo = BallPoolManager.GetDefaultCapacity();
        OnAmmoChanged?.Invoke(CurrentAmmo); // Notifica a la UI para que actualice el texto de bolas
    }
}
