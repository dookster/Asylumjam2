using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Candlelight.UI;
using System.Collections;
using UnityEngine.UI;
using System;

public class TwineThing : MonoBehaviour {

	public bool AutoStart;

	public CarStory carStory;
	public StoryHandler storyHandler;

	public InputField inputField;
	public Button inputDoneButton;

	/*
	 *  Multi-body twine passages. Some words have two different variations. They are ordered on pairs between two (( and )) signs divided by |
	 *  ex:
	 * 
	 *  This is normal text followed by either ((this sentence|that sentence))
	 * 
	 * 	The marked text is colored differently and is changeable while playing.
	 */ 
	
	private static TwineThing instance = null;
	public static TwineThing Instance
	{
		get
		{
			if(instance == null)
			{
				instance = UnityEngine.Object.FindObjectOfType<TwineThing>();
			}
			return instance;
		}
	}

	public TextAsset TweeFile;
	public HyperText MainHyperText;

	public bool blockInput = false;

	public AudioClip woosh;
	public AudioClip click;
	
	private TweePassage currentPassage;
	
	public bool gameStarted;
	
	string tweeText;

	public Dictionary<string, TweePassage> passages = new Dictionary<string, TweePassage>();

	private Dictionary<string, string> variables = new Dictionary<string, string>();
		
	[System.Serializable]
	public class TweePassage 
	{
		public string title;
		public string[] tags;
		private string body;

		public string Body { set { body = value; } get { return body; } }

		public void SetBodies(string rawText)
		{
			string textWithLinks = TwineToHyper(rawText);
			Body = textWithLinks;
		}

	}
	
	// Use this for initialization
	void Start () 
	{
		tweeText = TweeFile.text;
		Parse();

		if(AutoStart)
		{
			StartGame();
		}
	}

	void Update()
	{

	}

	public void StartGame()
	{
		TweePassage startPassage = passages["Start"];
		currentPassage = startPassage;

		StartCoroutine(SwitchToPassage(startPassage, 0.5f));

		gameStarted = true;
	}

	private void Parse () {
		TweePassage currentPassage = null;
		
		// Buffer to hold the content of the current passage while we build it
		StringBuilder buffer = new StringBuilder();
		
		// Array that will hold all of the individual lines in the twee source
		string[] lines; 
		
		// Utility array used in various instances where a string needs to be split up
		string[] chunks;

		// Split the twee source into lines so we can make sense of it while parsing
		lines = tweeText.Split(new string[] {"\n"}, System.StringSplitOptions.None);
		
		for (long i = 0; i < lines.LongLength; i++) {
			
			// If a line begins with "::" that means a new passage has started
			if (lines[i].StartsWith("::")) {
				
				// If we were already building a passage, that one is done. Add it and get ready for a new
				if (currentPassage != null) {
					//currentPassage.body = TwineToHyper(buffer.ToString());
					currentPassage.SetBodies(buffer.ToString());
					passages.Add(currentPassage.title, currentPassage);
					buffer = new StringBuilder();
				}
				
				/* A new passage in a twee starts with a line like this:
	             *
	             * :: The Passage Begins Here [someTag anotherTag heyThere]
	             *               
	             * What's happening here is when a new passage starts, we ignore the
	             * :: prefix, strip off the ] at the end of the tags, and split the
	             * line on [ into two strings, one of which will be the passage title
	             * while the other has all of the passage's tags, if any are found.
	             */
				chunks = lines[i].Substring(2).Replace ("]", "").Split ('[');
				
				// We should always have at least a passage title, so we can
				// start a new passage here with that title.
				currentPassage = new TweePassage();
				currentPassage.title = chunks[0].Trim();
				
				// If there was anything after the [, the passage has tags, so just
				// split them up and attach them to the passage.
				if (chunks.Length > 1) {
					currentPassage.tags = chunks[1].Trim().Split(' ');  
				}
				
			} else if (currentPassage != null) {
				
				// If we didn't start a new passage, we're still in the previous one,
				// so just append this line to the current passage's buffer.
				buffer.AppendLine(lines[i]);    
			}
		}

		// When we hit the end of the file, we should still have the last passage in
		// the file in the buffer. Wrap it up and end it as well.
		if (currentPassage != null) {
			//currentPassage.body = TwineToHyper(buffer.ToString());
			currentPassage.SetBodies(buffer.ToString());
			passages.Add(currentPassage.title, currentPassage);
		}
		
	}
	
