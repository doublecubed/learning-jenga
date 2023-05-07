// ------------------------
// Onur Ereren - May 2023
// ------------------------

// CreditsPanel is a separate script because the credits panel
// operates independently from the rest of the application.

using System.Collections;
using UnityEngine;

namespace JengaGame.UI
{
	public class CreditsPanel : MonoBehaviour
	{
		#region REFERENCES

		public Transform PanelTransform;
		
		#endregion

		#region VARIABLES

		public float ScaleDuration;
		private bool _panelVisible;
		private bool _panelScaling;
		
		#endregion

		#region METHODS

		public void ToggleCreditsPanel()
		{
			if (_panelScaling) return;
			
			if (_panelVisible)
			{
				_panelVisible = false;
				StartCoroutine(ScalePanel(0f));
			}
			else
			{
				_panelVisible = true;
				StartCoroutine(ScalePanel(1f));
			}
		}

		#endregion
		
		#region COROUTINES
		
		private IEnumerator ScalePanel(float targetScale)
		{
			_panelScaling = true;
			
			Vector3 startScale = PanelTransform.localScale;
			Vector3 endScale = Vector3.one * targetScale;
			
			float timer = 0f;
			while (timer < ScaleDuration)
			{
				timer += Time.deltaTime;
				float lerp = Mathf.Clamp(timer / ScaleDuration, 0f, 1f);

				PanelTransform.localScale = Vector3.Lerp(startScale, endScale, lerp);
				
				yield return null;
			}

			_panelScaling = false;
		}

		#endregion

	}
}