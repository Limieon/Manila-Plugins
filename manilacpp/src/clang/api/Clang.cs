
using Manila.Core;
using Manila.Scripting.API;

namespace ManilaCPP.Clang.API;

public static class Clang {
	internal static readonly ClangCompiler clang = new ClangCompiler(@"clang++.exe");

	public class Flags {
		/// <summary>
		/// The base output name of the binary
		/// </summary>
		public string name { get; set; }
		/// <summary>
		/// The directory containing the obj files
		/// </summary>
		public ManilaDirectory objDir { get; set; }
		/// <summary>
		/// The directory containing the binary files
		/// </summary>
		public ManilaDirectory binDir { get; set; }
		/// <summary>
		/// The platform to compile for
		/// </summary>
		public string platform { get; set; }
		/// <summary>
		/// Forces compilation on every file
		/// </summary>
		public bool force { get; set; }
	}

	internal static Flags flags() { return new Flags(); }

	internal static ClangCompiler.CompilerResults compile(Flags flags, Project project, Workspace workspace, ManilaFile file) {
		return clang.compile(flags, project, workspace, file);
	}
	internal static ClangCompiler.LinkerResults link(Flags flags, Project project, Workspace workspace, ManilaFile[] objFiles) {
		return clang.link(flags, project, workspace, objFiles);
	}
}
