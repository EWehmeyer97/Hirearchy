using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clothSwitch : MonoBehaviour
{
    public Color[] colors;
    public Sprite[] options;

    public int orcColorSpecial = -1;
    public int goblinColorSpecial = -1;
    public int gnomeColorSpecial = -1;

    public int optionSpecial = -1;

    public void chooseOption(Race race)
    {
        Image image = GetComponent<Image>();
        //Change Color
        if(colors.Length > 0)
        {
            if (race == Race.Orc && orcColorSpecial > -1)
            {
                image.color = colors[orcColorSpecial];
            } 
            else if (race == Race.Goblin && goblinColorSpecial > -1)
            {
                image.color = colors[goblinColorSpecial];
            } 
            else if (race == Race.Gnome && gnomeColorSpecial > -1)
            {
                image.color = colors[gnomeColorSpecial];
            } else
            {
                int length = colors.Length;
                if (orcColorSpecial > -1)
                {
                    length -= 1;
                }
                if (goblinColorSpecial > -1 && goblinColorSpecial != orcColorSpecial)
                {
                    length -= 1;
                }
                if (gnomeColorSpecial > -1 && gnomeColorSpecial != goblinColorSpecial && gnomeColorSpecial != orcColorSpecial)
                {
                    length -= 1;
                }

                image.color = colors[Random.Range(0, length)];
            }
        }

        //Change Option
        if (options.Length > 0)
        {
            if ((race == Race.Orc || race == Race.Goblin || race == Race.Gnome) && optionSpecial > -1)
            {
                image.sprite = options[optionSpecial];
            }
            else
            {
                int length = options.Length;
                if (optionSpecial > -1)
                {
                    length -= 1;
                }

                image.sprite = options[Random.Range(0, length)];
            }
        }
    }
}
