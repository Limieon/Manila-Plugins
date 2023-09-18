
using System.Diagnostics;
using ClangSharp.Interop;
using Manila.Core;
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Utils;

namespace ManilaCPP.Clang;

internal class ClangCompiler {
	public class CompileResults {
		internal CompileResults(ManilaFile objFile, bool success) {
			this.objFile = objFile;
			this.success = success;
		}

		public readonly ManilaFile objFile;
		public readonly bool success;
	}

	internal ClangCompiler(string clangBinary) {
	}

	internal ManilaFile compileToObj(ManilaFile src, API.Clang.Flags flags, Project project, Workspace workspace) {
		var objFile = new ManilaFile(flags.objDir, src.getPathRelative(project.location.getPath())).setExtension("obj");
		ManilaCPP.instance.debug("OBJFile:", objFile.getPath());

		return objFile;
	}
}
