using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private string tagName = "Player";
    [SerializeField] private bool onEnter = true;
    [ShowIf("onEnter")]
    public CustomGameEvent enterResponse;
    [SerializeField] private bool onExit;
    [ShowIf("onExit")]
    public CustomGameEvent exitResponse;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            enterResponse.Invoke(this, collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            exitResponse.Invoke(this, collision.gameObject);
        }
    }
}
