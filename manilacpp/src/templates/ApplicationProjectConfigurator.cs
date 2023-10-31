
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Utils;
using Microsoft.ClearScript;

public class ApplicationProjectConfigurator : ProjectConfigurator {
	public ApplicationProjectConfigurator() { }

	public override void init() {
		_fileSets = new Dictionary<string, ManilaFile[]>();
	}

	internal Dictionary<string, ManilaFile[]> _fileSets;

	public ApplicationProjectConfigurator fileSets(ScriptObject obj) {
		foreach (var k in obj.PropertyNames) {
			_fileSets.Add(k, (ManilaFile[]) obj[k]);
		}
		return this;
	}

	public override Dictionary<string, dynamic> getProperties() {
		foreach (var e in _fileSets) {
			ManilaCPP.ManilaCPP.instance.info($"Name: '{e.Key}'");
			foreach (var f in e.Value) {
				ManilaCPP.ManilaCPP.instance.info($"\t{f}");
			}
		}

		var d = new Dictionary<string, dynamic> {
			{ "fileSets", _fileSets }
		};

		return d;
	}
}
