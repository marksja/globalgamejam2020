using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI text;

	public void SetText(string inText)
	{
		text.text = inText;
	}

	[ContextMenu("Test text")]
	public void TestText()
	{
		SetText("Hey this is some dialogue! Is this working? If it isn't, you wouldn't even see that question.");
	}
}
