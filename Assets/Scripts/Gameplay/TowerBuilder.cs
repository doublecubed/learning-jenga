// ------------------------
// Onur Ereren - May 2023
// ------------------------

// TowerBuilder builds and releases towers for each grade level.

using System.Collections.Generic;
using UnityEngine;
using JengaGame.Data;

namespace JengaGame.Gameplay
{
	
	public class TowerBuilder : MonoBehaviour
	{
		#region REFERENCES

		public TowerCoordinator Coordinator;
		
		#endregion

		#region VARIABLES

		private List<Block> _allBlocks;
		private List<Block> _glassBlocks;

		private GameObject _blockPrefab;
		private float _blockInterval;
		private float _blockHeight;
		
		#endregion

		#region METHODS

		public void ReleaseTower()
		{
			foreach (Block block in _glassBlocks)
			{
				block.gameObject.SetActive(false);
			}

			foreach (Block block in _allBlocks)
			{
				block.Rigidbody.isKinematic = false;
				block.Rigidbody.useGravity = true;
			}
		}
		
		public void BuildTower(List<BlockData> blocks)
		{
			_blockPrefab = Coordinator.BlockPrefab;
			_blockInterval = Coordinator.BlockSpacing;
			_blockHeight = Coordinator.BlockHeight;

			_allBlocks = new List<Block>();
			_glassBlocks = new List<Block>();
			
			for (int i = 0; i < blocks.Count; i++)
			{
				// Instantiate and position the block
				GameObject block = Instantiate(_blockPrefab, transform);
				block.transform.position = transform.position + Vector3.up * _blockHeight * BlockLevel(i) +
				                           (Quaternion.Euler(0f, BlockOrientationAngle(i), 0f) * Vector3.right) * _blockInterval * (BlockLevelPosition(i) - 1);
				block.transform.rotation = Quaternion.Euler(0f, 90f - BlockOrientationAngle(i), 0f);

				// Set the data of the block. Add the block to the block list.				
				Block blockScript = block.GetComponent<Block>();
				blockScript.SetData(blocks[i]);
				_allBlocks.Add(blockScript);
				
				// Get the mastery of the block. If it's 0, add it to glass block list.
				int mastery = blocks[i].Mastery;
				if (mastery == 0) _glassBlocks.Add(blockScript);

				// Set block material and text according to its mastery.
				blockScript.SetMaterial(Coordinator.MasteryMaterials[mastery]);
				string labelContent = mastery == 0 ? " " : mastery == 1 ? "LEARNED" : "MASTERED";
				blockScript.DressLabels(labelContent);
			}
		}

		public void DestroyTower()
		{
			Debug.Log(_allBlocks);
			
			if (_allBlocks == null || _allBlocks.Count == 0) return;
			
			foreach (Block block in _allBlocks)
			{
				Destroy(block.gameObject);
			}
		}
		
		private float BlockOrientationAngle(int index)
		{
			int orientationIndex = BlockLevel(index) % 2;
			return (float)orientationIndex * 90f;
		}
		
		private int BlockLevel(int index)
		{
			return index / 3;
		}

		private int BlockLevelPosition(int index)
		{
			return index % 3;
		}
		
		#endregion

	}
}