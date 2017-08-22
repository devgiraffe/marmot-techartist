using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Marmot : MonoBehaviour {

    public Transform hammerPoint;
    public SkeletonAnimation animation;
    public ParticleSystem smokeParticle;

    private Collider2D collider2D;

	void Start ()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;

    }

    public void GoOut(float lifeTime)
    {
        animation.loop = false;
        animation.AnimationName = "Pop";

        Invoke("PlayIdle", 0.5f);

        Invoke("EndLife", lifeTime);
    }

    private void EndLife()
    {
        if (animation.AnimationName == "Idle")
        {
            animation.loop = false;
            animation.AnimationName = "Return";

            SendMessageUpwards("NextMarmotShuffle",1);

            Invoke("BackToDefautAnim",1f);
        }
    }

    private void BackToDefautAnim()
    {
        animation.AnimationName = "Default";
    }

    public void PlayIdle()
    {
        collider2D.enabled = true;
        animation.loop = true;
        animation.AnimationName = "Idle";
    }

    public void TakeHit()
    {
        if (animation.AnimationName != "Return")
        {
            SendMessageUpwards("CountMarmot");

            animation.loop = false;
            animation.AnimationName = "Hurt";

            collider2D.enabled = false;

            Invoke("WaitHitAnimation", 1f);
        }
    }

    public bool IsOut()
    {
        return animation.AnimationName == "Idle" || animation.AnimationName == "Return" || animation.AnimationName == "Pop";
    }

    public bool CanHit()
    {
        return animation.AnimationName == "Idle";
    }

    public bool IsHide()
    {
        return animation.AnimationName == "Default";
    }

    public void WaitHitAnimation()
    {
        smokeParticle.Play();
        SendMessageUpwards("NextMarmotShuffle",1);
        animation.AnimationName = "Default";
    }

}
