using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private BallLauncher BallLauncher;

    private Coroutine launchCoroutine;

    public void OnPointerDown(PointerEventData eventData)
    {
        launchCoroutine = StartCoroutine(Hold());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (launchCoroutine != null)
        {
            StopCoroutine(launchCoroutine);
            launchCoroutine = null;
        }
    }

    private IEnumerator Hold()
    {
        while (true)
        {
            BallLauncher.BottonLaunchBall();

            yield return null;
        }
    }
}