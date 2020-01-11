/**
 * Copyright 2018 BDKCreate, LLC 
 * May be used in any way so long as credit is given where credit is due
 * 
 * Editor Window to manage text resizing and make a one click addign of text resizing system to Unity Text elements
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace BDKCreate.Accessibility {
	public class BDKTextResizing_EditorWindow : EditorWindow {

		// List of all text elements
		//public List<BDKSettings_TextSize> textList = new List<BDKSettings_TextSize> ();
		// the Game Object in the scene that manages the text sizing
		public BDKSettings_Manager textManager;
		private BDKSettings_TextSize textSize;

		// Modifies the current text size when adding to system
		public float maxPercent = 1.0f;
		public float minPercent = 0.5f;

		// Current text percent size for editing
		public float textSizeValue = 50f;

		// GUI Scroll bar varible
		private Vector2 scropPos = Vector2.zero;

		// Start Methods ------------------------------------------------
		[MenuItem ("BDKCreate/Accesability/Text Resizing")]
		public static void  ShowWindow () {
			BDKTextResizing_EditorWindow myEditor = GetWindow<BDKTextResizing_EditorWindow> ("BDK Text");
		}

		// Drawng Methods ------------------------------------------------

		void OnGUI(){
			// HEADER
			drawHeaderArea();

			// BODY
			scropPos = EditorGUILayout.BeginScrollView (scropPos);
			EditorGUILayout.BeginVertical ();


			for (int i = 0; i < textManager.textList.Count; i++) {
				if (!drawTextEditArea (i)) {
					indexAllText ();
					OnGUI ();
				}
			}

			// FOOTER
			drawFooter ();

			EditorGUILayout.EndVertical ();
			EditorGUILayout.EndScrollView ();

		}

		// draws header area TRUE if we have text
		private void drawHeaderArea(){
			EditorGUILayout.BeginVertical ();
			if (textManager == null) {
				EditorGUILayout.BeginHorizontal ();
				textManager = (BDKSettings_Manager) EditorGUILayout.ObjectField ("Settings Manager", textManager, typeof(BDKSettings_Manager));
				BDKSettings_Manager tempManager = FindObjectOfType<BDKSettings_Manager> ();
				if (tempManager == null) {
					if (GUILayout.Button ("Add Maanger")) {
						GameObject go = new GameObject ("BDKSettings_Maanger");
						go.AddComponent<BDKSettings_Manager>();
						textManager = go.GetComponent<BDKSettings_Manager>();
						//textManager.textList = this.textList;
					}
				} else {
					textManager = tempManager;
					//textManager.textList = this.textList;
				}

				EditorGUILayout.EndHorizontal ();
			} 

			if (textManager.textList.Count  < 1) {
				GUILayout.Label ("No Text Objects Indexed");
			
				// check for startup sequence and editor scipt changes
				addAllTextResizingObject ();
				alltextResizeGetTextObjects ();

			} else {
				GUILayout.Label ( textManager.textList.Count.ToString() + " Text to edit");
				EditorGUILayout.BeginHorizontal ();
				if (textManager != null) {
					textSizeValue = EditorGUILayout.Slider ("Text Size", textSizeValue, 0, 100);
					textManager.changeTextSize (textSizeValue);
				}
				EditorGUILayout.EndHorizontal ();
			}

			EditorGUILayout.EndVertical ();
		}

		// Draws each cell area for text views
		// return if we can keep going or if it needs to be cancled due to null errors
		private bool drawTextEditArea(int index){
			// prevent index errors
			if (index < 0 || index >= textManager.textList.Count) {
				Debug.LogWarning ("Index out of Bounds: BDKTextResizing_EditorWindow.drawTextEditArea.index for textList");
				return false;
			}

			// check for deletions
			if (textManager.textList[index] == null){
				return false;
			}

			EditorGUILayout.BeginHorizontal ();
			//GUILayout.TextField ("Object Name");
			textManager.textList [index].text.name = GUILayout.TextField (textManager.textList [index].text.name);
			//GUILayout.TextField (textManager.textList [index].text.text);
			textManager.textList [index].minSize = EditorGUILayout.IntField ("Min" , textManager.textList [index].minSize);
			textManager.textList [index].maxSize = EditorGUILayout.IntField ("Max" , textManager.textList [index].maxSize);
			textManager.textList [index].gameObject.SetActive (GUILayout.Toggle (textManager.textList [index].gameObject.activeSelf, "On"));
			if (GUILayout.Button("Remove")){
				removeTextResizing(index);
			}
			EditorGUILayout.EndHorizontal ();
			textManager.textList [index].text.text = GUILayout.TextField (textManager.textList [index].text.text);

			EditorGUILayout.Separator ();

			return true;
		}

		private void drawFooter(){
			// Add all
			EditorGUILayout.BeginHorizontal ();
			minPercent = EditorGUILayout.FloatField ("Min %", minPercent);
			maxPercent = EditorGUILayout.FloatField ("Max %", maxPercent);
			EditorGUILayout.EndHorizontal ();

			if( GUILayout.Button("Add All Text")){
				indexAllText ();
			}

			// Add One specific
			//BDKSettings_TextSize textSize;
			textSize = EditorGUILayout.ObjectField(textSize, typeof(BDKSettings_TextSize), true) as BDKSettings_TextSize;
			if (textSize != null) {
				addTextResizing (textSize);
			}

			// Wipe out issues
			GUILayout.Label ("Wipe Items added Clean");

			if( GUILayout.Button("Delete Added Text Size")){
				delteAllTextResizing ();
			}
		}

		// Funtional Get and find of all objects ------------------------------------------------

		private void indexAllText(){
			//?do i need to activate/deacivate GO
			//Text[] tempText = FindObjectsOfType<Text>();

			GameObject[] allGo = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
			//Debug.Log ("GOs : " + allGo.Length);
			int tempNum = 0;

			foreach (GameObject go in allGo) {
				Text[] tempText = go.GetComponentsInChildren<Text> (true);
				//tempNum += tempText.Length;
				foreach (Text text in tempText) {
					if (text.gameObject.GetComponents<BDKSettings_TextSize> ().Length < 1) {
						text.gameObject.AddComponent<BDKSettings_TextSize> ();
						text.gameObject.GetComponent<BDKSettings_TextSize> ().minSize = (int)(text.fontSize * minPercent);
						text.gameObject.GetComponent<BDKSettings_TextSize> ().maxSize = (int)(text.fontSize * maxPercent);
						tempNum++;
					}
				}
			}
			addAllTextResizingObject ();
			alltextResizeGetTextObjects ();
			//Debug.Log ("Text To Add: " + tempNum);
		}
			

		// Deletes all text resizing components
		private void delteAllTextResizing(){
			GameObject[] allGo = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
			//Debug.Log ("GOs : " + allGo.Length);
			int tempNum = 0;

			foreach (GameObject go in allGo) {
				BDKSettings_TextSize[] tempTextRS = go.GetComponentsInChildren<BDKSettings_TextSize> (true);
				foreach (BDKSettings_TextSize text in tempTextRS) {
					DestroyImmediate (text);
					tempNum++;
				}
			}
			textManager.textList = new List<BDKSettings_TextSize> ();
			//Debug.Log ("Text Ressizing Removed: " + tempNum);
		}

		// adds to the editing list
		private void addAllTextResizingObject(){
			textManager.textList = findAllTextResizingObjects ();
		}

		private List<BDKSettings_TextSize> findAllTextResizingObjects(){
			List<BDKSettings_TextSize> returnList = new List<BDKSettings_TextSize> ();

			// Get object
			GameObject[] allGo = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
			// aprse for correct ytpe
			foreach (GameObject go in allGo) {
				returnList.AddRange (go.GetComponentsInChildren<BDKSettings_TextSize> (true));
			}
			return returnList;
		}

		private void alltextResizeGetTextObjects(){
			foreach (BDKSettings_TextSize bdkText in textManager.textList) {
				if (bdkText.text == null) {
					bdkText.text = bdkText.gameObject.GetComponent<Text> ();
				}
			}
			if (textManager != null) {
				//textManager.textManager.textList = this.textList;
			}
		}


		// Removes specific 
		private void removeTextResizing(int index){
			textManager.textList.RemoveAt (index);
			//textList = textManager.textList;
			OnGUI ();
		}

		// add Specific 
		private void addTextResizing(BDKSettings_TextSize textSize){
			if (textSize == null) {
				return;
			}

			textManager.addText (textSize);
		}
	}
}