using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globaldata : MonoBehaviour
{
        public static globaldata Instance;
        public int playerClass = 0;
        void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    public void setMelee()
    {
        playerClass = 1;
        Debug.Log("Melee Set");
    }

    public void setGunner()
    {
        playerClass = 2;
        Debug.Log("Gunner Set");
    }
}
