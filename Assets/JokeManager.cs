using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokeManager : MonoBehaviour
{
    public float jokeLength;
    public GameObject barContainer;
    public Image bar;
    public TextMeshProUGUI jokeText;
    public CustomGameEvent OnJokeFinished;

    private Coroutine joke;

    public void InitiateJoke()
    {
        barContainer.SetActive(true);
        joke = StartCoroutine(JokeRoutine());
    }

    public void StopJoke()
    {
        if (joke != null) 
        {
            StopCoroutine(joke);
            barContainer.SetActive(false);
        }
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
