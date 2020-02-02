using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float startDelay;
	public float fadeSpeed;
	public UnityEvent eventsOnStart;
	public UnityEvent eventsOnFadeIn;
	public UnityEvent eventsOnFadeOut;
	public Image fadeImage;

	void Start()
	{
		StartCoroutine(FadeInC());
		eventsOnStart.Invoke();
	}

	IEnumerator FadeInC()
	{
		yield return new WaitForSeconds(startDelay);

		float t = 1;

		while(t > 0)
		{
			t -= Time.deltaTime * fadeSpeed;
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g,fadeImage.color.b,t);
			yield return new WaitForEndOfFrame();
		}

		eventsOnFadeIn.Invoke();

		yield return new WaitForEndOfFrame();
	}

	IEnumerator FadeOutC()
	{
		yield return new WaitForSeconds(startDelay);

		float t = 0;

		while(t < 1)
		{
			t += Time.deltaTime * fadeSpeed;
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g,fadeImage.color.b,t);
			yield return new WaitForEndOfFrame();
		}

		eventsOnFadeOut.Invoke();

		yield return new WaitForEndOfFrame();
	}
}
