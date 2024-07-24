using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private static MoneyManager oneAndOnly;
    public static MoneyManager Get()
    {
        return oneAndOnly;
    }
    private void Awake()
    {
        if(oneAndOnly != null)
        {
            DestroyImmediate(gameObject);
        } else
        {
            oneAndOnly = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [Header ("Money")]
    public int totalMoney = 0;
    public int madeToday = 0;
    public int lostTodayFromEmail = 0;
    public int lostTodayFromWrongCall = 0;
    public int lostTodayFromMissingMeeting = 0;

    [Header ("Days")]
    private int day = 0;
    public int[] rentCost;

    [System.NonSerialized] public Dictionary<Role, int> value = new Dictionary<Role, int>();

    private void Start()
    {
        value.Add(Role.Knight, 3);
        value.Add(Role.Wizard, 5);
        value.Add(Role.Bard, 2);
        value.Add(Role.Paladin, 7);
    }

    public int GetRent()
    {
        if(day < rentCost.Length)
        {
            return rentCost[day];
        }
        return rentCost[rentCost.Length - 1];
    }

    public void nextDay()
    {
        day++;
    }

    public int getDay()
    {
        return day + 1;
    }
}
