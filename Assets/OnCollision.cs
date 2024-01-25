using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private string tagName = "Player";
    public CustomGameEvent response;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            response.Invoke(this, null);
        }
    }
}
