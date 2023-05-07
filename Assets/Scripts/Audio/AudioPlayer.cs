// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JengaGame.Audio
{
	public class AudioPlayer : MonoBehaviour
	{
		#region REFERENCES

		private AudioSource _player;

		public AudioClip ButtonClick;
		
		#endregion

		#region VARIABLES

		#endregion

		#region MONOBEHAVIOUR

		private void Awake()
		{
			_player = GetComponent<AudioSource>();
		}

		#endregion

		#region METHODS

		public void PlayButtonClick()
		{
			_player.PlayOneShot(ButtonClick);
		}
		
		#endregion

	}
}