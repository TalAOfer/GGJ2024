using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathSceneManager : MonoBehaviour
{
    public CustomGameEvent OnFinishedDeathAnimation;
    public GameEvent ShakeScreen;
    public RectTransform face;
    [SerializeField] private AnimationSO faceRightSO;
    [SerializeField] private AnimationSO faceMiddleSO;
    [SerializeField] private UICustomAnimator faceAnimator;

    public RectTransform TopBlock;
    [SerializeField] private AnimationSO topBlockInSO;
    [SerializeField] private AnimationSO topBlockOutSO;
    [SerializeField] private UICustomAnimator topBlockAnimator;

    public RectTransform BottomBlock;
    [SerializeField] private AnimationSO bottomBlockInSO;
    [SerializeField] private AnimationSO bottomBlockOutSO;
    [SerializeField] private UICustomAnimator bottomBlockAnimator;

    public void StartOnDeathAnimation()
    {
        StartCoroutine(DeathRoutine());
    }

    public IEnumerator DeathRoutine()
    {
        topBlockAnimator.StartAnimation(topBlockInSO);
        bottomBlockAnimator.StartAnimation(bottomBlockInSO);
        faceAnimator.StartAnimation(faceRightSO);
        ShakeScreen.Raise(this, CameraShakeTypes.Roar);

        yield return new WaitForSeconds(1.25f);

        faceAnimator.StartAnimation(faceMiddleSO);

        yield return new WaitForSeconds(1.25f);

        topBlockAnimator.StartAnimation(topBlockOutSO);
        bottomBlockAnimator.StartAnimation(bottomBlockOutSO);
        faceAnimator.StartAnimation(faceRightSO);

        yield return new WaitForSeconds(1.25f);

        OnFinishedDeathAnimation.Invoke(this, null);
    }

}
