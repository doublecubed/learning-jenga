// ------------------------
// Onur Ereren - April 2023
// ------------------------

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

		[Space(5)] public Button TestStackButton;

		[Space(5)] public Button GenerateTowerButton;

		#endregion

		#region VARIABLES

		private Dictionary<string, ButtonComposite> _buttons;

		#endregion

		#region MONOBEHAVIOUR

		private void Awake()
		{
			_buttons = new Dictionary<string, ButtonComposite>();
			_buttons.Add("sixthGrade", GenerateButtonComposite(SixthGradeButton));
			_buttons.Add("seventhGrade", GenerateButtonComposite(SeventhGradeButton));
			_buttons.Add("eighthGrade", GenerateButtonComposite(EighthGradeButton));
			_buttons.Add("generateTower", GenerateButtonComposite(GenerateTowerButton));
			_buttons.Add("testStack", GenerateButtonComposite(TestStackButton));
		}

		#endregion

		#region METHODS

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

		#endregion

		public ButtonComposite GenerateButtonComposite(Button theButton)
		{
			ButtonComposite theComposite = new ButtonComposite();
			theComposite.ButtonScript = theButton;
			theComposite.ButtonTexts = theButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
			return theComposite;
		}
		


	}

	public struct ButtonComposite
	{
		public Button ButtonScript;
		public TextMeshProUGUI[] ButtonTexts;
	}
	
}