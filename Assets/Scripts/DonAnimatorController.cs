using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DonAnimatorController : MonoBehaviour
{
    Animator anim;
	public static DonAnimatorController Instance;

	void Awake()
	{
		Instance = this;		
		anim = GetComponent<Animator>();
	}

	public void PlayAnimation(string animationToPlay)
	{
		anim.Play(animationToPlay);
	}

	[ContextMenu("Play Idle")]
	void PlayITest()
	{
		anim.Play("Idle");
	}
	[ContextMenu("Play Greeting")]
	void PlayGTest()
	{
		anim.Play("Greeting");
	}
	[ContextMenu("Play Negative")]
	void PlayNTest()
	{
		anim.Play("Negative");
	}
	[ContextMenu("Play Sheepish")]
	void PlaySTest()
	{
		anim.Play("Sheepish");
	}
	[ContextMenu("Play Thinking")]
	void PlayTTest()
	{
		anim.Play("Thinking");
	}
}
