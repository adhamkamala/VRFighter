using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static System.TimeZoneInfo;

public class AnimationSystem : MonoBehaviour
{
    public AnimationClip[] animationsFromStillLeft;
    public AnimationClip[] animationsFromStillRight;
    public AnimationClip[] transitionAnimationsLeft;
    public AnimationClip[] transitionAnimationsRight;
    public AnimationClip[] transitionAnimationsReverseLeft;
    public AnimationClip[] transitionAnimationsReverseRight;
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


    // mode 2
    // change animation from --> to !! GetCurrent Location in the array..
    //if GetCurrentAnimationName == null --> from still

    public void PadsNetAnimate(string leftAnimationName, string rightAnimationName)
    {
        if (!string.IsNullOrEmpty(leftAnimationName)) {
            StartCoroutine(LeftPadNetAnimate(leftAnimationName));
        }
        if (!string.IsNullOrEmpty(rightAnimationName)) {
            StartCoroutine(RightPadNetAnimate(rightAnimationName));
        }
    }

    private IEnumerator LeftPadNetAnimate(string leftAnimationName)
    {
        bool firstSwitch;
        string currentAnimation = GetCurrentAnimationName(1);
        Debug.Log("EL Current: "+currentAnimation);
        if (currentAnimation == null) //from still
        {
            Debug.Log("EL Cadwdadaurrent: " + currentAnimation);
            int index = FindInArray(leftAnimationName, animationsFromStillLeft);
           if (index !=-1) {
                Debug.Log("EL Cadwdawdawdawdawdadawdaadaurrent: " + currentAnimation);
                yield return StartCoroutine(PlayAnimationAndWaitLayerCoroutine(animationsFromStillLeft[index].name, 1));
            }
        } else // from move
        {
            int indexOfCurrent = FindInArray(currentAnimation, animationsFromStillLeft);
            firstSwitch = true;
            if (indexOfCurrent ==-1)
            {
                indexOfCurrent=  FindInArray(currentAnimation, transitionAnimationsLeft);
                firstSwitch = false;
            }
            if (indexOfCurrent == -1)
            {
                indexOfCurrent = FindInArray(currentAnimation, transitionAnimationsReverseLeft);
                firstSwitch = false;
            }
            int indexOfRequested = FindInArray(leftAnimationName, animationsFromStillLeft);
            if (indexOfCurrent < indexOfRequested)
            {
            ///    SetAnimatorSpeed(0.3f);
                Debug.Log("inedx a " + indexOfCurrent + "inedx b " + indexOfRequested);
                int i;
                if (firstSwitch)
                {
                   i = indexOfCurrent - 1;
                } else
                {
                    i = indexOfCurrent;
                }
             
                for (; i < indexOfRequested; i++)
                {
                    Debug.Log("play i left" + i);
                    yield return StartCoroutine(PlayAnimationAndWaitLayerCoroutine(transitionAnimationsLeft[i].name, 1));
                }
            }
            else if (indexOfCurrent > indexOfRequested)
            {
                int i;
                if (firstSwitch)
                {
                    i = indexOfCurrent - 1;
                }
                else
                {
                    i = indexOfCurrent;
                }
             ///   SetAnimatorSpeed(0.3f);
                Debug.Log("inedx a " + indexOfCurrent+ "inedx b " + indexOfRequested);
                for (; i >= indexOfRequested; i--)
                {
                    yield return StartCoroutine(PlayAnimationAndWaitLayerCoroutine(transitionAnimationsReverseLeft[i].name, 1));

                }
            }
        }
    }
    private IEnumerator RightPadNetAnimate(string rightAnimationName)
    {
        bool firstSwitch;
        string currentAnimation = GetCurrentAnimationName(2);
        if (currentAnimation == null) //from still
        {
            int index = FindInArray(rightAnimationName, animationsFromStillRight);
           if (index != -1)
            {
                yield return StartCoroutine(PlayAnimationAndWaitLayerCoroutine(animationsFromStillRight[index].name, 2));
            }
        }
        else // from move
        {
            int indexOfCurrent = FindInArray(currentAnimation, animationsFromStillRight);
            firstSwitch = true;
            if (indexOfCurrent == -1)
            {
                indexOfCurrent = FindInArray(currentAnimation, transitionAnimationsRight);
                firstSwitch = false;
            }
            if (indexOfCurrent == -1)
            {
                indexOfCurrent = FindInArray(currentAnimation, transitionAnimationsReverseRight);
                firstSwitch = false;
            }
            int indexOfRequested = FindInArray(rightAnimationName, animationsFromStillRight);
            if (indexOfCurrent < indexOfRequested)
            {
               /// SetAnimatorSpeed(0.3f);
                int i;
                if (firstSwitch)
                {
                    i = indexOfCurrent - 1;
                }
                else
                {
                    i = indexOfCurrent;
                }
                for (; i < indexOfRequested; i++)
                {
                    yield return StartCoroutine(PlayAnimationAndWaitLayerCoroutine(transitionAnimationsRight[i].name, 2));
                }
            }
            else if (indexOfCurrent > indexOfRequested)
            {
             ///   SetAnimatorSpeed(0.3f);
                Debug.Log("inedx a " + indexOfCurrent + "inedx b " + indexOfRequested);
                int i;
                if (firstSwitch)
                {
                    i = indexOfCurrent - 1;
                }
                else
                {
                    i = indexOfCurrent;
                }
                for (; i >= indexOfRequested; i--)
                {
                    Debug.Log("play i right Order" + i + " "+ transitionAnimationsReverseRight[i].name);
                    yield return StartCoroutine(PlayAnimationAndWaitLayerCoroutine(transitionAnimationsReverseRight[i].name, 2));
                }
            }
        }
    }
    public void ChangeWeight()
    {
        animator.SetLayerWeight(1, 1f);
        SetAnimatorSpeed(1f);
       // PadsNetAnimate("TrainerHighLeft", "TrainerHighRight");
        // StartCoroutine(PlayTest());
    }

  

    public IEnumerator PlayAnimationAndWaitLayerCoroutine(string animationName, int layerIndex)
    {
       // animator.Play(animationName, layerIndex);
        animator.CrossFade(animationName, 0.3f, layerIndex);
        Debug.Log(animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime + " " + animator.IsInTransition(layerIndex));
        while (animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime < 1.0f || animator.IsInTransition(layerIndex))
        {
           // Debug.Log((animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime) + " " + animator.IsInTransition(layerIndex));
            yield return null;
        }
        Debug.Log("DONE0");
        yield return true;
    }
    private int FindInArray(string animationName, AnimationClip[] array )
    {
        int index = Array.FindIndex(array, anim => anim.name == animationName);
        if (index != -1)
        {
            return index;
        }
        else
        {
            return -1;
        }
    }
    private string GetCurrentAnimationName(int layerIndex)
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(layerIndex);

        if (clipInfo.Length > 0)
        {
            AnimationClip clip = clipInfo[0].clip;
            return clip.name;
        }

        return null; // No animation currently playing
    }
}
