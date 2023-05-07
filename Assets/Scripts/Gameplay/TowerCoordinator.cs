// ------------------------
// Onur Ereren - May 2023
// ------------------------

// TowerCoordinator conveys the commands to build and release of towers
// to individual tower builders.

using System.Collections.Generic;
using JengaGame.Data;
using JengaGame.UI;
using JengaGame.DI;
using UnityEngine;

namespace JengaGame.Gameplay
{

	public class TowerCoordinator : MonoBehaviour, IDataReceiver
	{
		#region REFERENCES

		private IDataProvider _dataSorter;
		
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

		#region VARIABLES

		private List<BlockData> _sixthGradeBlockData;
		private List<BlockData> _seventhGradeBlockData;
		private List<BlockData> _eighthGradeBlockData;
		
		
		#endregion
		
		#region MONOBEHAVIOUR

		private void Awake()
		{
			var injectionContainer = FindObjectOfType<DIContainer>();
			_dataSorter = injectionContainer.Resolve<IDataProvider>();
			
			SixthGradeTower.Coordinator = this;
			SeventhGradeTower.Coordinator = this;
			EighthGradeTower.Coordinator = this;
		}

		private void Start()
		{
			_dataSorter.RequestData(this);
		}
		
		#endregion

		#region METHODS

		public void DataReady(List<BlockData>[] towerBlockData)
		{
			_sixthGradeBlockData = towerBlockData[0];
			_seventhGradeBlockData = towerBlockData[1];
			_eighthGradeBlockData = towerBlockData[2];
			
			UIController.Instance.ActivateGenerateButton();
			
		}
		
		public void BuildTowers()
		{
			DestroyPreviousTowers();
			InitializeBuild(_sixthGradeBlockData, _seventhGradeBlockData, _eighthGradeBlockData);
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