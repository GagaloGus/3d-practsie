using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColorChange : MonoBehaviour
{
    Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(ChangeCol());
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
}
