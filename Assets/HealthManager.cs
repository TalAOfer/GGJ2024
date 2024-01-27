using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public List<Image> hearts;

    private void Awake()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(true);
        }
    }

    public void RemoveHeart(Component sender, object data)
    {
        int amount = (int)data;
        hearts[amount].gameObject.SetActive(false);
    }

}
