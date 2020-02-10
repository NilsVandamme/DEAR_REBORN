/**
 * Copyright 2018 BDKCreate, LLC 
 * May be used in any way so long as credit is given where credit is due
 * 
 * A Quick script to report he Setting Maangers Text Perecent for UI Control purposes
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BDKCreate.Accessibility {
	[RequireComponent(typeof(UnityEngine.UI.Text))]
	public class BDKSettings_TextPercentValue : MonoBehaviour {

		private UnityEngine.UI.Text text;
		public BDKSettings_Manager myManager;

		// Use this for initialization
		void Start () {
            text = this.GetComponent<UnityEngine.UI.Text> ();
		}
		
		// Update is called once per frame
		void Update () {
			if (text != null && myManager != null) {
				text.text = myManager.textPercent.ToString("##0");
			}
		}
	}
}