	/**
	 * Convert all links in given passage to hyperlinks
	 */
	public static string TwineToHyper(string bodyText)
	{
		while(bodyText.Contains("[["))
		{
			int startB = bodyText.IndexOf("[[");
			int endB = bodyText.IndexOf("]]") + 2;
			string link = bodyText.Substring(startB, endB - startB);
			bodyText = bodyText.Replace(link, TwineLinkToHyper(link));
		}
		return bodyText;
	}

	/**
	 * Convert a given [[...|...]] link to a hyperlink
	 */
	private static string TwineLinkToHyper(string bodyText)
	{
		bodyText = bodyText.Replace("[[", "");
		bodyText = bodyText.Replace("]]", "");

		if(bodyText.Contains("|"))
		{
			string[] split = bodyText.Split('|');
			return "<a name=\"" + split[1] + "\">" + split[0] + "</a>";
		}
		else
		{
			return "<a name=\"" + bodyText + "\">" + bodyText + "</a>";
		}

	}
	
	public void OnLinkClick(HyperText hyperText, HyperText.LinkInfo linkInfo)
	{
		//hyperText.text = passages[linkInfo.Id].body;
		//SetUiText(passages[linkInfo.Id]);
		StartCoroutine(SwitchToPassage(passages[linkInfo.Id], 0.25f));
	}

	public void GoToPassage(string passageName)
	{
		if(!passages.ContainsKey(passageName))
		{
			Debug.LogError("Passage name not found! : " + passageName);
		}

		StartCoroutine(SwitchToPassage(passages[passageName], 0.25f));
	}

	/**
	 * Show the text from the given passage in the UI, alt-body set on HyperTextB
	 */
	void SetUiText(string passageText)
	{
		//Debug.Log ("SETTING TEXT");
		MainHyperText.text = passageText;	
	}

	IEnumerator SwitchToPassage(TweePassage passage, float time)
	{
		//Debug.Log ("SWITCH TO PASSAGE, " + passage.title);
		blockInput = true;
	
		if(click != null)
		{
			AudioSource.PlayClipAtPoint(click, transform.position);
		}

		MainHyperText.CrossFadeAlpha(0, time, false);

		yield return new WaitForSeconds(time);

		string passageText = passage.Body;

		if(passage.Body.Contains("<<"))
		{
			passageText = ParseTwineCode(passage);
		}

		SetUiText(passageText);

		MainHyperText.CrossFadeAlpha(1, time, false);

		currentPassage = passage;
		blockInput = false;
	}

