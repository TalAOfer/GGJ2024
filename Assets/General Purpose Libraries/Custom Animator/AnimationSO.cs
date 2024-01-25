using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Animation Config")]
public class AnimationSO : ScriptableObject
{
    public List<AnimationConfig> configs;
}

[System.Serializable]
public class AnimationConfig
{
    public AnimationType type;
    public AnimationStyle executionStyle;
    
    [ShowIf("executionStyle", AnimationStyle.Lerp)]
    public float duration;

    [ShowIf("executionStyle", AnimationStyle.Lerp)]
    public AnimationCurve curve;

    [ShowIf("type", AnimationType.Position)]
    public Vector2 targetPositionOffset;

    [ShowIf("@ShouldShowExpansion()")]
    public float scaleFactor;

    private bool ShouldShowExpansion()
    {
        return (type == AnimationType.SizeY || type == AnimationType.SizeX);
    }
}


public enum AnimationType
{
    SizeY,
    SizeX,
    Position
}

public enum AnimationStyle
{
    Instant,
    Lerp
}
