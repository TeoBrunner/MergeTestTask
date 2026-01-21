using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class ChipView : MonoBehaviour, IChipView
{
    [Header("UI Components")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation Settings")]
    [SerializeField] private float spawnDuration = 0.3f;
    [SerializeField] private float mergeDuration = 0.4f;
    [SerializeField] private float despawnDuration = 0.2f;
    [SerializeField] private float mergeScale = 1.5f;
    [SerializeField] private Ease spawnEase = Ease.OutBack;
    [SerializeField] private Ease mergeEase = Ease.InOutQuad;
    [SerializeField] private Ease despawnEase = Ease.InBack;

    private Chip chipData;
    private RectTransform rectTransform;
    private Sequence currentAnimation;


    public Chip ChipData => chipData;
    public RectTransform RectTransform => rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        ValidateComponents();
    }
    private void OnDestroy()
    {
        currentAnimation?.Kill();
    }
    public void Initialize(Chip chip)
    {
        if (chip == null)
        {
            Debug.LogError("ChipView: Cannot initialize with null chip");
            return;
        }

        chipData = chip;
        UpdateVisual(chip);

        SetVisible(false);
    }

    public void UpdateVisual(Chip chip)
    {
        if (chip == null)
        {
            Debug.LogWarning("ChipView: UpdateVisual called with null chip");
            return;
        }

        chipData = chip;

        if (chip.Type != null && iconImage != null)
        {
            iconImage.sprite = chip.Type.Sprite;
            iconImage.enabled = true;
        }
        else
        {
            if (iconImage != null)
                iconImage.enabled = false;
        }

        UpdateLevelVisual(chip.Level);
    }

    public void PlaySpawnAnimation(Action onComplete = null)
    {
        StopCurrentAnimation();

        SetVisible(true);

        transform.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;

        currentAnimation = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one, spawnDuration).SetEase(spawnEase))
            .Join(canvasGroup.DOFade(1f, spawnDuration))
            .OnComplete(() => onComplete?.Invoke());
    }

    public void PlayMergeAnimation(Action onComplete = null)
    {
        StopCurrentAnimation();

        currentAnimation = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one * mergeScale, mergeDuration * 0.5f).SetEase(mergeEase))
            .Append(transform.DOScale(Vector3.one, mergeDuration * 0.5f).SetEase(mergeEase))
            .OnComplete(() => onComplete?.Invoke());
    }

    public void PlayDisappearAnimation(Action onComplete = null)
    {
        StopCurrentAnimation();

        currentAnimation = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.zero, despawnDuration).SetEase(despawnEase))
            .Join(canvasGroup.DOFade(0f, despawnDuration))
            .OnComplete(() =>
            {
                SetVisible(false);
                onComplete?.Invoke();
            });
    }
    public void SetVisible(bool visible)
    {
        canvasGroup.alpha = visible ? 1f : 0f;
        gameObject.SetActive(visible);
    }
    public void SetPosition(Vector3 position)
    {
        rectTransform.position = position;
    }

    private void UpdateLevelVisual(int level)
    {
        levelText.text = level.ToString();
    }
    private void StopCurrentAnimation()
    {
        currentAnimation?.Kill();
        currentAnimation = null;
    }
    private void ValidateComponents()
    {
        if (iconImage == null)
        {
            Debug.LogError($"ChipView: iconImage is not assigned on {gameObject.name}");
        }
    }
    public override string ToString()
    {
        return $"ChipView({chipData?.ToString() ?? "null"})";
    }

    public class Factory : PlaceholderFactory<ChipView> { }
}