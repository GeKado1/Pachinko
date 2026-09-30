using UnityEngine;

public class BouncePinController : MonoBehaviour
{
    [SerializeField] private GameObject BouncePin;
    [SerializeField] private GameObject GoldBouncePin;

    void Start()
    {
        GoldBouncePin.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Instance.PlaySFX(SFX.BallBounce);
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            HaveGoldUpgrade();
        }
    }

#endif

    public void HaveGoldUpgrade()
    {
        BouncePin.SetActive(false);
        GoldBouncePin.SetActive(true);
    }
}