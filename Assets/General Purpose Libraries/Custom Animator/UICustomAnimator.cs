using System.Collections;
using UnityEngine;

public class UICustomAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform target;

    private float defaultRectSizeX;
    private float defaultRectSizeY;
    private Vector3 defaultRectPosition;

    private void Awake()
    {
        defaultRectSizeX = target.sizeDelta.x;
        defaultRectSizeY = target.sizeDelta.y;
        defaultRectPosition = target.anchoredPosition;
    }

    public void StartAnimation(AnimationSO animationSO)
    {
        StopAllCoroutines();
        ResetSizeToDefault();
        ResetPositionToDefault();

        foreach (AnimationConfig config in animationSO.configs)
        {
            switch (config.type)
            {
                case AnimationType.SizeY:
                    if (config.executionStyle is AnimationStyle.Instant) SetHeight(config);
                    else StartCoroutine(LerpHeight(config));
                    break;
                case AnimationType.SizeX:
                    if (config.executionStyle is AnimationStyle.Instant) SetWidth(config);
                    else StartCoroutine(LerpWidth(config));
                    break;
                case AnimationType.Position:
                    if (config.executionStyle is AnimationStyle.Instant) SetPosition(config);
                    else StartCoroutine(LerpToPosition(config));
                    break;
            }
        }
    }

    private void ResetSizeToDefault()
    {
        target.sizeDelta = new Vector2(defaultRectSizeX, defaultRectSizeY);
    }

    private void ResetPositionToDefault()
    {
        target.anchoredPosition = defaultRectPosition;
    }

    private void SetWidth(AnimationConfig config)
    {
        target.sizeDelta = new Vector2(defaultRectSizeX + config.scaleFactor, defaultRectSizeY);
    }

    private void SetHeight(AnimationConfig config)
    {
        target.sizeDelta = new Vector2(defaultRectSizeX, defaultRectSizeY + config.scaleFactor);
    }

    private IEnumerator LerpWidth(AnimationConfig config)
    {
        if (config == null) yield break;

        float time = 0;
        float originalWidth = target.sizeDelta.x; // Original width

        while (time < config.duration)
        {
            time += Time.deltaTime;
            float expansion = config.curve.Evaluate(time / config.duration) * config.scaleFactor;
            float width = originalWidth + expansion;
            target.sizeDelta = new Vector2(width, target.sizeDelta.y);
            yield return null;
        }

        // Reset to the original size
        target.sizeDelta = new Vector2(originalWidth, target.sizeDelta.y);
    }

    private IEnumerator LerpHeight(AnimationConfig config)
    {
        if (config == null) yield break;

        float time = 0;
        float originalHeight = target.sizeDelta.y; // Original width

        while (time < config.duration)
        {
            time += Time.deltaTime;
            float expansion = config.curve.Evaluate(time / config.duration) * config.scaleFactor;
            float height = originalHeight + expansion;
            target.sizeDelta = new Vector2(target.sizeDelta.x, height);
            yield return null;
        }

        // Reset to the original size
        target.sizeDelta = new Vector2(target.sizeDelta.x, originalHeight);
    }

    private void SetPosition(AnimationConfig config)
    {
        Vector2 originalPosition = target.anchoredPosition;
        Vector2 targetPosition = originalPosition + config.targetPositionOffset;
        target.anchoredPosition = targetPosition;
    }

    private IEnumerator LerpToPosition(AnimationConfig config)
    {
        if (config == null) yield break;

        Vector2 originalPosition = target.anchoredPosition;
        Vector2 targetPosition = originalPosition + config.targetPositionOffset;
        float time = 0;

        while (time < config.duration)
        {
            time += Time.deltaTime;
            target.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, config.curve.Evaluate(time / config.duration));
            yield return null;
        }
    }
}
