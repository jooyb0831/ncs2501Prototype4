using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public delegate void MineHandler();
    public static event MineHandler OnMineReady;
    
    private int catchCnt = 0;
    private PlayerController player;

    private void Start()
    {
        //player = GameObject.Find("Player").GetComponent<PlayerController>();   
    }

    public void AddCatch()
    {
        catchCnt ++;
        if(catchCnt >= 3)
        {
            OnMineReady();
            //player.MineIsReady();
            Destroy(gameObject);
        }
    }
}
