using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationManager : MonoBehaviour
{
	ConversationData storedData;

	public List<SpeakerLocator> speakerLocations;
	public Dictionary<string, SpeakerLocator> speakerLocationDictionary;

	public int currentConversationLine = 0;
	public float countdownToNextLine;

	public string conversationToLoadOnStart;

	GameObject currentLine;

	void Start()
	{
		speakerLocationDictionary = new Dictionary<string, SpeakerLocator>();
		foreach (SpeakerLocator locator in speakerLocations)
		{
			speakerLocationDictionary.Add(locator.speakerID, locator);
		}

		LoadConversation(conversationToLoadOnStart);
	}

	//Update is called Don, but only when it's wearing a mask
	void Update()
	{
		if (countdownToNextLine > 0)
		{
			countdownToNextLine -= Time.deltaTime;
			if (countdownToNextLine < 0)
			{
				countdownToNextLine = 0;
			}
		}
		if (countdownToNextLine == 0 || Input.GetMouseButtonDown(0))
		{
			PlayNextLine();
		}
	}

	public void LoadConversation(string filePath)
	{
		ConversationData data = SaveAndLoadXML.LoadFromXML<ConversationData>(filePath);
		LoadConversation(data);
	}

	public void LoadConversation(ConversationData data)
	{
		storedData = data;
		currentConversationLine = 0;
		PlayLine(0);
	}

	public void PlayNextLine()
	{
		PlayLine(++currentConversationLine);
	}

	public void PlayLine(int lineNumber)
	{
		if (lineNumber >= storedData.conversationLines.Count)
		{
			return;
		}

		ConversationLine lineToShow = storedData.conversationLines[lineNumber];

		//Destroy the old line
		if (currentLine != null)
		{
			currentLine.SetActive(false);
		}
		//Spawn a new line
		// GameObject obj = Resources.Load<GameObject>("DialogueBox");
		// if(obj != null)
		// {
		//     obj = Instantiate(obj, Vector3.zero, Quaternion.identity);

		//Find the speaker locator
		if (speakerLocationDictionary.ContainsKey(lineToShow.speakerID))
		{
			//Place it at the speaker location
			SpeakerLocator speakerLocator = speakerLocationDictionary[lineToShow.speakerID];
			// obj.transform.parent = speakerLocator.sceneLocator.transform;
			// obj.transform.localPosition = Vector3.zero;
			speakerLocator.sceneLocator.SetActive(true);
			speakerLocator.sceneLocator.GetComponent<DialogueController>().SetText(lineToShow.dialogue);
			if(DonAnimatorController.Instance != null)
			{
				DonAnimatorController.Instance.PlayAnimation(lineToShow.speakerAnimation);
			}
			//text.text = lineToShow.dialogue;

			//Store the reference for later
			currentLine = speakerLocator.sceneLocator;
		}

		//Fill in the text

		//}

		//Play audio
		//Run animation       

		countdownToNextLine = lineToShow.timeToDisplay;
	}
}

[System.Serializable]
public class SpeakerLocator
{
	public string speakerID;
	public GameObject sceneLocator;
}
