using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        IEnumerator coroutine = PlayAnimationAndWaitCoroutine(animationName);
        StartCoroutine(coroutine);
        while (coroutine.MoveNext())
        {
            if (coroutine.Current is bool result && result)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator PlayAnimationAndWaitCoroutine(string animationName)
    {
        animator.Play(animationName);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f || animator.IsInTransition(0))
        {
            yield return null;
        }
        yield return true;
    }
}
