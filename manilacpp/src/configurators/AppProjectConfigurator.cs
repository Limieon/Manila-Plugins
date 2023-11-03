
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

	public ManilaDirectory? _workingDir;

	public void workingDir(ManilaDirectory dir) {
		_workingDir = dir;
	}

	public override void check() {
		base.check();

		if (_workingDir == null) throw new NullReferenceException("Property workingDir cannot be null!");
	}

	public override Dictionary<string, dynamic> getProperties() {
		return DictUtils.fromFields(this);
	}
}
