
using System.Diagnostics;
using Manila.Core;
using Manila.Scripting.API;
using Manila.Utils;
using Microsoft.ClearScript.JavaScript;

namespace ManilaCPP.Clang;

public class ClangCompiler {
	private readonly string clangBinary;

	private static readonly ManilaCPP plugin = ManilaCPP.instance;

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
		this.clangBinary = clangBinary;
	}

	internal CompilerResults compile(API.Clang.Flags flags, Project project, Workspace workspace, ManilaFile src) {
		var objFile = new ManilaFile(flags.objDir, src.getPathRelative(project.location.getPath())).setExtension("obj");
		var outputDir = objFile.getFileDirHandle();
		if (!outputDir.exists()) outputDir.create();

		executeCommand(
			src.getPath(), // Sets the input file
			"-c", // Says clang to compile to obj
			"-o", objFile.getPath() // Sets the output file
		);

		return new CompilerResults(objFile, true);
	}
	internal LinkerResults link(API.Clang.Flags flags, Project project, Workspace workspace, ManilaFile[] objFiles) {
		var binaryFile = new ManilaFile(flags.binDir, $"{flags.name}{Utils.getBinaryFileEndingByPlatform(flags.platform)}");
		if (!flags.binDir.exists()) flags.binDir.create();

		plugin.debug($"Linking {objFiles.Length} obj file(s) into executeable...");

		List<string> objFilesList = new List<string>();
		foreach (var f in objFiles) objFilesList.Add(f.getPath());

		executeCommand(
			string.Join(" ", objFilesList),
			"-o", binaryFile.getPath()
		);

		plugin.debug("Binary File:", binaryFile.getPath());
		return new LinkerResults(binaryFile);
	}

	internal void executeCommand(params string[] args) {
		var i = new ProcessStartInfo(clangBinary);
		i.Arguments = string.Join(" ", args);

		var process = Process.Start(i);
		process?.WaitForExit();
	}
}
