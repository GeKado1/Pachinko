using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BallAmmoManager : MonoBehaviour
{
    [Header("Configuración de Reserva")]
    [SerializeField] private int initialAmmo = 50;
    [SerializeField] private int maxAmmoCap = 999;

    [Header("Referencias")]
    [SerializeField] private BallPoolManager poolManager;

    [Header("Referencias de Transición")]
    [SerializeField] private TransitionManager transitionManager;

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
                Debug.Log("Ronda terminada. Tienes puntos suficientes.");

                if (transitionManager != null)
                    transitionManager.TriggerTransitionToShop();
            }
            else
            {
                Debug.Log("¡GAME OVER! Te has quedado sin bolas y tus puntos han llegado a 0.");

                // Aquí activas tu panel de Game Over o reinicias la escena
                // GameOverPanel.SetActive(true);
            }
        }

        if(poolManager.GetCountActiveBalls() == 0 && PuntuactionManager.GetPunctuaction() == 0)
            Debug.Log("¡GAME OVER! Te has quedado sin bolas y tus puntos han llegado a 0.");
    }

    public void ResetAmmo(int amount)
    {
        CurrentAmmo = amount;
        OnAmmoChanged?.Invoke(CurrentAmmo); // Notifica a la UI para que actualice el texto de bolas
    }
}
