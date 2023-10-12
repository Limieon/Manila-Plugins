
using Manila.Core;
using Manila.Scripting.API;
using ManilaCPP.MSBuild;

namespace ManilaCPP.API;

public static class MSBuild {
	private static ManilaCPP plugin = ManilaCPP.instance;

	public static List<ProjectFile> files = new List<ProjectFile>();

	public class Flags {
		public ManilaDirectory binDir;
		public ManilaDirectory objDir;

		public FileSet srcFiles;

		public List<ManilaDirectory> includeDirs = new List<ManilaDirectory>();
		public List<ManilaDirectory> libDirs = new List<ManilaDirectory>();

		public bool debug = true;
	}

	public static Flags flags() {
		return new Flags();
	}

	public static void build(Workspace workspace, Project project, BuildConfig config, Flags flags) {
		var prj = new ProjectFile(project, flags.objDir, flags.binDir);

		prj.srcFiles.AddRange(flags.srcFiles.files());
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

		prj.generate();

		files.Add(prj);
	}
}
