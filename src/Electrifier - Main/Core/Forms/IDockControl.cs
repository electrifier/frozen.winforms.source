using System;

namespace Electrifier.Core.Forms.DockControls {
	/// <summary>
	/// Zusammenfassung f�r IDockControl.
	/// </summary>
	public interface IDockControl : IPersistent {
		void AttachToDockControlContainer(IDockControlContainer dockControlContainer);
	}
}
