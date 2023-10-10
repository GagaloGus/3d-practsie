using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwapJSONTXT : MonoBehaviour
{
    public bool value;
    TMP_Text awesomeText;

    private void Start()
    {
        awesomeText = transform.Find("boton").gameObject.transform.Find("texto").gameObject.GetComponent<TMP_Text>();
    }
    public void swap()
    {
        value = !value;
        if(value)
        {
            awesomeText.text = "TXT";
        }
        else
        {
            awesomeText.text = "JSON";
        }
    }
}
