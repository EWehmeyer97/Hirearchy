using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EmailManager : MonoBehaviour
{
    public Sprite[] emailProfiles;

    public GameObject emailObject;
    public GameObject emailList;

    public GameObject emailFull;
    public Text emailHeader;
    public Text emailBody;
    public GameObject emailDelete;

    private Dictionary<int, List<Email>> emails = new Dictionary<int, List<Email>>();
    private int rulesAdded = 0;
    private int questionsAsked = 0;
    private bool meetingScheduled = false;

    private Email displayEmail;

    private void UpdateDot()
    {
        transform.GetChild(1).gameObject.SetActive(GetComponent<Button>().interactable);
    }

    public void UpdateEmail(int time)
    {
        //Check unresponsive emails
        if (emails.ContainsKey(time))
        {
            foreach (Email mail in emails[time])
            {
                bool x = mail.gameObject.activeSelf;
                mail.gameObject.SetActive(true);
                if (!mail.answered)
                {
                    MoneyManager.Get().lostTodayFromEmail += mail.cost;
                }
                if (!x)
                {
                    Destroy(mail.gameObject);
                }
            }
        }

        //Create New Emails
        //New Rule
        if(rulesAdded < 5 && time < 29)
        {
            if (Random.Range(0, 10) >= 8)
            {
                GameObject x = Instantiate(emailObject, emailList.transform);
                Email y = x.GetComponent<Email>();
                int rand = Random.Range(0, 16);
                TimeManager.Get().rules.AddNewRule(rand);
                y.Setup(emailProfiles[0], "New Rule", ((Role)(rand/4)).GetDescription() + " must meet new requirements to be hired. The required value of the "+ ((ColorDie)(rand % 4)).GetDescription() + " has increased by 1 point. Please confirm you understand.", true, false, 2);
                int res = time + Random.Range(3, 5);
                SetupEmailList(res);
                emails[res].Add(y);
                rulesAdded++;
                UpdateDot();
            }
        }
        //Question
        if(questionsAsked < 10 && time < 26)
        {
            if (Random.Range(0, 10) >= 6)
            {
                GameObject x = Instantiate(emailObject, emailList.transform);
                Email y = x.GetComponent<Email>();
                int rand = Random.Range(0, 4);
                if (rand == 0)
                {
                    y.Setup(emailProfiles[1], "A Quick Question", "Hey, quick question for you about something specific. I've been hearing a lot of rumors about layoffs. Have you heard anything?", true, true, 3);
                } else if (rand == 1)
                {
                    y.Setup(emailProfiles[1], "A Quick Question", "Hey, quick question for you about something specific. Do Paladins make us more money than Knights? I'd look at the CALENDAR tab myself, but I don't want to.", true, false, 3);
                } else if (rand == 2)
                {
                    y.Setup(emailProfiles[1], "A Quick Question", "Hey, quick question for you about something specific. Do bards need experience to work here with us?", false, true, 3);
                } else if (rand == 3)
                {
                    y.Setup(emailProfiles[1], "A Quick Question", "Hey, quick question for you about something specific. Do/did we have a meeting today? I didn't get an email and was concerned.", meetingScheduled, !meetingScheduled, 3);
                } else if (rand == 4)
                {
                    y.Setup(emailProfiles[1], "A Quick Question", "Hey, quick question for you about something specific. Wizards need more job experience than Paladins, right?", false, true, 3);
                }
                int res = time + Random.Range(3, 5);
                SetupEmailList(res);
                emails[res].Add(y);
                questionsAsked++;
                UpdateDot();
            }
        }
        //Meeting Scheduled
        if (!meetingScheduled && time < 6)
        {
            if(Random.Range(0, 10) >= 7)
            {
                GameObject x = Instantiate(emailObject, emailList.transform);
                Email y = x.GetComponent<Email>();
                y.Setup(emailProfiles[2], "Company Meeting", "A Company wide virtual meeting has been scheduled for today. Please look at your calendar for more information. Attendance is required. Please make sure that you're switch is set to NOT AVAILABLE to candidates and respond to let me know that you are coming.", true, false, 5);
                TimeManager.Get().calendar.AddMeeting();
                SetupEmailList(time + 4);
                emails[time + 4].Add(y);
                meetingScheduled = true;
                UpdateDot();
            }
        }
    }

    public IEnumerator RejectedWrong(string name, Role role)
    {
        yield return new WaitForSeconds(2);
        GameObject x = Instantiate(emailObject, emailList.transform);
        Email y = x.GetComponent<Email>();
        y.Setup(emailProfiles[2], "Incorrect Rejection", "While checking your work, a supervisor noticed that you rejected " + name + " when they were qualified to be a " + role.GetDescription() + ". A penalty has been applied to your pay.", true, false, 5);
        UpdateDot();
    }

    public IEnumerator PlacedWrong(string name, Role role)
    {
        yield return new WaitForSeconds(2);
        GameObject x = Instantiate(emailObject, emailList.transform);
        Email y = x.GetComponent<Email>();
        y.Setup(emailProfiles[2], "Incorrect Placement", "While checking your work, a supervisor noticed that you placed " + name + " in the role of " + role.GetDescription() + ". On further inspection, they were not qualified. A penalty has been applied to your pay.", true, false, 5);
        UpdateDot();
    }

    public IEnumerator TooMuchEdit()
    {
        yield return new WaitForSeconds(2);
        GameObject x = Instantiate(emailObject, emailList.transform);
        Email y = x.GetComponent<Email>();
        y.Setup(emailProfiles[2], "Disciplinary Action", "A supervisor noticed that you have been using the edit feature. This is only meant for managers. A severe penalty has been applied to your pay.", true, false, 5);
        UpdateDot();
    }

    private void SetupEmailList(int x)
    {
        if(!emails.ContainsKey(x))
        {
            emails.Add(x, new List<Email>());
        }
    }

    public void ShowEmail(Email set)
    {
        displayEmail = set;

        emailFull.SetActive(true);
        emailHeader.text = displayEmail.header;
        emailBody.text = displayEmail.body;

        emailDelete.SetActive(displayEmail.attemptAnswer);
    }

    public void EmailRespond(bool x)
    {
        displayEmail.respond(x);
        emailDelete.SetActive(displayEmail.attemptAnswer);
    }

    public void DeleteEmail()
    {
        displayEmail.gameObject.SetActive(false);
        emailFull.SetActive(false);
        displayEmail = null;
    }
}
