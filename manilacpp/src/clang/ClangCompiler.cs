
using System.Diagnostics;
using ClangSharp.Interop;
using Manila.Core;
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Utils;

namespace ManilaCPP.Clang;

public class ClangCompiler {
	public class CompilerResults {
		public CompilerResults(ManilaFile objFile, bool success) {
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

	internal CompilerResults compile(API.Clang.Flags flags, Project project, Workspace workspace, ManilaFile src) {
		var objFile = new ManilaFile(flags.objDir, src.getPathRelative(project.location.getPath())).setExtension("obj");
		ManilaCPP.instance.debug("OBJFile:", objFile.getPath());
		return new CompilerResults(objFile, true);
	}
	internal LinkerResults link(API.Clang.Flags flags, Project project, Workspace workspace, ManilaFile[] objFiles) {
		var binaryFile = new ManilaFile(flags.binDir, flags.name);
		ManilaCPP.instance.debug("Binary File:", binaryFile.getPath());
		return new LinkerResults(binaryFile);
	}
}
