using UnityEngine;
using UnityEngine.Pool;

public class BallPoolManager : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int defaultCapacity = 50;
    [SerializeField] private int maxSize = 150; 
    [SerializeField] private Transform poolParent;

    [Header("Límite de Juego")]
    [Tooltip("Número máximo de bolas que pueden estar en pantalla al mismo tiempo.")]
    [SerializeField] private int maxActiveBallsInScreen = 20;

    [Header("Debug Info")]
    [SerializeField] private int activeBalls;
    [SerializeField] private int inactiveBalls;
    [SerializeField] private int totalBalls;

    private ObjectPool<GameObject> pool;

    //Variable para la funcion static de coger las bolas
    private static int remaingBalls;

    private void Awake()
    {
        if (poolParent == null)
            poolParent = new GameObject("--- BALL POOL CONTAINER ---").transform;

        pool = new ObjectPool<GameObject>(
            createFunc: CreateBall,
            actionOnGet: OnGetBall,
            actionOnRelease: OnReleaseBall,
            actionOnDestroy: OnDestroyBall,
            collectionCheck: true, // Lanza error si intentas devolver una bola ya devuelta
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );

        remaingBalls = defaultCapacity;
    }

    private void Start()
    {
        PrePool(defaultCapacity);
    }

    private void Update()
    {
        if (pool != null)
        {
            activeBalls = pool.CountActive;
            inactiveBalls = pool.CountInactive;
            totalBalls = pool.CountAll;
        }
    }

    private void PrePool(int count)
    {
        var tempArray = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            tempArray[i] = pool.Get();
        }
        for (int i = 0; i < count; i++)
        {
            pool.Release(tempArray[i]);
        }
    }

    // se crea un nuevo objeto si el pool está vacío
    private GameObject CreateBall()
    {
        GameObject ball = Instantiate(ballPrefab, poolParent);

        if (ball.TryGetComponent<PachinkoBall>(out var ballComponent))
        {
            ballComponent.SetPool(pool);
        }

        return ball;
    }

    // cuando sacamos una bola del pool
    private void OnGetBall(GameObject ball)
    {
        ball.SetActive(true);
    }

    // cuando devolvemos una bola al pool
    private void OnReleaseBall(GameObject ball)
    {
        // Reset físicas para que quitar inercia y rotación 
        if (ball.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.velocity = Vector2.zero; 
            rb.angularVelocity = 0f;
        }

        ball.SetActive(false);
    }

    // si el pool excede la capacidad máxima o se limpia
    private void OnDestroyBall(GameObject ball)
    {
        Destroy(ball);
    }

    // disparar la bola desde el cañón
    public GameObject SpawnBall(Vector3 spawnPosition, Vector2 launchForce)
    {
        // Límite de bolas en pantalla
        if (pool.CountActive >= maxActiveBallsInScreen)
            return null;

        GameObject ball = pool.Get();
        ball.transform.position = spawnPosition;
        ball.transform.rotation = Quaternion.identity;

        if (ball.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.AddForce(launchForce, ForceMode2D.Impulse);
        }

        return ball;
    }

    //Retorna la capacidad de la pool para mostrarla como bolas restantes que tiene el jugador/que hay en el nivel
    public static int GetDefaultCapacity() => remaingBalls;

    public int GetCountActiveBalls() => activeBalls;
}
