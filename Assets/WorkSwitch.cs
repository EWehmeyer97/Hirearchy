using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSwitch : MonoBehaviour
{
    public CandidateUpdate caller;

    public bool avail = false;
    private bool firstTime = true;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Available(bool x)
    {
        avail = x;
        anim.SetBool("avail", avail);
    }

    public void ChangeAvailablity()
    {
        if (TimeManager.Get().time < 32)
        {
            Available(!avail);
            if (firstTime)
            {
                firstTime = false;
                TimeManager.Get().StartTime();
            }

            if(avail && caller.candidate == null)
            {
                StartCoroutine(caller.UpdateCandidate());
            }
        }
    }
}
