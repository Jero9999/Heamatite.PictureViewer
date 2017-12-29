using System.Collections.Generic;

namespace Heamatite.PictureViewer.app.Models
{
	public class ListModel
	{
		public Dictionary<string, string> Folders { get; internal set; }
		public IEnumerable<string> Images { get; internal set; }
	}
}
