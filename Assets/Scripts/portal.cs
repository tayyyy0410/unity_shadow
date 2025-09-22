using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class portal: MonoBehaviour
{
    public string startSceneName = "start";
    public Light2D doorLight;
    public SpriteRenderer doorVisual;

    [Header("Dotween stuff")]
    public float pulseTo = 2.4f;
    public float pulseDur = 0.18f;
    public float scalePunch = 0.08f;
    public float loadDelay = 0.12f;

    float _origIntensity;

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        if (doorLight) _origIntensity = doorLight.intensity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (doorLight)
        {
            DOTween.Kill(doorLight); 
            DOTween.To(
                    () => doorLight.intensity,
                    x => doorLight.intensity = x,
                    pulseTo,
                    pulseDur
                ).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    DOTween.To(
                        () => doorLight.intensity,
                        x => doorLight.intensity = x,
                        _origIntensity,
                        pulseDur * 0.7f
                    );
                });
        }

        if (doorVisual)
        {
            doorVisual.transform.DOPunchScale(Vector3.one * scalePunch, 0.22f, 6, 0.8f);
        }

        DOVirtual.DelayedCall(loadDelay + pulseDur, () => SceneManager.LoadScene(startSceneName));
    }
}