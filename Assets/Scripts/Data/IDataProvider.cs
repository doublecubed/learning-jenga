// ------------------------
// Onur Ereren - May 2023
// ------------------------

using JengaGame.Gameplay;

namespace JengaGame.Data
{
	public interface IDataProvider
	{
		public void RequestData(IDataReceiver receiver);

	}
}