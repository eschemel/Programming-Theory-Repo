using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : Enemy
{
    BoxCollider2D m_Collider;

    Vector2 origCollider;
    Vector2 flameCollider;

    bool flameOn;

    float activeTime;
    float activeWait;

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<BoxCollider2D>();
        origCollider = new Vector2(m_Collider.size.x, m_Collider.size.y);
        flameCollider = new Vector2(origCollider.x, origCollider.y * 2f);

        flameOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flameOn != true)
        {
            StartCoroutine("TrapOn");
        }
    }

    IEnumerator TrapOn()
    {
        activeTime = Random.Range(2, 6);
        //animator trigger PLUS increase box collider
        flameOn = true;
        animator.SetBool("flame", true);
        m_Collider.size = flameCollider;
        //Debug.Log("Flame On!");

        //wait ... seconds then end animation, bool = false, and collider back to normal
        yield return new WaitForSeconds(activeTime);

        activeWait = Random.Range(5, 11);
        animator.SetBool("flame", false);
        m_Collider.size = origCollider;
        //Debug.Log("Flame Off!");
        yield return new WaitForSeconds(activeWait);
        flameOn = false;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (flameOn != false)
        {
            base.OnCollisionEnter2D(collision);
            //print("Child hit with collider");
        }
    }

    public override void EnemyMove()
    {
        rigidbody2d.MovePosition(rigidbody2d.position); // override to remain stationary
    }
}
