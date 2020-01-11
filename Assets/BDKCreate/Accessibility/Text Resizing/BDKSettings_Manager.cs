/**
 * Copyright 2018 BDKCreate, LLC 
 * May be used in any way so long as credit is given where credit is due
 * 
 * Scene's mangaer object to be used as controls for text resizing and Volume (currenlty under dev)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BDKCreate.Accessibility {


	public class BDKSettings_Manager : MonoBehaviour {

		[Header("Text Size Settings")]
		// Text Size Managment Vars
		public float textPercent = 50f;
		public string textSizeKey = "textSize";


		public List<BDKSettings_TextSize> textList = new List<BDKSettings_TextSize> ();



		// Use this for initialization
		void Start () {
			cleanUpList ();

			// set saved player preferences here
			textPercent = PlayerPrefs.GetFloat(textSizeKey);

			// Text Size Start
			this.changeTextSize(textPercent);
		}

		// Text managment Methods

		public void changeTextSize(float value){
			if (value < 0) {
				value = 0;
			} else if (value > 100) {
				value = 100;
			}
			textPercent = value;
			setTextSizes (value);
		}

		protected virtual void setTextSizes(float value){
			foreach(BDKSettings_TextSize text in textList){
				if (text != null) {
					text.settingsSizeToPercent (value);
				} else {
					cleanUpList ();
					setTextSizes (value);
				}
			}
			//Update Player Prefs Here 
			PlayerPrefs.SetFloat (textSizeKey, value);
		}

		public void addText(BDKSettings_TextSize textSize){
			if (textSize == null) {
				return;
			}

			if (!textList.Contains (textSize)) {
				textList.Add (textSize);
			}
		}

		public void cleanUpList(){
			for (int i = 0; i < textList.Count; i++) {
				if (textList [i] == null) {
					textList.RemoveAt (i);
					cleanUpList ();
					break;
				}
			}
		}
	}

}

/**
 * Mod out player prefs at Lines: 22,34,62
 */