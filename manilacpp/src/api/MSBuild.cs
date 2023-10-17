
using System.Diagnostics;
using Manila.Core;
using Manila.Scripting.API;
using ManilaCPP.MSBuild;

namespace ManilaCPP.API;

public static class MSBuild {
	private static ManilaCPP plugin = ManilaCPP.instance;

	public static List<ProjectFile> files = new List<ProjectFile>();

	public class Flags {
		public enum BinaryType {
			CONSOLE_APP, STATIC_LIB, DYNAMIC_LIB
		}

		public ManilaDirectory binDir;
		public ManilaDirectory objDir;

		public ManilaFile[] srcFiles;

		public List<ManilaDirectory> includeDirs = new List<ManilaDirectory>();
		public List<ManilaDirectory> libDirs = new List<ManilaDirectory>();

		public BinaryType binaryType = BinaryType.CONSOLE_APP;

		public bool debug = true;
	}

	public static Flags flags() {
		return new Flags();
	}

	public static Flags.BinaryType consoleApp() { return Flags.BinaryType.CONSOLE_APP; }
	public static Flags.BinaryType staticLib() { return Flags.BinaryType.STATIC_LIB; }
	public static Flags.BinaryType dynamicLib() { return Flags.BinaryType.DYNAMIC_LIB; }

	public static ManilaFile build(Workspace workspace, Project project, BuildConfig config, Flags flags) {
		var prj = new ProjectFile(project, flags.objDir, flags.binDir);

		prj.srcFiles.AddRange(flags.srcFiles);
		prj.includeDirs.AddRange(flags.includeDirs);
		prj.libDirs.AddRange(flags.libDirs);

		plugin.debug("Bin Dir: " + prj.binDir.getPath());
		plugin.debug("Obj Dir: " + prj.objDir.getPath());

		plugin.debug("Src Files:");
		foreach (var f in prj.srcFiles) {
			plugin.debug($"  {f.getPath()}");
		}

		plugin.debug("Include Dirs:");
		foreach (var f in prj.includeDirs) {
			plugin.debug($"  {f.getPath()}");
		}

		plugin.debug("Lib Dirs:");
		foreach (var f in prj.libDirs) {
			plugin.debug($"  {f.getPath()}");
		}

		prj.generate((CPPBuildConfig) config, flags);

		var dir = Directory.GetCurrentDirectory();
		Directory.SetCurrentDirectory(project.location.getPath());

		var i = new ProcessStartInfo("msbuild.exe");
		i.Arguments = Compiler.arguments((CPPBuildConfig) config, flags);

		var p = Process.Start(i);
		p.WaitForExit();

		ManilaCPP.instance.debug("Exiting Dir...");
		Directory.SetCurrentDirectory(dir);

		files.Add(prj);

		return new ManilaFile(flags.binDir.getPath(), $"{project.name}.exe");
	}
}
