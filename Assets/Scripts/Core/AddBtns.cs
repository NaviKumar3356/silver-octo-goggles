using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBtns : MonoBehaviour
{
    [SerializeField] private Transform puzeelFiled;
    [SerializeField] private GameObject btns;
    private void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject button = Instantiate(btns);
            button.name = "" + i;
            button.transform.SetParent(puzeelFiled, false);
        }
    }
}
