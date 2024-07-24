using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private static TimeManager oneAndOnly;

    public Text clock;
    public WorkSwitch work;
    public EmailManager email;
    public RulesManager rules;
    public NationManager nations;
    public CalendarManager calendar;
    public EndOfDay eod;

    private string[] hour = { " 9", "10", "11", "12", " 1", " 2", " 3", " 4", " 5" };
    private string[] min = { "00", "15", "30", "45" };
    [System.NonSerialized] public int time = 0;

    private int[] supervisor = { 0, 0, 0 };
    private int supNum = 0;

    private void Awake()
    {
        oneAndOnly = this;
    }

    public static TimeManager Get()
    {
        return oneAndOnly;
    }
    // Start is called before the first frame update
    void Start()
    {
        clock.text = hour[time / 4] + ":" + min[time % 4];
    }

    public void StartTime()
    {
        StartCoroutine(TimeMove());
    }

    public IEnumerator TimeMove()
    {
        yield return new WaitForSeconds(10);
        time++;
        clock.text = hour[time / 4] + ":" + min[time % 4];
        if (time < 32)
        {
            StartCoroutine(TimeMove());
            email.UpdateEmail(time);

            //Check Calendar for Meetings
            if (calendar.CheckMeetingStart(time))
            {
                if (work.avail == false && work.caller.candidate == null)
                {
                    calendar.DisplayMeeting();
                } else
                {
                    MoneyManager.Get().lostTodayFromMissingMeeting += 8;
                }
            } else
            {
                calendar.EndMeeting();
            }
        } else
        {
            work.Available(false);
            if(work.caller.candidate == null)
            {
                eod.gameObject.SetActive(true);
                eod.Activate();
            }
        }

        //Changing supervisors
        switch (time)
        {
            case 4:
                supNum = 1;
                break;
            case 8:
                supNum = 2;
                break;
            case 16:
                supNum = 1;
                break;
            case 20:
                supNum = 2;
                break;
            case 24:
                supNum = 0;
                break;
            case 28:
                supNum = 1;
                break;
            default:
                break;
        }
    }

    public bool AddToSupervisor()
    {
        return ++supervisor[supNum] > 3;
    }

    public string TimeString(int check = -1)
    {
        string x;
        if (check == -1)
        {
            x = hour[time / 4] + ":" + min[time % 4];
            if (time > 11)
            {
                x = x + "pm";
            } else
            {
                x = x + "am";
            }
        } else
        {
            x = hour[check / 4] + ":" + min[check % 4];
            if (check > 11)
            {
                x = x + "pm";
            }
            else
            {
                x = x + "am";
            }
        }

        return x;
    }
}
