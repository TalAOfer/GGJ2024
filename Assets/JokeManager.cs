using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokeManager : MonoBehaviour
{
    public float jokeLength;
    public GameObject speechBubble;
    public Image bar;
    public TextMeshProUGUI jokeText;
    public CustomGameEvent OnJokeFinished;
    public void InitiateJoke()
    {
        speechBubble.SetActive(true);
        StartCoroutine(JokeRoutine());
    }

    private IEnumerator JokeRoutine()
    {
        float timer = 0;
        while (timer < jokeLength)
        {
            timer += Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(0, 1, timer / jokeLength);
            yield return null;
        }

        jokeText.text = "finished";
        OnJokeFinished.Invoke(this, null);
    }
}
