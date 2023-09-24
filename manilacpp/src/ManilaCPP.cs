
using Manila.Core;
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Utils;

using ManilaCPP.Clang;

using Microsoft.ClearScript;

namespace ManilaCPP;

public class ManilaCPP : Plugin {
	public static ManilaCPP instance { get; private set; }

	public ManilaCPP() : base("manilacpp") {
		instance = this;
	}

	public readonly ClangCompilerStorage clangCompilerStorage = new ClangCompilerStorage();

	public override void init() {
		base.init();

		clangCompilerStorage.load(this);

		debug("Initializing...");

		addType(typeof(ManilaCPP));

		setBuildConfig(new ClangBuildConfig());
	}
	public override void shutdown() {
		clangCompilerStorage.save(this);
		base.shutdown();

		debug("Shutting down...");
	}

	public static Clang.API.Clang.Flags clangFlags() { return Clang.API.Clang.flags(); }
	public static ClangCompiler.CompilerResults clangCompile(Clang.API.Clang.Flags flags, Project project, Workspace workspace, ManilaFile src) {
		return Clang.API.Clang.compile(flags, project, workspace, src);
	}
	public static ClangCompiler.LinkerResults clangLink(Clang.API.Clang.Flags flags, Project project, Workspace workspace, ScriptObject objFiles) {
		return Clang.API.Clang.link(flags, project, workspace, ScriptUtils.toArray<ManilaFile>(objFiles));
	}
}
