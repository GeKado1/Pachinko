using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    // HOW TO USE
    // Fast Shake ->    CameraShake.Instance.Shake(DURATION, MAGNITUDE);
    // Start Countinuous Shake ->   CameraShake.Instance.StartContinuousShake(0.1f);
    // Stop Countinuous Shake ->    CameraShake.Instance.StopContinuousShake();

    private Vector3 m_OriginalPos;
    private Coroutine m_CurrentShake;
    private bool m_IsCountinuousShaking = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        m_OriginalPos = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        if (m_CurrentShake != null)
        {
            StopCoroutine(m_CurrentShake);
            transform.localPosition = m_OriginalPos;
        }
        m_CurrentShake = StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float l_elapsed = 0.0f;

        float l_initialMagnitude = magnitude;

        while (l_elapsed < duration)
        {
            float l_pctMagnitude = l_elapsed / duration;
            magnitude = l_initialMagnitude * (1.0f - l_pctMagnitude);

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(m_OriginalPos.x + x, m_OriginalPos.y + y, m_OriginalPos.z);

            l_elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = m_OriginalPos;
    }

    public void StartContinuousShake(float magnitude)
    {
        m_IsCountinuousShaking = true;
        if (m_CurrentShake != null) StopCoroutine(m_CurrentShake);
        m_CurrentShake = StartCoroutine(ContinuousShakeCoroutine(magnitude));
    }

    public void StopContinuousShake()
    {
        m_IsCountinuousShaking = false;
        if (m_CurrentShake != null)
        {
            StopCoroutine(m_CurrentShake);
        }
        transform.localPosition = m_OriginalPos;
    }

    private IEnumerator ContinuousShakeCoroutine(float magnitude)
    {
        while (m_IsCountinuousShaking)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(m_OriginalPos.x + x, m_OriginalPos.y + y, m_OriginalPos.z);

            yield return null;
        }
    }
}
