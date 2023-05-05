// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using JengaGame.Data;
using UnityEngine;

namespace JengaGame.Gameplay
{

	public class TowerCoordinator : MonoBehaviour
	{
		#region REFERENCES

		public TowerBuilder SixthGradeTower;
		public TowerBuilder SeventhGradeTower;
		public TowerBuilder EighthGradeTower;

		[Space(10)] 
		
		public GameObject BlockPrefab;

		public Material[] MasteryMaterials;

		public float BlockSpacing;
		public float BlockHeight;

		#endregion

		#region VARIABLES

		#endregion

		#region MONOBEHAVIOUR

		private void Start()
		{
			SixthGradeTower.Coordinator = this;
			SeventhGradeTower.Coordinator = this;
			EighthGradeTower.Coordinator = this;
			
		}

		#endregion

		#region METHODS

		public void InitializeBuild(List<BlockData> sixth, List<BlockData> seventh, List<BlockData> eighth)
		{
			SixthGradeTower.BuildTower(sixth);
			SeventhGradeTower.BuildTower(seventh);
			EighthGradeTower.BuildTower(eighth);
		}
		
		#endregion

	}
}