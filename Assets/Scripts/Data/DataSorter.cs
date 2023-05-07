// ------------------------
// Onur Ereren - May 2023
// ------------------------

// DataSorter pulls json data from the URL specified using Newtonsoft json package.
// It creates a JsonImporter instance and tasks it with importing json data.
// The data is then parsed into Lists for appropriate grade levels,
// and the go ahead is given to generate the towers by enabling the Generate button in the UI.

using System.Collections.Generic;
using System.Threading.Tasks;
using JengaGame.UI;
using UnityEngine;
using Newtonsoft.Json;

namespace JengaGame.Data
{
	public class DataSorter : MonoBehaviour
	{
		#region REFERENCES
		
		private string _url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
		
		private JsonImporter _importer;

		#endregion

		#region VARIABLES

		public List<BlockData> SixthGradeData { get; private set; }
		public List<BlockData> SeventhGradeData { get; private set; }
		public List<BlockData> EighthGradeData { get; private set; }

		private string _rawJsonData;
		private List<BlockData> _unsortedBlockData;
		
		#endregion

		#region MONOBEHAVIOUR

		private async void Start()
		{
			_importer = new JsonImporter();
			await StartImport();
			SortIntoBlockData();
			SortIntoGrades();
			ActivateGenerateButton();
		}

	
		#endregion

		#region METHODS

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

			SixthGradeData = new List<BlockData>();
			SeventhGradeData = new List<BlockData>();
			EighthGradeData = new List<BlockData>();
			
			for (int i = 0; i < _unsortedBlockData.Count; i++)
			{
				switch (_unsortedBlockData[i].Grade)
				{
					case "6th Grade" :
						SixthGradeData.Add(_unsortedBlockData[i]);
						break;
					case "7th Grade" :
						SeventhGradeData.Add(_unsortedBlockData[i]);
						break;
					case "8th Grade" :
						EighthGradeData.Add(_unsortedBlockData[i]);
						break;
					default :
						break;
				}	
			}
			
			Debug.Log("sixth grade has " + SixthGradeData.Count + " elements");
			Debug.Log("seventh grade has " + SeventhGradeData.Count + " elements");
			Debug.Log("eighth grade has " + EighthGradeData.Count + " elements");
		}
		
		private List<BlockData> ParseJsonToBlockDataList(string json)
    {
	    List<BlockData> blockDataList = JsonConvert.DeserializeObject<List<BlockData>>(json);
	    return blockDataList;
    }

		private void ActivateGenerateButton()
		{
			UIController.Instance.ActivateGenerateButton();
		}
		
    #endregion

	}
}