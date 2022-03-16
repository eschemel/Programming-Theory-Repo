using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShortPatroller : EnemyPatroller
{
    // Start is called before the first frame update
    void Start()
    {
        changeTime = 1.5f;
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
