using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CustomAnimator animator;
    [SerializeField] private AnimationConfig animConfig;

    private void Awake()
    {
        animator = GetComponent<CustomAnimator>();
        //Camera cam = Camera.main;
        //float screenHeightInUnits = cam.orthographicSize * 2; // orthographic size is half the screen height
        //float screenWidthInUnits = screenHeightInUnits * cam.aspect; // aspect ratio is width/height
        //Debug.Log("Screen width in world units: " + screenWidthInUnits);
    }

    public void MoveToNextRoom()
    {
        StartCoroutine(animator.LerpToPosition(animConfig));
    }
}
