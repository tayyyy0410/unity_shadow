using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Linq;

public class TextGroupJuice : MonoBehaviour
{
    [Header("Targets")]
    public bool includeInactive = false;  
    public bool affectTMP = true;
    public bool affectSprites = true;

    [Header("Intro")]
    public float introScaleFrom = 0.9f;

    public float introDuration = 0.5f;
    public float introStagger = 0.06f;        
    public Ease  introEase = Ease.OutBack;

    [Header("Breath")]
    public float breathScale = 1.05f;
    public float breathDuration = 2.0f;       
    public Ease  breathEase = Ease.InOutSine;

    void Start()
    {
    
        var tmps = affectTMP
            ? GetComponentsInChildren<TMP_Text>(includeInactive)
            : new TMP_Text[0];

    
        var sprites = affectSprites
            ? GetComponentsInChildren<SpriteRenderer>(includeInactive)
            : new SpriteRenderer[0];

        int index = 0;

     
        foreach (var t in tmps.Where(t => t != null && t.enabled))
        {
            var rt = t.rectTransform;

          
            var c = t.color; c.a = 0f; t.color = c;
            rt.localScale = Vector3.one * introScaleFrom;

        
            Sequence s = DOTween.Sequence();
            s.SetDelay(index * introStagger);
            s.Join(t.DOFade(1f, introDuration));
            s.Join(rt.DOScale(1f, introDuration).SetEase(introEase));
            
            s.OnComplete(() =>
            {
                rt.DOScale(breathScale, breathDuration)
                  .SetEase(breathEase)
                  .SetLoops(-1, LoopType.Yoyo);
            });

            index++;
        }

        foreach (var sr in sprites.Where(s => s != null && s.enabled))
        {
    
            var c = sr.color; c.a = 0f; sr.color = c;
            sr.transform.localScale = Vector3.one * introScaleFrom;

            Sequence s = DOTween.Sequence();
            s.SetDelay(index * introStagger);
            s.Join(sr.DOFade(1f, introDuration));
            s.Join(sr.transform.DOScale(1f, introDuration).SetEase(introEase));
            s.OnComplete(() =>
            {
                sr.transform.DOScale(breathScale, breathDuration)
                  .SetEase(breathEase)
                  .SetLoops(-1, LoopType.Yoyo);
            });

            index++;
        }
    }
}
