// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
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

		#region MONOBEHAVIOUR

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				foreach (Block block in _glassBlocks)
				{
					block.gameObject.SetActive(false);
				}

				foreach (Block block in _allBlocks)
				{
					block.gameObject.GetComponent<Rigidbody>().isKinematic = false;
					block.gameObject.GetComponent<Rigidbody>().useGravity = true;
				}
			}
		}

		#endregion

		#region METHODS

		public void BuildTower(List<BlockData> blocks)
		{
			_blockPrefab = Coordinator.BlockPrefab;
			_blockInterval = Coordinator.BlockSpacing;
			_blockHeight = Coordinator.BlockHeight;

			_allBlocks = new List<Block>();
			_glassBlocks = new List<Block>();
			
			for (int i = 0; i < blocks.Count; i++)
			{
				GameObject block = Instantiate(_blockPrefab, transform);
				block.transform.position = transform.position + Vector3.up * _blockHeight * BlockLevel(i) +
				                           (Quaternion.Euler(0f, BlockOrientationAngle(i), 0f) * Vector3.right) * _blockInterval * (BlockLevelPosition(i) - 1);
				block.transform.rotation = Quaternion.Euler(0f, 90f - BlockOrientationAngle(i), 0f);

				Block blockScript = block.GetComponent<Block>();

				blockScript.SetData(blocks[i]);

				int mastery = blocks[i].Mastery;
				
				_allBlocks.Add(blockScript);
				
				if (mastery == 0) _glassBlocks.Add(blockScript);
				
				blockScript.SetMaterial(Coordinator.MasteryMaterials[mastery]);

				string labelContent = mastery == 0 ? " " : mastery == 1 ? "LEARNED" : "MASTERED";
				
				blockScript.DressLabels(labelContent);
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