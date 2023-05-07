// ------------------------
// Onur Ereren - May 2023
// ------------------------

// DataSorter pulls json data from the URL specified using Newtonsoft json package.
// It creates a JsonImporter instance and tasks it with importing json data.
// The data is then parsed into Lists for appropriate grade levels,
// and the go ahead is given to generate the towers by enabling the Generate button in the UI.

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using JengaGame.Gameplay;

namespace JengaGame.Data
{
	public class DataSorter : MonoBehaviour, IDataProvider
	{
		#region REFERENCES
		
		private string _url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
		
		private JsonImporter _importer;

		#endregion

		#region VARIABLES

		private List<BlockData> _sixthGradeData;
		private List<BlockData> _seventhGradeData;
		private List<BlockData> _eighthGradeData;

		private string _rawJsonData;
		private List<BlockData> _unsortedBlockData;
		
		#endregion

		#region METHODS

		public void RequestData(IDataReceiver receiver)
		{
			ImportData(receiver);
		}

		private async void ImportData(IDataReceiver receiver)
		{
			_importer = new JsonImporter();
			await StartImport();
			SortIntoBlockData();
			SortIntoGrades();

			List<BlockData>[] blockDataBundle = new List<BlockData>[3];
			blockDataBundle[0] = _sixthGradeData;
			blockDataBundle[1] = _seventhGradeData;
			blockDataBundle[2] = _eighthGradeData;
			
			receiver.DataReady(blockDataBundle);
		}
		
		
		private async Task StartImport()
		{
			_rawJsonData = await _importer.ImportJson(_url);
		}

		private void SortIntoBlockData()
		{
			_unsortedBlockData = ParseJsonToBlockDataList(_rawJsonData);
		}
		
		private void SortIntoGrades()
		{
			Debug.Log("sorting into grades");

			_sixthGradeData = new List<BlockData>();
			_seventhGradeData = new List<BlockData>();
			_eighthGradeData = new List<BlockData>();
			
			for (int i = 0; i < _unsortedBlockData.Count; i++)
			{
				switch (_unsortedBlockData[i].Grade)
				{
					case "6th Grade" :
						_sixthGradeData.Add(_unsortedBlockData[i]);
						break;
					case "7th Grade" :
						_seventhGradeData.Add(_unsortedBlockData[i]);
						break;
					case "8th Grade" :
						_eighthGradeData.Add(_unsortedBlockData[i]);
						break;
					default :
						break;
				}	
			}
			
			Debug.Log("sixth grade has " + _sixthGradeData.Count + " elements");
			Debug.Log("seventh grade has " + _seventhGradeData.Count + " elements");
			Debug.Log("eighth grade has " + _eighthGradeData.Count + " elements");
		}
		
		private List<BlockData> ParseJsonToBlockDataList(string json)
    {
	    List<BlockData> blockDataList = JsonConvert.DeserializeObject<List<BlockData>>(json);
	    return blockDataList;
    }

		
    #endregion

	}
}