using UnityEngine;
using TMPro;
using DG.Tweening;
using Zenject;

public class FloatingMessageView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textLabel;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation")]
    [SerializeField] private Ease flyEase = Ease.OutCubic;
    [SerializeField] private float appearTime = 0.3f;
    [SerializeField] private float flyTime = 1.5f;
    [SerializeField] private float flyHeight = 100f;
    [SerializeField] private float fadeTime = 0.5f;

    public void Initialize(string message)
    {
        textLabel.text = message;

        canvasGroup.alpha = 0;
        Vector3 startPos = transform.localPosition;

        Sequence seq = DOTween.Sequence();

        seq.Append(canvasGroup.DOFade(1f, appearTime));
        seq.Join(transform.DOLocalMoveY(startPos.y + flyHeight, flyTime).SetEase(flyEase));

        seq.Insert(1.0f, canvasGroup.DOFade(0f, fadeTime));

        seq.OnComplete(() => Destroy(gameObject));
    }
    public class Factory : PlaceholderFactory<FloatingMessageView> { }
}