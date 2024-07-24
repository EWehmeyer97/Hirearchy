using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfDay : MonoBehaviour
{
    public GameObject LoseScreen;
    public Text numbers;
    public void Activate()
    {
        //Update Numbers
        int num = MoneyManager.Get().totalMoney + MoneyManager.Get().madeToday - MoneyManager.Get().GetRent() - MoneyManager.Get().lostTodayFromEmail - MoneyManager.Get().lostTodayFromWrongCall - MoneyManager.Get().lostTodayFromMissingMeeting;

        string x = MoneyManager.Get().totalMoney.ToString() + '\n' + MoneyManager.Get().madeToday.ToString() + '\n' + MoneyManager.Get().GetRent().ToString() + '\n' +
            MoneyManager.Get().lostTodayFromEmail.ToString() + '\n' + MoneyManager.Get().lostTodayFromWrongCall.ToString() + '\n' + MoneyManager.Get().lostTodayFromMissingMeeting.ToString() + '\n' + "--\n\n" + num.ToString();

        numbers.text = x;

        MoneyManager.Get().totalMoney = num;
        MoneyManager.Get().madeToday = 0;
        MoneyManager.Get().lostTodayFromEmail = 0;
        MoneyManager.Get().lostTodayFromWrongCall = 0;
        MoneyManager.Get().lostTodayFromMissingMeeting = 0;

        //Increment Day
        MoneyManager.Get().nextDay();
    }

    public void moveOn()
    {
        if (MoneyManager.Get().totalMoney >= 0)
        {
            SceneManager.LoadScene(1);
        } else
        {
            LoseScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
