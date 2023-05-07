// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Block component used on block prefabs.

using UnityEngine;
using JengaGame.Data;
using TMPro;

namespace JengaGame.Gameplay
{

	public class Block : MonoBehaviour
	{
		#region REFERENCES

		private MeshRenderer _renderer;
		[SerializeField]
		private TextMeshPro[] _sideLabels;
		[SerializeField]
		private TextMeshPro[] _faceLabels;

		public Rigidbody Rigidbody { get; private set; }
		
		#endregion

		#region VARIABLES

		public BlockData Data { get; private set; }
		
		#endregion

		#region MONOBEHAVIOUR

		private void Awake()
		{
			_renderer = GetComponentInChildren<MeshRenderer>();
			Rigidbody = GetComponent<Rigidbody>();
		}
		

		#endregion

		#region METHODS
		
		public void SetMaterial(Material material)
		{
			_renderer.material = material;
		}

		public void SetData(BlockData data)
		{
			Data = data;
		}

		public void DressLabels(string label)
		{
			for (int i = 0; i < _faceLabels.Length; i++)
			{
				_faceLabels[i].text = label;
			}

			for (int i = 0; i < _sideLabels.Length; i++)
			{
				_sideLabels[i].text = label[0].ToString();
			}
		}

		#endregion

	}
}