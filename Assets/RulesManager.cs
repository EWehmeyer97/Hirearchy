using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesManager : MonoBehaviour
{
    public Rules theRules;

    [SerializeField] private Transform rulesHolder;

    private void Start()
    {
        CreateNewRules();
    }

    public void CreateNewRules()
    {
        CharClass knight = new CharClass(new Aptitude(5, 1, 1, 3), 0);
        CharClass wizard = new CharClass(new Aptitude(1, 5, 5, 1), 4);
        CharClass bard = new CharClass(new Aptitude(1, 1, 3, 5), 0);
        CharClass paladin = new CharClass(new Aptitude(5, 4, 3, 3), 8);

        theRules = new Rules(knight, wizard, bard, paladin);

        UpdateVisuals(0, knight);
        UpdateVisuals(1, wizard);
        UpdateVisuals(2, bard);
        UpdateVisuals(3, paladin);
    }

    void UpdateVisuals(int x, CharClass y)
    {
        rulesHolder.GetChild(x).GetChild(0).GetChild(0).GetComponent<Text>().text = y.apt.strength.ToString();
        rulesHolder.GetChild(x).GetChild(1).GetChild(0).GetComponent<Text>().text = y.apt.magic.ToString();
        rulesHolder.GetChild(x).GetChild(2).GetChild(0).GetComponent<Text>().text = y.apt.intellect.ToString();
        rulesHolder.GetChild(x).GetChild(3).GetChild(0).GetComponent<Text>().text = y.apt.charisma.ToString();
        rulesHolder.GetChild(x).GetChild(4).GetComponent<Text>().text = "("+y.years.ToString()+" years)";
    }

    public void AddNewRule(int i)
    {
        Aptitude x;
        switch(i / 4)
        {
            case 0:
                x = theRules.knight.apt;
                break;
            case 1:
                x = theRules.wizard.apt;
                break;
            case 2:
                x = theRules.bard.apt;
                break;
            default:
                x = theRules.paladin.apt;
                break;
        }

        switch (i % 4)
        {
            case 0:
                x.strength++;
                break;
            case 1:
                x.magic++;
                break;
            case 2:
                x.intellect++;
                break;
            default:
                x.charisma++;
                break;
        }

        switch (i / 4)
        {
            case 0:
                theRules.knight.apt = x;
                break;
            case 1:
                theRules.wizard.apt = x;
                break;
            case 2:
                theRules.bard.apt = x;
                break;
            default:
                theRules.paladin.apt = x;
                break;
        }

        UpdateVisuals(0, theRules.knight);
        UpdateVisuals(1, theRules.wizard);
        UpdateVisuals(2, theRules.bard);
        UpdateVisuals(3, theRules.paladin);
    }
}

public class Rules
{
    public CharClass knight;
    public CharClass wizard;
    public CharClass bard;
    public CharClass paladin;

    public Rules(CharClass k, CharClass w, CharClass b, CharClass p)
    {
        knight = k;
        wizard = w;
        bard = b;
        paladin = p;
    }
}

public class CharClass
{
    public Aptitude apt;
    public int years;
    public Race[] racistAgainst;
    public Home[] cantLive;
    public int[] cantBackground;

    public CharClass(Aptitude aptTest, int num, Race[] races = null, Home[] homes = null, int[] backs = null)
    {
        apt = aptTest;
        years = num;
        racistAgainst = races;
        cantLive = homes;
        cantBackground = backs;
    }
}
