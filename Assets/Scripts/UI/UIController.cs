// ------------------------
// Onur Ereren - May 2023
// ------------------------

// UIController coordinates the UI actions.
// Its public methods activate or deactivate buttons
// together based on the action the application needs.
// It utilizes the Singleton pattern for access from
// anywhere within the application.

using System.Collections;
using UnityEngine;
using JengaGame.Data;
using TMPro;

namespace JengaGame.UI
{
	public class UIController : MonoBehaviour
	{
		#region REFERENCES

		// Singleton Instance
		public static UIController Instance;
		
		private ButtonController _buttonController;
		
		public Transform InformationPanel;

		[Space(10)] 
		public TextMeshProUGUI gradeText;
		public TextMeshProUGUI clusterText;
		public TextMeshProUGUI standardIdText;

		[Space(10)] 
		public Color NotLearnedColor;
		public Color LearnedColor;
		public Color MasteredColor;
		
		#endregion

		#region VARIABLES

		public float InformationPanelScaleDuration;

		private bool _informationPanelScaling;

		#endregion

		#region MONOBEHAVIOUR

		private void Awake()
		{
			#region Singleton Logic
			if (Instance != null && Instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				Instance = this;
			}
			#endregion
			
			_buttonController = GetComponent<ButtonController>();
		}

		private void Start()
		{
			HideInformationPanel();
			DeactivateAllButtons();
		}

		#endregion

		#region METHODS

		#region Panel Operation
		
		public void DisplayBlockDataOnPanel(BlockData data)
		{
			gradeText.text = data.Grade + ": " + data.Domain;
			clusterText.text = data.Cluster;
			clusterText.color = ClusterTextColor(data);
			standardIdText.text = data.StandardId + ": " + data.StandardDescription;
			
			ShowInformationPanel();
		}

		private Color ClusterTextColor(BlockData data)
		{
			switch (data.Mastery)
			{
				case 0:
					return NotLearnedColor;
				case 1:
					return LearnedColor;
				case 2:
					return MasteredColor;
				default:
					return LearnedColor;
			}
		}
		
		private void ShowInformationPanel()
		{
			if (_informationPanelScaling) return;
			
			StartCoroutine(ScaleInformationPanel(1f));
		}

		public void HideInformationPanel()
		{
			if (_informationPanelScaling) return;
			
			StartCoroutine(ScaleInformationPanel(0f));
		}

		private IEnumerator ScaleInformationPanel(float targetScale)
		{
			_informationPanelScaling = true;

			Vector3 startingScale = InformationPanel.localScale;
			Vector3 endScale = Vector3.one * targetScale;

			float timer = 0f;
			while (timer < InformationPanelScaleDuration)
			{
				timer += Time.deltaTime;
				float lerp = Mathf.Clamp(timer / InformationPanelScaleDuration, 0f, 1f);

				InformationPanel.localScale = Vector3.Lerp(startingScale, endScale, lerp);
				yield return null;
			}

			_informationPanelScaling = false;
		}

		#endregion
		
		#region Button Operation

		public void DeactivateAllButtons()
		{
			_buttonController.DeactivateButton("sixthGrade");
			_buttonController.DeactivateButton("seventhGrade");
			_buttonController.DeactivateButton("eighthGrade");
			_buttonController.DeactivateButton("testStack");
			_buttonController.DeactivateButton("generateTower");
		}

		public void ActivateGenerateButton()
		{
			_buttonController.ActivateButton("generateTower");
		}

		public void ActivateOperationButtons()
		{
			_buttonController.ActivateButton("sixthGrade");
			_buttonController.ActivateButton("seventhGrade");
			_buttonController.ActivateButton("eighthGrade");
			_buttonController.ActivateButton("testStack");
		}
		
		
		#endregion
		
		#endregion

	}
}