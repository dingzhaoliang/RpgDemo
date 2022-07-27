using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleFloat : MonoBehaviour
{
    public Animator anim;
    public GameObject root;
    public Text text;

    public void OnActive()
    {
        anim.Play("Anim",0,0);
    }

    public void OnUnActive()
    {
        
    }
}
