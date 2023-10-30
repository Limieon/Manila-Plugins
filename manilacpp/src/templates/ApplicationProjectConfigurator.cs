
using Manila.Plugin.API;
using Manila.Scripting.API;
using Microsoft.ClearScript;

public class ApplicationProjectConfigurator : ProjectConfigurator {
	public ApplicationProjectConfigurator() { }

	public override void init() {
		_fileSets = new Dictionary<string, FileSet>();
	}

	internal Dictionary<string, FileSet> _fileSets;

	public ApplicationProjectConfigurator fileSets(ManilaDirectory root, ScriptObject obj) {
		foreach (var k in obj.PropertyNames) {
			var fs = new FileSet(root.getPath());
			((ScriptObject) obj[k]).InvokeAsFunction(fs);
			_fileSets.Add(k, fs);
		}
		return this;
	}

	public override Dictionary<string, dynamic> getProperties() {
		foreach (var e in _fileSets) {
			ManilaCPP.ManilaCPP.instance.info($"Name: '{e.Key}', Root: {e.Value.root}");
			foreach (var f in e.Value.files()) {
				ManilaCPP.ManilaCPP.instance.info($"\t{f}");
			}
		}

		var d = new Dictionary<string, dynamic> {
			{ "fileSets", _fileSets }
		};

		return d;
	}
}
