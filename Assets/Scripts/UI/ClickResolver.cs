// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JengaGame.Gameplay;

public class ClickResolver : MonoBehaviour
{
	#region REFERENCES

	private Camera mainCam;

	#endregion

	#region VARIABLES

	#endregion

	#region MONOBEHAVIOUR

	private void Start()
	{
		mainCam = Camera.main;
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
		Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hitInfo))
		{
			if (hitInfo.collider.gameObject.GetComponent<Block>())
			{
				Debug.Log("it's a block");
			}
			else
			{
				Debug.Log("it's not a block");
			}
		}
	}
	
	#endregion

}
