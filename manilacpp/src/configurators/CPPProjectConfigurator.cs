
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Scripting.Exceptions;
using Manila.Utils;
using Manila.Core;
using Microsoft.ClearScript;

namespace ManilaCPP.Configurators;

public abstract class CPPProjectConfigurator : ProjectConfigurator {
	private ManilaCPP plugin = ManilaCPP.instance;

	public CPPProjectConfigurator() { }

	public override void init() {
		_fileSets = new Dictionary<string, List<ManilaFile>>();
		_includeDirs = new List<ManilaDirectory>();
		_libDirs = new List<ManilaDirectory>();
		_defines = new List<string>();

		_binDir = null;
		_objDir = null;

		_arch = null;

		_systemversion = "latest";
		_cppdialect = "C++17";
		_cdialect = "C99";
	}

	public Dictionary<string, List<ManilaFile>> _fileSets;
	public List<ManilaDirectory> _includeDirs;
	public List<ManilaDirectory> _libDirs;
	public List<string> _defines;

	public ManilaDirectory? _binDir;
	public ManilaDirectory? _objDir;

	public string? _arch;

	public string _cppdialect = "C++17";
	public string _cdialect = "C99";

	public string _systemversion = "latest";

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

	public void arch(string v) {
		_arch = v;
	}

	public void systemversion(string v) {
		_systemversion = v;
	}

	public void cppdialect(string v) {
		_cppdialect = v;
	}
	public void cdialect(string v) {
		_cdialect = v;
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

	public override void check() {
		if (_binDir == null) throw new NullReferenceException("Property binDir cannot be null!");
		if (_objDir == null) throw new NullReferenceException("Property objDir cannot be null!");
		if (_arch == null) throw new NullReferenceException("Property arch cannot be null!");
	}

	public override Dictionary<string, dynamic> getProperties() {
		return DictUtils.fromFields(this);
	}

	// Functions
	public override void generate(Workspace ws, string toolset) {
		plugin.debug($"Generating build files using '{toolset}'...");
		foreach (var p in ws.projects) {
			if (p.configurator.GetType() == typeof(AppProjectConfigurator)) {
				plugin.debug($"Generating {p.name}...");
			}
		}
	}
	public override void build(Workspace ws, string toolset) {
		plugin.debug($"Building projects using '{toolset}'...");
		foreach (var p in ws.projects) {
			if (p.configurator.GetType() == typeof(AppProjectConfigurator)) {
				plugin.debug($"Building {p.name}...");
			}
		}
	}
}
