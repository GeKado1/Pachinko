using UnityEngine;

public class PlayerPocket : MonoBehaviour
{
    public static PlayerPocket Instance { get; private set; }

    public static int Points;
    public static int Balls;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Points += 1000;
        }
    }
#endif

    //Función que se llama para guardar los puntos y las bolas para usarlos en la tienda antes de cambiar a la scene de la tienda
    public static void ChangeScene()
    {
        Points = PuntuactionManager.GetPunctuaction();
        Balls = BallManager.GetBalls();
    }
}