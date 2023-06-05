using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationSystem : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClearAnimator()
    {
        animator.Rebind();
    }

    public void SetAnimatorSpeed(float speed)
    {
        animator.speed = speed;
    }

    public void PlayAnimationNormal(string animationName)
    {
        animator.Play(animationName);
    }

    public bool PlayAnimationAndWait(string animationName)
    {
        //Debug.Log("Started");
        //bool coroutineDone = false;
        //IEnumerator coroutine = PlayAnimationAndWaitCoroutine(animationName, () =>
        //{
        //    coroutineDone = true;
        //    Debug.Log("Coroutine is done, notified in Function1");
        //});
        //StartCoroutine(coroutine);
        //while (coroutine.MoveNext())
        //{
        //    if (coroutine.Current is bool result && result)
        //    {
        //        Debug.Log("Here");
        //        return true;
        //    }
        //}
        //while (!coroutineDone)
        //{
        //    // Do other work or wait
        //    Debug.Log("Waiting....");
        //}
        //Debug.Log("Function1 completed");
        return false;
    }

    public IEnumerator PlayAnimationAndWaitCoroutine(string animationName)
    {
        animator.Play(animationName);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f || animator.IsInTransition(0))
        {
            yield return null;
        }
      //  Debug.Log("Animation Done");
        yield return true;
    }
}
