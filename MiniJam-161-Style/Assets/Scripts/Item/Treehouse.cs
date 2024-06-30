using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treehouse : MonoBehaviour
{
    public GameObject TreehouseWithKids;

    public void GetKidsIn()
    {
        Debug.Log("Kids in.");
        TreehouseWithKids.SetActive(true);
    }

    public void GetKidsOut()
    {
        Debug.Log("Kids out.");
        TreehouseWithKids.SetActive(false);
    }
}
