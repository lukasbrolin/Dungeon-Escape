using UnityEngine;

public class SkeletonAnimationEvent : MonoBehaviour
{
    private Skeleton _skeleton;

    public void Awake()
    {
        _skeleton = GetComponentInParent<Skeleton>();
    }
    public void Fire()
    {
        AudioManager.instance.Play("Skeleton_Attack");
    }

}
