using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAmmoManager : MonoBehaviour
{
    [Header("Configuración de Reserva")]
    [SerializeField] private int initialAmmo = 50;
    [SerializeField] private int maxAmmoCap = 999;

    [Header("Referencias")]
    [SerializeField] private BallPoolManager poolManager;

    public int CurrentAmmo { get; private set; }

    // Eventos C# para actualizar la UI y notificar el fin de partida
    public event Action<int> OnAmmoChanged;
    public event Action OnGameOver;

    private void Start()
    {
        CurrentAmmo = initialAmmo;
        OnAmmoChanged?.Invoke(CurrentAmmo);
    }

    public bool TryConsumeAmmo()
    {
        if (CurrentAmmo <= 0)
        {
            CheckGameOverCondition();
            return false;
        }

        CurrentAmmo--;
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
        CheckGameOverCondition();
    }

    private void CheckGameOverCondition()
    {
        // Si no quedan bolas en reserva Y tampoco hay bolas rebotando en pantalla
        if (CurrentAmmo <= 0 && poolManager != null && poolManager.GetCountActiveBalls() == 0)
        {
            Debug.Log("¡GAME OVER! Te has quedado sin bolas.");
            OnGameOver?.Invoke();
        }
    }
}
