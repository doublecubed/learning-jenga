// ------------------------
// Onur Ereren - May 2023
// ------------------------

// ButtonController controls the activation & deactivation of UI buttons.
// Specifically, it makse sure that button text also fades out when
// buttons are made inactive.
// It uses a Discitonary to match string keys to ButtonComposite struct,
// which holds the Button component and TextMeshPro components of the button.


using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JengaGame.UI
{
	
	public class ButtonController : MonoBehaviour
	{
		#region REFERENCES
		
		public Button SixthGradeButton;
		public Button SeventhGradeButton;
		public Button EighthGradeButton;

		[Space(5)] 

		public Button TestStackButton;

		[Space(5)] 
		
		public Button GenerateTowerButton;

		#endregion

		#region VARIABLES

		private Dictionary<string, ButtonComposite> _buttons;

		#endregion

		#region MONOBEHAVIOUR

		private void Awake()
		{
			GenerateButtonDictionary();
		}

		#endregion

		#region METHODS

		private void GenerateButtonDictionary()
		{
			_buttons = new Dictionary<string, ButtonComposite>();
			_buttons.Add("sixthGrade", GenerateButtonComposite(SixthGradeButton));
			_buttons.Add("seventhGrade", GenerateButtonComposite(SeventhGradeButton));
			_buttons.Add("eighthGrade", GenerateButtonComposite(EighthGradeButton));
			_buttons.Add("generateTower", GenerateButtonComposite(GenerateTowerButton));
			_buttons.Add("testStack", GenerateButtonComposite(TestStackButton));
		}
		
		public void ActivateButton(string buttonName)
		{
			if (_buttons.ContainsKey(buttonName))
			{
				_buttons[buttonName].ButtonScript.interactable = true;
				foreach (TextMeshProUGUI text in _buttons[buttonName].ButtonTexts)
				{
					text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
				}
			}
		}

		public void DeactivateButton(string buttonName)
		{
			if (_buttons.ContainsKey(buttonName))
			{
				_buttons[buttonName].ButtonScript.interactable = false;
				foreach (TextMeshProUGUI text in _buttons[buttonName].ButtonTexts)
				{
					text.color = new Color(text.color.r, text.color.g, text.color.b, 0.5f);
				}
			}
		}



		// Generates a ButtonComposite for a given button by finding its child TextMeshPro components.
		public ButtonComposite GenerateButtonComposite(Button theButton)
		{
			ButtonComposite theComposite = new ButtonComposite();
			theComposite.ButtonScript = theButton;
			theComposite.ButtonTexts = theButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
			return theComposite;
		}
		
		#endregion
	}

	public struct ButtonComposite
	{
		public Button ButtonScript;
		public TextMeshProUGUI[] ButtonTexts;
	}
	
}