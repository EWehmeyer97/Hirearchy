using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationManager : MonoBehaviour
{
    [SerializeField] private List<Home> humans;
    [SerializeField] private List<Home> elfs;
    [SerializeField] private List<Home> orcs;
    [SerializeField] private List<Home> goblins;
    [SerializeField] private List<Home> gnomes;


    public bool checkNationality(Race x, Home home)
    {
        switch (x)
        {
            case Race.Human:
                return humans.Contains(home);
            case Race.Elf:
                return elfs.Contains(home);
            case Race.Orc:
                return orcs.Contains(home);
            case Race.Goblin:
                return goblins.Contains(home);
            case Race.Gnome:
                return gnomes.Contains(home);
            default:
                return false;
        }
    }
}
