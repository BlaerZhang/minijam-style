using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;



public class Elephant : MonoBehaviour, IItem
{
    private bool isWorking = false; // if the elephant was triggered or not
    public static bool deviceTriggered = false;
    public Animator NoseAnimator;
    public Animator ElephantAnimator;
    public GameObject Water;
    public SpriteRenderer Rainbow;

    private Animator WaterAnimator;

    public float Interval = 1.5f;
    public float ElephantDuration = 3f;

    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        if (!isWorking)
        {
            StartCoroutine(PlayElephantAnimation());
        }
    }

    private IEnumerator PlayElephantAnimation()
    {
        //nose-in -> nose-up -> water -> rainbow
        //water -> rainbow -> nose-down -> nose-out

        isWorking = true; // change state so not interactable

        ElephantAnimator.SetTrigger("elephantTrigger");
        yield return new WaitForSeconds(Interval);
        NoseAnimator.Play("nose-rise");

        yield return new WaitForSeconds(Interval);
        Debug.Log("came here");
        Water.SetActive(true);

        yield return new WaitForSeconds(Interval);
        Rainbow.gameObject.SetActive(true);
        Rainbow.DOFade(1, 2f);
        WaterAnimator = Water.GetComponent<Animator>();

        // static bool state changed here after animation finishes
        deviceTriggered = true;

        yield return new WaitForSeconds(ElephantDuration); //how long everything stays

        //water disappear
        WaterAnimator.Play("Water-Exit");
        yield return new WaitForSeconds(0.5f);
        Water.SetActive(false);

        //rainbow disappear 
        yield return new WaitForSeconds(0.5f);
        Rainbow.DOFade(0, 1f).OnComplete(() => Rainbow.gameObject.SetActive(false));
        

        NoseAnimator.Play("nose-down");
        yield return new WaitForSeconds(Interval);
        ElephantAnimator.SetTrigger("elephantTrigger");

        //change state back
        isWorking = false;
        deviceTriggered = false;

        //    yield return new WaitForSeconds(Interval);
        //    ElephantAnimator.SetTrigger("elephantTrigger");
        //}
    }



}
