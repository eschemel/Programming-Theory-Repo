using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyGameOver : MonoBehaviour
{
    private static DontDestroyGameOver instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
