using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayOneEmail : MonoBehaviour
{
    public int num = 1;
    private void Start()
    {
        if(MoneyManager.Get().getDay() != num)
        {
            Destroy(gameObject);
        }
    }
}
