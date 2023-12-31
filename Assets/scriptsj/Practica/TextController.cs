using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    TMP_Text timeText, scoreText, altText;
    void Awake()
    {
        timeText = transform.Find("time").gameObject.GetComponent<TMP_Text>();
        scoreText = transform.Find("score").gameObject.GetComponent<TMP_Text>();
        altText = transform.Find("alt text").gameObject.GetComponent<TMP_Text>();
    }

    private void Start()
    {
        timeText.text = "Time: 0";
        scoreText.text = "Score: 0";
        
    }
    // Update is called once per frame
    void Update()
    {
        //actualiza el tiempo
        timeText.text = $"Time: {string.Format("{0:0.##}", GameManager.instance._time)}";

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            altText.text = "";
        }
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
        //actualiza el texto solo, no la variable del gamemanager
        scoreText.text = $"Score: {GameManager.instance._score}";
        while (count <= 1)
        {
            scoreText.color = new Color(1f, 1f, 1f, count);
            yield return new WaitForSeconds(1 / 100);
            count += 1 / 60f;
        }
    }

    
}
