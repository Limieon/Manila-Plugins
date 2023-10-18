
using Manila.Core;
using Manila.Scripting.API;

namespace ManilaCPP.MSBuild;

public class SolutionFile {
	public static void generate(Workspace workspace, ManilaFile file) {
		List<string> res = new List<string>();
		res.Add($"Microsoft Visual Studio Solution File, Format Version 12.00");
		res.Add($"# Visual Studio Version 16");
		res.Add($"VisualStudioVersion = 16.0.30723.126");
		res.Add($"MinimumVisualStudioVersion = 10.0.40219.1");

		foreach (var prj in workspace.projects) {
			res.Add(getProjectHeader(prj, file.getFileDirHandle()));
			res.Add("EndProject");
		}

		res.Add("Global");
		res.Add($"\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
		res.Add($"\t\tDebug|x64 = Debug|x64");
		res.Add($"\tEndGlobalSection");
		res.Add($"\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");

		foreach (var prj in workspace.projects) {
			res.Add($"\t\t{{{prj.uuid.ToString().ToUpper()}}}.Debug|x64.ActiveCfg = Debug|x64");
			res.Add($"\t\t{{{prj.uuid.ToString().ToUpper()}}}.Debug|x64.Build.0 = Debug|x64");
		}

		res.Add($"\tEndGlobalSection");
		res.Add($"EndGlobal");

		file.write(res);
	}

	private static string getProjectHeader(Project prj, ManilaDirectory root) {
		return $"Project(\"{{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}}\") = \"{prj.name}\", \"{new ManilaFile(prj.location, $"{prj.name}.vcxproj").getPathRelative(root.getPath())}\", \"{{{prj.uuid.ToString().ToUpper()}}}\"";
	}
}
