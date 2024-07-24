using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    public Text calendarText;
    public GameObject meetingObject;

    private int calendarTime = 0;


    public void AddMeeting()
    {
        calendarTime = Random.Range(10, 17);
        string x = TimeManager.Get().TimeString(calendarTime);
        calendarText.text = x;
    }

    public bool CheckMeetingStart(int time)
    {
        return time == calendarTime;
    }

    public void DisplayMeeting()
    {
        meetingObject.SetActive(true);
        TimeManager.Get().work.GetComponent<Button>().interactable = false;
    }

    public void EndMeeting()
    {
        meetingObject.SetActive(false);
        TimeManager.Get().work.GetComponent<Button>().interactable = true;
    }
}
