using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using UnityEngine;

public class VoiceManager : MonoBehaviour {

    KeywordRecognizer recognizer;
    Dictionary<string, System.Action> keywords;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
