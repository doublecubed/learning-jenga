// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JengaGame.Gameplay;
using JengaGame.Data;

namespace JengaGame.UI
{

	public class ClickResolver : MonoBehaviour
	{
		#region REFERENCES

		private Camera _mainCam;
		private UIController _uiController;
		
		#endregion

		#region VARIABLES

		#endregion

		#region MONOBEHAVIOUR

		private void Start()
		{
			_mainCam = Camera.main;
			_uiController = GetComponent<UIController>();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				ProcessRightClick();
			}
		}

		#endregion

		#region METHODS

		private void ProcessRightClick()
		{
			Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hitInfo))
			{
				if (hitInfo.collider.gameObject.TryGetComponent<Block>(out Block block))
				{
					BlockData data = block.Data;
					_uiController.DisplayBlockDataOnPanel(data);
					return;
				}
			}
			
			_uiController.HideInformationPanel();
		}

		#endregion

	}
}