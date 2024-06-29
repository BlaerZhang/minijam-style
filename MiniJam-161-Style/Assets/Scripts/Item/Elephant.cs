using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Elephant : MonoBehaviour, IItem
{
    private bool isWorking = false; // if the elephant was triggered or not
    public static bool deviceTriggered = false;
    public Animator NoseAnimator;
    public Animator ElephantAnimator;
    public GameObject Water;

    private Animator WaterAnimator;

    public float IntervalBeforeWater = 1.5f;
    public float ElephantLength = 0.2f;
    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        Debug.Log("Interacted with " + gameObject.name);
        if (!isWorking)
        {
            StartCoroutine(PlayElephantAnimation());
        }
    }

    private IEnumerator PlayElephantAnimation()
    {
        isWorking = true;
        ElephantAnimator.Play("elephant_park");
        yield return new WaitForSeconds(ElephantAnimator.GetCurrentAnimatorStateInfo(0).length / 3);
        ElephantAnimator.speed = 0;
        NoseAnimator.Play("nose-rise");
        yield return new WaitForSeconds(IntervalBeforeWater);
        Water.SetActive(true);
        WaterAnimator = Water.GetComponent<Animator>();
        deviceTriggered = true;
    }



}
