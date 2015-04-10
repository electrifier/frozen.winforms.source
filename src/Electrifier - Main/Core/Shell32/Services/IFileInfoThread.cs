using System;

namespace Electrifier.Core.Shell32.Services {
	/// <summary>
	/// Zusammenfassung f�r IFileInfoThread.
	/// </summary>
	public interface IFileInfoThread {
		void Prioritize(IShellObject sender);
		void Remove(IShellObject sender);
	}
}
