// ------------------------
// Onur Ereren - May 2023
// ------------------------

using System.Collections.Generic;
using JengaGame.Data;


namespace JengaGame.Gameplay
{
	public interface IDataReceiver
	{
		public void DataReady(List<BlockData>[] dataListBundle);

	}
}