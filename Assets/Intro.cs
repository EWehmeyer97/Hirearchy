using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text.text = "Day " + MoneyManager.Get().getDay().ToString() + "\n_____";
    }

    public void DeleteThis()
    {
        Destroy(gameObject);
    }
}
