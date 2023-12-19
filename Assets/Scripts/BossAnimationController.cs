using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    public void EnableAnimator()
    {
        if (animator != null)
        {
            animator.enabled = true;
        }
    }
}