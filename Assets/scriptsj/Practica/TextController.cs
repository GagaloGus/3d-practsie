using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    TMP_Text timeText, scoreText;
    void Start()
    {
        timeText = transform.Find("time").gameObject.GetComponent<TMP_Text>();
        scoreText = transform.Find("score").gameObject.GetComponent<TMP_Text>();

        timeText.text = "Time: 0";
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        //actualiza el tiempo
        timeText.text = $"Time: {string.Format("{0:0.##}", GameManager.instance._time)}";
    }

    public void StartScoreFade()
    {
        StartCoroutine(FadeinFadeoutText());
    }

    IEnumerator FadeinFadeoutText()
    {
        float count = 1;
        while (count >= 0)     
        {
            scoreText.color = new Color(1f, 1f, 1f, count);
            yield return new WaitForSeconds(1/100);
            count -= 1 / 60f;
        }
        scoreText.text = $"Score: {GameManager.instance._score}";
        while (count <= 1)
        {
            scoreText.color = new Color(1f, 1f, 1f, count);
            yield return new WaitForSeconds(1 / 100);
            count += 1 / 60f;
        }
    }
}
