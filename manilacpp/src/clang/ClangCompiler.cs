
using System.Diagnostics;
using Manila.Core;
using Manila.Scripting.API;

namespace ManilaCPP.Clang;

public class ClangCompiler {
	private readonly string clangBinary;

	private static readonly ManilaCPP plugin = ManilaCPP.instance;

	public class CompilerResults {
		public ManilaFile objFile { get; internal set; }
		public bool success { get; internal set; }
		public bool skipped { get; internal set; }
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
		var currentChecksum = src.checksum();

		var results = new CompilerResults();
		results.success = true;
		results.objFile = objFile;

		plugin.debug("Checksum:", currentChecksum);

		if (!outputDir.exists()) outputDir.create();

		if (!flags.force && objFile.exists() && plugin.clangCompilerStorage.data.lastChecksum.ContainsKey(src.getPath()) && plugin.clangCompilerStorage.data.lastChecksum[src.getPath()] == currentChecksum) {
			plugin.debug("Skipped!");
			results.skipped = true;

			return results;
		}

		executeCommand(
			src.getPath(), // Sets the input file
			"-c", // Says clang to compile to obj
			"-o", objFile.getPath() // Sets the output file
		);

		if (plugin.clangCompilerStorage.data.lastChecksum.ContainsKey(src.getPath()))
			plugin.clangCompilerStorage.data.lastChecksum[src.getPath()] = currentChecksum;
		else
			plugin.clangCompilerStorage.data.lastChecksum.Add(src.getPath(), currentChecksum);

		return results;
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
