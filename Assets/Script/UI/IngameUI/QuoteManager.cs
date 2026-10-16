using UnityEngine;
using UnityEngine.UI;

public class QuoteManager : MonoBehaviour
{
    [Header("Configuración de Cuota")]
    [SerializeField] private int Quote;
    [SerializeField] private int actualRound = 1;
    [SerializeField] private int maxRounds = 3;
    private static int PointsRequired;

    [Header("UI y Transición")]
    [SerializeField] private TransitionManager transitionManager;
    [SerializeField] private Button botonSiguienteRonda;
    [SerializeField] private Button botonPagarCuota;

    [Header("Win Condition")]
    public int paiedQuotes = 0;
    public int quotesTotalsForWin = 3;

    void Awake()
    {
        PointsRequired = Quote;
    }

    public void UpdateShopButtons()
    {
        int puntosActuales = PuntuactionManager.GetPunctuaction();

        // El botón de "Siguiente Ronda" SOLO se puede pulsar en rondas 1 y 2
        if (botonSiguienteRonda != null)
            botonSiguienteRonda.interactable = (actualRound < maxRounds);

        // El botón de "Pagar Cuota" SOLO se puede pulsar si tienes puntos suficientes
        if (botonPagarCuota != null)
            botonPagarCuota.interactable = (puntosActuales >= Quote);
    }

    public void GoToNextRound()
    {
        if (actualRound < maxRounds)
        {
            actualRound++;
            transitionManager.StartNextRound(); // Recarga bolas y vuelve al tablero
        }
    }

    public void PayQuota()
    {
        int puntosActuales = PuntuactionManager.GetPunctuaction();

        if (puntosActuales >= Quote)
        {
            // 1. Cobramos la cuota
            PuntuactionManager.UpdatePuntuaction(-Quote);

            paiedQuotes++;

            // 3. COMPROBACIÓN DE VICTORIA
            if (paiedQuotes >= quotesTotalsForWin)
            {
                if (transitionManager != null)
                    transitionManager.TriggerWin();

                return;
            }

            // 2. Subimos la dificultad y reseteamos rondas
            Quote *= 2; 
            actualRound = 1;
            PointsRequired = Quote;
            UIManager.NextQuoteUpdate = true;

            // 3. Volvemos al tablero como una ronda nueva
            transitionManager.StartNextRound();
        }
    }

    public static int GetPointsRequired()
    {
        return PointsRequired;
    }

    public int GetActualRound() => actualRound;

    public int GetMaxRounds() => maxRounds;

    public int GetQuote() => Quote;
}