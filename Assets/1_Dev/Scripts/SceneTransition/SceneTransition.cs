using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    [SerializeField] private Image transitionImage;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float waitDuration = 0.25f;

#region LifeCycle
    private void Awake()
    {
        Instance = this;

        // Başlangıçta alpha değerini 1'e ayarla ve fade-out yap
        transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 1f);
        StartFadeOutOnStart();
    }

    private void OnEnable() {
        GameStateEvents.OnDie += StartTransition;
    }

    private void OnDisable() {
        GameStateEvents.OnDie -= StartTransition;
    }
#endregion
    public void StartTransition()
    {
        // Normal geçiş işlemi (alpha 0'dan 1'e, bekle, sonra 1'den 0'a)
        transitionImage.DOFade(1f, fadeDuration / 4)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(waitDuration, () =>
                {
                    transitionImage.DOFade(0f, fadeDuration);
                });
            });
    }

    private void StartFadeOutOnStart()
    {
        // Başlangıçta alpha değerini 1'den 0'a geçiş yap
        transitionImage.DOFade(0f, fadeDuration);
    }
}
