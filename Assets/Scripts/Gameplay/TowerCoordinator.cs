// ------------------------
// Onur Ereren - May 2023
// ------------------------

// TowerCoordinator conveys the commands to build and release of towers
// to individual tower builders.

using System.Collections.Generic;
using JengaGame.Data;
using JengaGame.UI;
using UnityEngine;

namespace JengaGame.Gameplay
{

	public class TowerCoordinator : MonoBehaviour
	{
		#region REFERENCES

		private DataSorter _dataSorter;
		
		public TowerBuilder SixthGradeTower;
		public TowerBuilder SeventhGradeTower;
		public TowerBuilder EighthGradeTower;

		[Space(10)] 
		
		public GameObject BlockPrefab;

		// Materials array. Material indexes are for mastery levels of 0,1,and 2 respectively.
		public Material[] MasteryMaterials;

		public float BlockSpacing;
		public float BlockHeight;

		#endregion

		#region MONOBEHAVIOUR

		private void Awake()
		{
			_dataSorter = GetComponent<DataSorter>();
			
			SixthGradeTower.Coordinator = this;
			SeventhGradeTower.Coordinator = this;
			EighthGradeTower.Coordinator = this;
		}

		#endregion

		#region METHODS

		public void BuildTowers()
		{
			DestroyPreviousTowers();
			InitializeBuild(_dataSorter.SixthGradeData, _dataSorter.SeventhGradeData, _dataSorter.EighthGradeData);
		}

		public void ReleaseTowers()
		{
			SixthGradeTower.ReleaseTower();
			SeventhGradeTower.ReleaseTower();
			EighthGradeTower.ReleaseTower();
			
			UIController.Instance.DeactivateAllButtons();
			UIController.Instance.ActivateGenerateButton();
		}

		private void DestroyPreviousTowers()
		{
			SixthGradeTower.DestroyTower();
			SeventhGradeTower.DestroyTower();
			EighthGradeTower.DestroyTower();
		}
		
		private void InitializeBuild(List<BlockData> sixth, List<BlockData> seventh, List<BlockData> eighth)
		{
			SixthGradeTower.BuildTower(sixth);
			SeventhGradeTower.BuildTower(seventh);
			EighthGradeTower.BuildTower(eighth);
			
			UIController.Instance.ActivateOperationButtons();
		}
		
		#endregion

	}
}