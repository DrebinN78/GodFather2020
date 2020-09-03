using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimCam : MonoBehaviour
{
    public PlayerController pc1;

    public Animator anim;

    public bool oneTime;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(oneTime == false){
        if(pc1.col.isTrigger == true){
            anim.SetTrigger("death");
            oneTime = true;
        }
        }
    }
}
