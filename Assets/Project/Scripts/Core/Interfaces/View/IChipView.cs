using System;
using UnityEngine;

public interface IChipView
{
    Chip ChipData { get; }
    RectTransform RectTransform { get; }
    void Initialize(Chip chip);
    void UpdateVisual(Chip chip);
    void PlaySpawnAnimation(Action onComplete = null);
    void PlayMergeAnimation(Action onComplete = null);
    void PlayDisappearAnimation(Action onComplete = null);
    void SetVisible(bool visible);
    void SetPosition(Vector3 position);
}