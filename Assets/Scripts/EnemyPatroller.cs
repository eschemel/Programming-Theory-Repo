using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : Enemy
{
    
    // Start is called before the first frame update
    void Start()
    {
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }
}
