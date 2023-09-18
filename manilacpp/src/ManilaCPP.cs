
using Manila.Core;
using Manila.Plugin.API;

using ManilaCPP.Clang;

namespace ManilaCPP;

public class ManilaCPP : Plugin {
	public static ManilaCPP instance { get; private set; }

	public ManilaCPP() : base("manilacpp") {
		instance = this;
	}

	public readonly ClangStorage clangStorage = new ClangStorage();

	public override void init() {
		base.init();

		clangStorage.load(this);

		debug("Initializing...");
		debug("clangStorage.name:", clangStorage.data.name);

		addType(typeof(ManilaCPP));

		setBuildConfig(new ClangBuildConfig());
	}
	public override void shutdown() {
		base.shutdown();

		debug("Shutting down...");
	}

	public static Clang.API.Clang.Flags clangFlags() { return Clang.API.Clang.flags(); }
	public static Clang.API.Clang.Results clangCompile(Clang.API.Clang.Flags flags, Project project, Workspace workspace) { return Clang.API.Clang.compile(flags, project, workspace); }
}
