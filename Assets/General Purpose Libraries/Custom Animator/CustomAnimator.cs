using System.Collections;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 defaultScale;
    private Vector3 defaultPosition;

    private void Awake()
    {
        defaultScale = target.localScale;
        defaultPosition = target.localPosition;
    }

    public void StartAnimation(AnimationSO animationSO)
    {
        StopAllCoroutines();
        ResetScaleToDefault();
        ResetPositionToDefault();

        foreach (AnimationConfig config in animationSO.configs)
        {
            switch (config.type)
            {
                case AnimationType.SizeY:
                    if (config.executionStyle is AnimationStyle.Instant) SetScale(config, false);
                    else StartCoroutine(LerpScale(config, false));
                    break;
                case AnimationType.SizeX:
                    if (config.executionStyle is AnimationStyle.Instant) SetScale(config, true);
                    else StartCoroutine(LerpScale(config, true));
                    break;
                case AnimationType.Position:
                    if (config.executionStyle is AnimationStyle.Instant) SetPosition(config);
                    else StartCoroutine(LerpToPosition(config));
                    break;
            }
        }
    }

    private void ResetScaleToDefault()
    {
        target.localScale = defaultScale;
    }

    private void ResetPositionToDefault()
    {
        target.localPosition = defaultPosition;
    }

    public void SetScale(AnimationConfig config, bool isWidth)
    {
        Vector3 newScale = isWidth ? new Vector3(defaultScale.x * config.scaleFactor, defaultScale.y, defaultScale.z)
                           : new Vector3(defaultScale.x, defaultScale.y * config.scaleFactor, defaultScale.z);
        target.localScale = newScale;
    }

    public IEnumerator LerpScale(AnimationConfig config, bool isWidth)
    {
        if (config == null) yield break;

        float time = 0;
        Vector3 originalScale = target.localScale;
        Vector3 targetScale = isWidth ? new Vector3(originalScale.x * config.scaleFactor, originalScale.y, originalScale.z)
                                      : new Vector3(originalScale.x, originalScale.y * config.scaleFactor, originalScale.z);

        while (time < config.duration)
        {
            time += Time.deltaTime;
            target.localScale = Vector3.Lerp(originalScale, targetScale, config.curve.Evaluate(time / config.duration));
            yield return null;
        }

        target.localScale = originalScale;
    }

    private void SetPosition(AnimationConfig config)
    {
        Vector3 targetPosition = defaultPosition + (Vector3)config.targetPositionOffset;
        target.localPosition = targetPosition;
    }

    public IEnumerator LerpToPosition(AnimationConfig config)
    {
        if (config == null) yield break;

        Vector3 originalPosition = target.localPosition;
        Vector3 targetPosition = originalPosition + (Vector3)config.targetPositionOffset;
        float time = 0;

        while (time < config.duration)
        {
            time += Time.deltaTime;
            target.localPosition = Vector3.Lerp(originalPosition, targetPosition, config.curve.Evaluate(time / config.duration));
            yield return null;
        }
    }
}