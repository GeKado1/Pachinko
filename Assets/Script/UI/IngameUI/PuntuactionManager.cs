using UnityEngine;

public class PuntuactionManager : MonoBehaviour
{
    private static int Punctuaction;

    void Start()
    {
        Punctuaction = 0;
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