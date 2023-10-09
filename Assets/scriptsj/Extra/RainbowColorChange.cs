using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColorChange : MonoBehaviour
{
    Renderer rend;
    public int score;
    float multiplier = 0.07f;
    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(ChangeCol());
        StartCoroutine(ChangePos());

        transform.position = transform.position + Vector3.up/2.5f;
        GetComponent<Rigidbody>().AddForce(Vector3.up * multiplier * -1 * 180);

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
        float count = 0;
        
        while (count < 1)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * multiplier);
            yield return new WaitForSeconds(1 / 360);

            count += 1 / 360f;
        }
        while (count > 0)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * multiplier * -1);
            yield return new WaitForSeconds(1 / 360);

            count -= 1 / 360f;
        }

        StartCoroutine(ChangePos());

    }
}
