
using Manila.Core;
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Utils;
using Microsoft.ClearScript;

namespace ManilaCPP.Clang.API;

public static class Clang {
	internal static readonly ClangCompiler clang = new ClangCompiler("clang");

	public class Flags {
		public string name { get; set; }
		public ManilaDirectory objDir { get; set; }
		public ManilaDirectory binDir { get; set; }
		public ManilaFile[] files { get; set; }
	}

	internal static Flags flags() { return new Flags(); }

	internal static ClangCompiler.CompilerResults compile(Flags flags, Project project, Workspace workspace, ManilaFile file) {
		return clang.compile(flags, project, workspace, file);
	}
	internal static ClangCompiler.LinkerResults link(Flags flags, Project project, Workspace workspace, ManilaFile[] objFiles) {
		return clang.link(flags, project, workspace, objFiles);
	}
}
