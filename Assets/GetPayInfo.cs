using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPayInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Email>().body = "Thank you for staying with Lock's Inn. Your bill today is " + MoneyManager.Get().GetRent().ToString() +" gold. We expect you to pay by the end of the day or will be forced to evict. Please let us know how we can improve your stay!";
    }
}
