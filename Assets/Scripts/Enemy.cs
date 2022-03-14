using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        DealDamage();
    }
   
    public virtual void DealDamage()
    { // virtual keyword allows overriding

        //Player.Health -= 10;
    }
}
