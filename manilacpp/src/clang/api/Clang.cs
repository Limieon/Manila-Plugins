
using Manila.Core;
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Utils;
using Microsoft.ClearScript;

namespace ManilaCPP.Clang.API;

public static class Clang {
	public class Flags {
		public string name { get; set; }
		public ManilaDirectory objDir { get; set; }
		public ManilaDirectory binDir { get; set; }
		public ManilaFile[] files { get; set; }
	}
	public class Results {
	}

	internal static Flags flags() { return new Flags(); }

	internal static Results compile(Flags flags, Project project, Workspace workspace) {
		ManilaCPP.instance.info("Compiling", flags.name + "...");
		ManilaCPP.instance.debug("BinDir:", flags.binDir.getPath());
		ManilaCPP.instance.debug("ObjDir:", flags.objDir.getPath());

		var clang = new ClangCompiler("clang");
		foreach (var f in flags.files) {
			ManilaCPP.instance.info(f.getFileName());
			clang.compileToObj(f, flags, project, workspace);
		}

		var results = new Results();
		return results;
	}
}
