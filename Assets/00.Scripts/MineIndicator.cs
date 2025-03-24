using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineIndicator : MonoBehaviour
{
    private void OnEnable()
    {   
        Mine.OnMineReady += ViewOff;
    }

    private void OnDisable()
    {
        Mine.OnMineReady -= ViewOff;
    }

    private void ViewOff()
    {
        gameObject.SetActive(false);
    }
}
