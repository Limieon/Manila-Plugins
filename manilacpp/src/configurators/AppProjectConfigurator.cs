
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Scripting.Exceptions;
using Manila.Utils;
using Microsoft.ClearScript;

namespace ManilaCPP.Configurators;

public class AppProjectConfigurator : CPPProjectConfigurator {
	public override void init() {
		base.init();

		_workingDir = null;
	}

	internal ManilaDirectory? _workingDir;

	public void workingDir(ManilaDirectory dir) {
		_workingDir = dir;
	}

	public override Dictionary<string, dynamic> getProperties() {
		if (_workingDir == null) throw new NullReferenceException("Property workingDir cannot be null!");
		System.Console.WriteLine("Working Dir: " + _workingDir.getPath());

		var d = new Dictionary<string, dynamic> {
			{"workingDir", _workingDir}
		};

		return DictUtils.merge<string, dynamic>(d, base.getProperties());
	}
}