	string ParseTwineCode(TweePassage passage)
	{
		// TESTING
		//variables["$searchString"] = "baumann";

		string bodyText = string.Copy(passage.Body);

		// Parse if,else (always expects if+else) (only handles 'or' structures for now)
		while(bodyText.Contains("<<if"))
		{
			int startBlockIndex = bodyText.IndexOf ("<<if");
			int endBlockIndex = bodyText.IndexOf ("<<endif>>");

			int elseIndex = bodyText.IndexOf("<<else>>");
			
			// figure out if condition resolves
			string ifCondition = bodyText.Substring(startBlockIndex+4, bodyText.IndexOf(">>",startBlockIndex) - startBlockIndex - 4);
			//Debug.Log ("IFCON: " + ifCondition);

			bool ifResolves = ResolveIfStatement(ifCondition);

			string ifBlock = bodyText.Substring(startBlockIndex, endBlockIndex - startBlockIndex + 9);

			string ifPassage;
			string elsePassage;

			ifPassage = bodyText.Substring(bodyText.IndexOf(">>", startBlockIndex) + 2, elseIndex - (bodyText.IndexOf(">>", startBlockIndex) + 2));
			elsePassage = bodyText.Substring(elseIndex + 9, endBlockIndex - (elseIndex + 9));

			//Debug.Log ("IFBLOCK: " + ifBlock);
			//Debug.Log("If pass: " + ifPassage);
			//Debug.Log("Else pass: " + elsePassage);

			// Replace code with relevant passage
			bodyText = bodyText.Replace(ifBlock, ifResolves ? ifPassage : elsePassage);
		}

		// Parse print
		while(bodyText.Contains("<<print"))
		{
			int startBlockIndex = bodyText.IndexOf("<<print");
			int endBlockIndex = bodyText.IndexOf (">>", startBlockIndex) + 2;

			string printBlock = bodyText.Substring(startBlockIndex, endBlockIndex - startBlockIndex);

			string printKey = printBlock.Replace("<<print", "").Replace(">>", "").Trim();

			if(variables.ContainsKey(printKey))
			{
				bodyText = bodyText.Replace(printBlock, variables[printKey]);
			}
			else
			{
				bodyText = bodyText.Replace(printBlock, "");
			}
		}

		// Handle textinput requests
		if(bodyText.Contains("<<textinput"))
		{
			ShowInputField("enter name", "$searchString");
		}

		// Handle special unity calls
		while(bodyText.Contains("<<unity"))
		{
			int startIndex = bodyText.IndexOf ("<<unity", System.StringComparison.OrdinalIgnoreCase);
			int endIndex = bodyText.IndexOf(">>", startIndex, System.StringComparison.OrdinalIgnoreCase) + 2;
			string unityCall = bodyText.Substring(startIndex, endIndex-startIndex);
			Debug.Log("Unity call: " + unityCall);
			
			string[] splitResult = unityCall.Split(' ');
			
			string target = splitResult[1].Replace("'", "");
			string method = splitResult[2];
			string param = splitResult[3].Replace(">>", "").Replace("\"", "");
			
			HandleTwineToUnityCall(target, method, param);
			
			// Remove unity call text
			bodyText = bodyText.Replace(unityCall, "");
		}

		return bodyText;
	}

	// resolves statements in the form:   $var is something or $var is somethingElse 
	bool ResolveIfStatement(string statement)
	{
		string[] ors = statement.Split(new string[] {"or"}, System.StringSplitOptions.None);

		foreach(string or in ors)
		{
			string[] words = or.Split(new string[]{"is"}, System.StringSplitOptions.None);

			Debug.Log ("word: " + words[0] + " , " + words[1]);

			if(variables.ContainsKey(words[0].Trim()) && variables[words[0].Trim()] == words[1].Trim(new char[]{' ', '\"'}))
			{
				return true;
			}
		}
		return false;
	}

	void ShowInputField(string passageOnDone, string variableToStore)
	{
		inputField.transform.parent.gameObject.SetActive(true);
		inputDoneButton.onClick.AddListener(() => OnInputDoneClick(passageOnDone, variableToStore));
	}

	void HideInputField()
	{
		inputField.transform.parent.gameObject.SetActive(false);
		inputField.text = "";
	}

	void OnInputDoneClick(string passageOnDone, string variableToStore)
	{
		if(!string.IsNullOrEmpty(inputField.text.Trim()))
		{
			variables[variableToStore] = inputField.text;
			Debug.Log (variableToStore + " is " + variables[variableToStore]);
			HideInputField();
			GoToPassage(passageOnDone);
		}
	}

	void HandleTwineToUnityCall(string target, string method, string param)
	{
		/*
		 */
		Debug.Log ("Target: " + target);
		Debug.Log ("Method: " + method);
		Debug.Log ("Param: " + param);

		if(target.Equals("CARSTORY", System.StringComparison.OrdinalIgnoreCase))
		{
			Type type = (typeof(CarStory));
			type.GetMethod(method).Invoke((object)carStory, String.IsNullOrEmpty(param) ? null : new string[]{param});
		}

		if(target.Equals("STORY", System.StringComparison.OrdinalIgnoreCase))
		{
			Type type = (typeof(StoryHandler));
			type.GetMethod(method).Invoke((object)storyHandler, String.IsNullOrEmpty(param) ? null : new string[]{param});
		}

	}


	static string ColorToHex(Color color)
	{
		string rgbString = string.Format("#{0:X2}{1:X2}{2:X2}", 
		                          (int)(color.r * 255), 
		                          (int)(color.g * 255), 
		                          (int)(color.b * 255));

		//Debug.Log("Color: " + rgbString);
		return rgbString;
	}

}
