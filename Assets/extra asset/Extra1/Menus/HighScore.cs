using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour 
{
	private TextMeshProUGUI highScoreText;
	void Start () {
        highScoreText=GetComponent<TextMeshProUGUI>();

		highScoreText.text="High Score : "+PlayerPrefs.GetInt("HighScore");		
	}
}
