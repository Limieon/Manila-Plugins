
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Scripting.Exceptions;
using Manila.Utils;
using Manila.Core;
using Microsoft.ClearScript;

namespace ManilaCPP.Configurators;

public class CPPProjectConfigurator : ProjectConfigurator {
	public CPPProjectConfigurator() { }

	public override void init() {
		_fileSets = new Dictionary<string, List<ManilaFile>>();
		_includeDirs = new List<ManilaDirectory>();
		_libDirs = new List<ManilaDirectory>();
		_defines = new List<string>();

		_binDir = null;
		_objDir = null;
	}

	internal Dictionary<string, List<ManilaFile>> _fileSets;
	internal List<ManilaDirectory> _includeDirs;
	internal List<ManilaDirectory> _libDirs;
	internal List<string> _defines;

	internal ManilaDirectory? _binDir;
	internal ManilaDirectory? _objDir;

	public void binDir(ManilaDirectory dir) {
		_binDir = dir;
	}
	public void objDir(ManilaDirectory dir) {
		_objDir = dir;
	}

	public void defines(ScriptObject obj) {
		defines(ScriptUtils.toArray<string>(obj));
	}
	public void defines(params string[] dirs) {
		_defines.AddRange(dirs);
	}

	public void includeDirs(ScriptObject obj) {
		includeDirs(ScriptUtils.toArray<ManilaDirectory>(obj));
	}
	public void includeDirs(params ManilaDirectory[] dirs) {
		_includeDirs.AddRange(dirs);
	}

	public void libDirs(ScriptObject obj) {
		libDirs(ScriptUtils.toArray<ManilaDirectory>(obj));
	}
	public void libDirs(params ManilaDirectory[] dirs) {
		_libDirs.AddRange(dirs);
	}

	public void fileSets(ScriptObject obj) {
		foreach (var k in obj.PropertyNames) {
			if (obj[k].GetType() != typeof(ManilaFile[])) throw new ScriptRuntimeException($"property in fileset '{k}' must be of type ManilaFile[]!");

			var files = new List<ManilaFile>((ManilaFile[]) obj[k]);

			if (_fileSets.ContainsKey(k)) {
				files.AddRange(_fileSets[k]);
			} else {
				_fileSets.Add(k, new List<ManilaFile>());
			}

			_fileSets[k] = files;
		}
	}

	public override Dictionary<string, dynamic> getProperties() {
		if (_binDir == null) throw new NullReferenceException("Property binDir cannot be null!");
		if (_objDir == null) throw new NullReferenceException("Property objDir cannot be null!");

		var d = new Dictionary<string, dynamic> {
			{ "fileSets", _fileSets },
			{ "includeDirs", _includeDirs },
			{ "libDirs", _libDirs },
			{ "binDir", _binDir },
			{ "objDir", _objDir },
			{ "defines", _defines }
		};

		return d;
	}
}
