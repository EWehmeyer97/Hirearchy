using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;

public class CandidateUpdate : MonoBehaviour
{
    [Header("Files")]
    public TextAsset firstNames;
    public TextAsset lastNames;
    public Sprite[] backgrounds;

    [Header("Video Screen")]
    public Text nameplate;
    public Image background;
    public GameObject blocker;

    [Header("Resume")]
    public Text rName;
    public Text rRace;
    public Text rHome;
    public Text rStr;
    public Text rMag;
    public Text rInt;
    public Text rCha;
    public GameObject[] work;
    

    [System.NonSerialized] public Candidate candidate = null;
    [System.NonSerialized] public int backgroundNum = 0;
    private string[] firstNameList;
    private string[] lastNameList;

    private void Start()
    {
        firstNameList = firstNames.text.Split('\n');
        lastNameList = lastNames.text.Split('\n');
    }

    public IEnumerator UpdateCandidate()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        //Create Work Experience
        int job = Random.Range(0, 31);
        if(job % 2 == 0)
        {
            job = 2;
        } else if (job % 3 == 0)
        {
            job = 3;
        } else
        {
            job = 1;
        }
        Work[] jobs = new Work[job];
        for(int i = 0; i < job; i++)
        {
            jobs[i] = new Work((Role)Random.Range(0, 4), (Employer)Random.Range(0, 6), Random.Range(1, 8));
        }

        //Create Candidate
        candidate = new Candidate(firstNameList[Random.Range(0, firstNameList.Length)], lastNameList[Random.Range(0, lastNameList.Length)], (Race)Random.Range(0, 5), (Home)Random.Range(0, 6), Random.Range(1, 9), Random.Range(1, 9), Random.Range(1, 9), Random.Range(1, 9), jobs);

        //Update visuals
        //Video Screen
        blocker.SetActive(false);
        backgroundNum = Random.Range(0, backgrounds.Length);
        background.sprite = backgrounds[backgroundNum];
        nameplate.text = candidate.firstName + " " + candidate.lastName;
        foreach(clothSwitch cloth in GetComponentsInChildren<clothSwitch>())
        {
            cloth.chooseOption(candidate.race);
        }

        //Resume
        rName.text = candidate.firstName + " " + candidate.lastName;
        rRace.text = candidate.race.GetDescription();
        rHome.text = candidate.hometown.GetDescription();
        rStr.text = candidate.apt.strength.ToString();
        rMag.text = candidate.apt.magic.ToString();
        rInt.text = candidate.apt.intellect.ToString();
        rCha.text = candidate.apt.charisma.ToString();

        //work
        for(int i = 0; i < jobs.Length; i++)
        {
            work[i].SetActive(true);
            work[i].GetComponent<Text>().text = jobs[i].role.GetDescription();
            work[i].transform.GetChild(0).GetComponent<Text>().text = "At " + jobs[i].employer.GetDescription();
            work[i].transform.GetChild(1).GetComponent<Text>().text = jobs[i].years.ToString() + " years";
        }
    }

    public void RerollCandidate(int x)
    {
        int y = 0;
        switch (x)
        {
            case 0:
                y = candidate.apt.strength;
                while (y <= candidate.apt.strength)
                {
                    candidate.apt.strength = Random.Range(1, 9);
                }
                rStr.text = candidate.apt.strength.ToString();
                break;
            case 1:
                y = candidate.apt.magic;
                while (y <= candidate.apt.magic)
                {
                    candidate.apt.magic = Random.Range(1, 9);
                }
                rMag.text = candidate.apt.magic.ToString();
                break;
            case 2:
                y = candidate.apt.intellect;
                while (y <= candidate.apt.intellect)
                {
                    candidate.apt.intellect = Random.Range(1, 9);
                }
                rInt.text = candidate.apt.intellect.ToString();
                break;
            case 3:
                y = candidate.apt.charisma;
                while (y <= candidate.apt.charisma)
                {
                    candidate.apt.charisma = Random.Range(1, 9);
                }
                rCha.text = candidate.apt.charisma.ToString();
                break;
            case 4:
                int job = Random.Range(0, 31);
                if (job % 2 == 0)
                {
                    job = 2;
                }
                else if (job % 3 == 0)
                {
                    job = 3;
                }
                else
                {
                    job = 1;
                }
                Work[] jobs = new Work[job];
                for (int i = 0; i < job; i++)
                {
                    jobs[i] = new Work((Role)Random.Range(0, 4), (Employer)Random.Range(0, 6), Random.Range(1, 8));
                }
                candidate.works = jobs;

                for (int i = 0; i < work.Length; i++)
                {
                    work[i].SetActive(false);
                }

                for (int i = 0; i < jobs.Length; i++)
                {
                    work[i].SetActive(true);
                    work[i].GetComponent<Text>().text = jobs[i].role.GetDescription();
                    work[i].transform.GetChild(0).GetComponent<Text>().text = "At " + jobs[i].employer.GetDescription();
                    work[i].transform.GetChild(1).GetComponent<Text>().text = jobs[i].years.ToString() + " years";
                }

                break;
            default:
                Home prev = candidate.hometown;
                while (prev == candidate.hometown)
                {
                    candidate.hometown = (Home)Random.Range(0, 6);
                }
                rHome.text = candidate.hometown.GetDescription();
                break;
        }

        if (TimeManager.Get().AddToSupervisor())
        {
            StartCoroutine(TimeManager.Get().email.TooMuchEdit());
            MoneyManager.Get().lostTodayFromWrongCall += 10;
        }
    }

    public void ClearCandidate()
    {
        //Update visuals
        //Video Screen
        blocker.SetActive(true);
        //background.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
        nameplate.text = "";

        //Resume
        rName.text = "";
        rRace.text = "";
        rHome.text = "";
        rStr.text = "";
        rMag.text = "";
        rInt.text = "";
        rCha.text = "";

        //work
        for (int i = 0; i < work.Length; i++)
        {
            work[i].SetActive(false);
        }

        candidate = null;
    }
}
