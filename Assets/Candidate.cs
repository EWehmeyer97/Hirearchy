using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class Candidate
{
    public string firstName;
    public string lastName;
    public Race race;
    public Home hometown;

    public Aptitude apt;

    public Work[] works;

    public Candidate(string first, string last, Race ethnic, Home place, int str, int mag, int inte, int cha, Work[] jobs)
    {
        firstName = first;
        lastName = last;
        race = ethnic;
        hometown = place;
        apt = new Aptitude(str, mag, inte, cha);

        works = jobs;
    }
}

public enum Race
{
    [Description("Human")]
    Human,

    [Description("Elf")]
    Elf,

    [Description("Orc")]
    Orc,

    [Description("Goblin")]
    Goblin,

    [Description("Gnome")]
    Gnome
}

public enum Home
{

    [Description("Lonwi")]
    Lonwi,

    [Description("Weto")]
    Weto,

    [Description("Yamao")]
    Yamao,

    [Description("Mentai")]
    Mentai,

    [Description("Cannach")]
    Cannach,

    [Description("Fleanor")]
    Fleanor
}

public class Aptitude
{
    public int strength;
    public int magic;
    public int intellect;
    public int charisma;

    public Aptitude(int s, int m, int i, int c)
    {
        strength = s;
        magic = m;
        intellect = i;
        charisma = c;
    }
}

public class Work
{
    public Role role;
    public Employer employer;
    public int years;

    public Work(Role rol, Employer emp, int num)
    {
        role = rol;
        employer = emp;
        years = num;
    }
}

public enum Role
{
    [Description("Knight")]
    Knight,

    [Description("Wizard")]
    Wizard,

    [Description("Bard")]
    Bard,

    [Description("Paladin")]
    Paladin
}

public enum ColorDie
{
    [Description("Red")]
    Red,

    [Description("Purple")]
    Purple,

    [Description("Green")]
    Green,

    [Description("Blue")]
    Blue
}

public enum Employer
{
    [Description("Valor Castle")]
    Valor,

    [Description("Redvale Fields")]
    Redvale,

    [Description("Helix Dome")]
    Helix,

    [Description("Blueridge Guild")]
    Blueridge,

    [Description("Tower's Peak")]
    Tower,

    [Description("Clawthorne Castle")]
    Clawthorne
}

public static class EnumHelper
{
    public static string GetDescription<T>(this T enumValue)
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            return null;

        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo != null)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return description;
    }
}
