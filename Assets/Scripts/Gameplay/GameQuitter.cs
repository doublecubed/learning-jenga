// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Quits the game

using UnityEngine;

public class GameQuitter : MonoBehaviour
{
	#region MONOBEHAVIOUR

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			QuitGame();
		}
	}

	#endregion
	
	#region METHODS

	public void QuitGame()
	{
		Application.Quit();
	}
	
	#endregion

}
