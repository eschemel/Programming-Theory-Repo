using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : Enemy
{
    BoxCollider2D m_Collider;

    Vector2 origCollider;
    Vector2 flameCollider;

    float trapOffTime = 0.5f;
    float trapOnTime = 6f;
    bool flameOn = false;

    // Start is called before the first frame update
    void Start()
    {
        changeTime = trapOffTime;
        m_Collider = GetComponent<BoxCollider2D>();
        origCollider = new Vector2(m_Collider.size.x, m_Collider.size.y);
        flameCollider = new Vector2(origCollider.x, origCollider.y * 2f);
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        //timer countdown
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            //when timer reaches 0 then flame on... 
            StartCoroutine("TrapOn");
        }
    }

    IEnumerator TrapOn()
    {
        //animator trigger PLUS increase box collider
        animator.SetBool("flame", true);
        flameOn = true;
        m_Collider.size = flameCollider;
        
        //wait ... seconds then end animation, bool = false, and collider back to normal
        yield return new WaitForSeconds(trapOnTime);
        timer = changeTime;
        animator.SetBool("flame", false);
        flameOn = false;
        m_Collider.size = origCollider;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (flameOn != false) { 
        base.OnCollisionEnter2D(collision);
            //print("Child hit with collider");
        }
    }

    public override void EnemyMove()
    {
        rigidbody2d.MovePosition(rigidbody2d.position); // override to remain stationary
    }
}
