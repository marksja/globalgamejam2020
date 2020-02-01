using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DonAnimatorController : MonoBehaviour
{
    Animator anim;

	void Start()
	{
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
}
