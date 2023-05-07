// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Basic AudioSource component controller. As the project has only one sound effect,
// there is no need for complex sfx selection structure.
// The play method is called directly by the buttons.

using UnityEngine;

namespace JengaGame.Audio
{
	public class AudioPlayer : MonoBehaviour
	{
		#region REFERENCES

		private AudioSource _player;

		public AudioClip ButtonClick;
		
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