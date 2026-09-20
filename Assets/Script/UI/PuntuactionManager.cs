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
        UIManager.PointsChanged = true;
    }

    public static int GetPunctuaction()
    {
        return Punctuaction;
    }
}