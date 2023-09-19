
using System.Diagnostics;
using ClangSharp.Interop;
using Manila.Core;
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Utils;

namespace ManilaCPP.Clang;

internal class ClangCompiler {
	public class CompileResults {
		public CompileResults(ManilaFile objFile, bool success) {
			this.objFile = objFile;
			this.success = success;
		}

		public readonly ManilaFile objFile;
		public readonly bool success;
	}
	public class LinkerResults {
		public LinkerResults() {
			success = false;
		}
		public LinkerResults(ManilaFile binary) {
			this.binary = binary;
			success = true;
		}

		public readonly ManilaFile binary;
		public readonly bool success;
	}

	internal ClangCompiler(string clangBinary) {
	}

	internal ManilaFile compileToObj(ManilaFile src, API.Clang.Flags flags, Project project, Workspace workspace) {
		var objFile = new ManilaFile(flags.objDir, src.getPathRelative(project.location.getPath())).setExtension("obj");
		ManilaCPP.instance.debug("OBJFile:", objFile.getPath());

		return objFile;
	}
	internal LinkerResults link(ManilaFile[] objFiles, API.Clang.Flags flags, Project project, Workspace workspace) {
		var binaryFile = new ManilaFile(flags.binDir, flags.name);
		ManilaCPP.instance.debug("Binary File:", binaryFile.getPath());

		return new LinkerResults(binaryFile);
	}
}
