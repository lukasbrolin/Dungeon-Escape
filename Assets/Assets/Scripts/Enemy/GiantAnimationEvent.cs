using UnityEngine;

public class GiantAnimationEvent : MonoBehaviour
{
    private Moss_Giant _giant;

    public void Awake()
    {
        _giant = GetComponentInParent<Moss_Giant>();
    }
    public void Fire()
    {
        AudioManager.instance.Play("Giant_Attack");
    }

}
