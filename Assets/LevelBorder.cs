using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBorder : MonoBehaviour
{
    public CustomGameEvent response;
    public void OnPlayerExit(Component sender, object data)
    {
        GameObject collidedGO = (GameObject) data;
        if (collidedGO.transform.position.x > transform.position.x)
        {
            response.Invoke(this, null);
        }
    }
}
