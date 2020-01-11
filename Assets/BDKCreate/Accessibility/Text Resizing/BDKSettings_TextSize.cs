/**
 * Copyright 2018 BDKCreate, LLC 
 * May be used in any way so long as credit is given where credit is due
 * 
 * Text Resizing object for Unity Text
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BDKCreate.Accessibility {

	[RequireComponent(typeof(Text))]
	public class BDKSettings_TextSize : MonoBehaviour {
		
		public Text text;

		public int currentSize = 50;

		public int minSize = 25;
		public int maxSize = 75;

		private bool firstTime = true;

		public BDKSettings_TextSize(int min, int max){
			minSize = min;
			maxSize = max;
		}
		// Use this for initialization
		void Start () {
			text = GetComponent<Text> ();

			// verify sizes in right order
			if (maxSize < minSize){
				int tempSize = maxSize;
				maxSize = minSize;
				minSize = tempSize;
			}

			//fix any text issues

			if (currentSize > maxSize) {
				currentSize = (maxSize);
			} else if (currentSize < minSize) {
				currentSize = minSize;
			}
			this.text.fontSize = currentSize;
			
		}

		//@param value is percent between max(100f) and min(0f)
		public void settingsSizeToPercent(float percent){
			if (this.text == null) { return; }
			// fix percent if outside 0...100
			if (percent < 0f) {
				percent = 0f;
			} else if (percent > 100f) {
				percent = 100f;
			}
				
			float range = maxSize - minSize;
			int newSize = (int) (minSize + (range * (percent / 100f)));

			text.fontSize = newSize;
			currentSize = newSize;

		}
	}
}
