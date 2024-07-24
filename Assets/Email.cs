using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Email : MonoBehaviour
{
    [SerializeField] private Image sender;
    [SerializeField] private Text headText;
    [SerializeField] private Text bodyText;

    public Sprite profile;
    public string header;
    public string body;
    public bool yesOkay;
    public bool noOkay;
    public int cost;

    [System.NonSerialized] public bool answered = false;
    [System.NonSerialized] public bool attemptAnswer = false;


    public void Setup(Sprite pro, string head, string bod, bool yes, bool no, int cos)
    {
        profile = pro;
        header = head;
        body = bod;
        yesOkay = yes;
        noOkay = no;
        cost = cos;

        sender.sprite = profile;
        headText.text = header;
        bodyText.text = body.Substring(0, 33)+ "...";
    }
    
    public void respond(bool response)
    {
        attemptAnswer = true;

        if(response && yesOkay)
        {
            answered = true;
        } else if(!response && noOkay)
        {
            answered = true;
        } else
        {
            answered = false;
        }
    }

    public void DisplayEmail()
    {
        TimeManager.Get().email.ShowEmail(this);
    }
}
