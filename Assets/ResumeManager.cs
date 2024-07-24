using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeManager : MonoBehaviour
{
    public CandidateUpdate candid;
    public EndOfDay eod;

    public Text placed;
    private int placeNum = 0;

    public void RateCandidate(int x)
    {
        //Place Candidate in Correct job to look at well
        if(candid.candidate == null)
        {
            return;
        }
        if(x < 4)
        {
            int money = CheckCandidate(new Applications(candid.candidate, (Role)x, TimeManager.Get().rules.theRules));

            if(money > 0) { MoneyManager.Get().madeToday += money; } 
            else { MoneyManager.Get().lostTodayFromWrongCall -= money; StartCoroutine(TimeManager.Get().email.PlacedWrong(candid.candidate.firstName + " " + candid.candidate.lastName, (Role)x)); }
            placeNum++;
            placed.text = placeNum.ToString();

        } else
        {
            for (int q = 3; q >= 0; q--)
            {
                int b = CheckCandidate(new Applications(candid.candidate, (Role)q, TimeManager.Get().rules.theRules));

                if(b > 0)
                {
                    MoneyManager.Get().lostTodayFromWrongCall += b;
                    StartCoroutine(TimeManager.Get().email.RejectedWrong(candid.candidate.firstName + " " + candid.candidate.lastName, (Role)q));
                    break;
                }
            }
        }

        //Clear and Replace
        candid.ClearCandidate();

        if(TimeManager.Get().time < 32 && TimeManager.Get().work.avail)
        {
            StartCoroutine(candid.UpdateCandidate());
        } else if (TimeManager.Get().time >= 32)
        {
            eod.gameObject.SetActive(true);
            eod.Activate();
        }
    }

    int CheckCandidate(Applications x)
    {
        //Get years worked for
        int years = 0;
        for (int j = 0; j < x.candid.works.Length; j++)
        {
            years += x.candid.works[j].years;
        }

        //Get Money at stake
        int earn = MoneyManager.Get().value[x.selection];

        //Find correct qualifications to check
        CharClass qual;
        switch (x.selection)
        {
            case Role.Knight:
                qual = x.rule.knight;
                break;
            case Role.Wizard:
                qual = x.rule.wizard;
                break;
            case Role.Bard:
                qual = x.rule.bard;
                break;
            default:
                qual = x.rule.paladin;
                break;
        }

        //Check Qualifications and Nationality
        bool success = CompareQualifications(x.candid.apt, qual.apt, years, qual.years) && TimeManager.Get().nations.checkNationality(x.candid.race, x.candid.hometown);

        if (!success) { earn *= -1; }

        return earn;
    }

    bool CompareQualifications(Aptitude can, Aptitude req, int years, int qual)
    {
        //TODO - Add support for additional rules
        return (can.strength >= req.strength && can.magic >= req.magic && can.intellect >= req.intellect && can.charisma >= req.charisma && years >= qual);
    }
}

public class Applications
{
    public Candidate candid;
    public Role selection;
    public Rules rule;

    public Applications(Candidate c, Role s, Rules r)
    {
        candid = c;
        selection = s;
        rule = r;
    }
}
