using UnityEngine;

public class PuntuactionManager : MonoBehaviour
{
    [SerializeField] private int PUNTUATION = 20;
    private static int Punctuaction;

    void Start()
    {
        Punctuaction = PUNTUATION;
    }

    public static void UpdatePuntuaction(int pointsToAdd)
    {
        Punctuaction += pointsToAdd;

        if (Punctuaction <= 0)
        {
            Punctuaction = 0;
        }

        UIManager.PointsChanged = true;
    }

    public static int GetPunctuaction()
    {
        return Punctuaction;
    }
}