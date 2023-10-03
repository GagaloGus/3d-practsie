using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColorChange : MonoBehaviour
{
    Renderer rend;
    public int score;
    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(ChangeCol());
        StartCoroutine(ChangePos());
    }

    

    IEnumerator ChangeCol()
    {
        float count = 0;
        while(count < 1)
        {
            Color rainbow = Color.HSVToRGB(count, 1, 1);

            rend.material.SetColor("_Color", rainbow);
            yield return new WaitForSeconds(1 / 360);

            count += 1 / 360f;
        }
        StartCoroutine(ChangeCol());

    }
    IEnumerator ChangePos()
    {
        float count = -0.5f;
        while (count < 0.5)
        {
            transform.position = transform.position + Vector3.up * count/4 * 0.01f;
            yield return new WaitForSeconds(1 / 360);

            count += 1 / 360f;
        }

        while (count > -0.5)
        {
            transform.position = transform.position + Vector3.up * count/4 * 0.001f;
            yield return new WaitForSeconds(1 / 360);

            count -= 1 / 360f;
        }
        StartCoroutine(ChangePos());

    }
}
