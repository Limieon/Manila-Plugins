
using Manila.Core;
using Manila.Scripting.API;
using ManilaCPP.Configurators;

namespace ManilaCPP.MSBuild;

public static class Generator {
	public static string CPP_PROJECT_GUID = "8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942";

	public static void generateSolution(Workspace workspace, BuildConfig config) {
		var outFile = workspace.location.file($"{workspace.name}.sln");
		var res = new List<string>();

		var projects = new List<Project>();
		foreach (var p in workspace.projects) {
			if (p.configurator == null) continue;

			var type = p.configurator.GetType();
			if (type != typeof(AppProjectConfigurator)) continue;
			projects.Add(p);
		}

		res.Add($"Microsoft Visual Studio Solution File, Format Version 12.00");
		res.Add($"# Visual Studio Version 16");
		res.Add($"VisualStudioVersion = 16.0.30723.126");
		res.Add($"MinimumVisualStudioVersion = 10.0.40219.1");

		foreach (var p in projects) {
			res.Add(getProjectHeader(p, outFile.getFileDirHandle()));
			res.Add("EndProject");

			generateProject(workspace, p, config);
		}

		res.Add("Global");
		res.Add($"\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
		res.Add($"\t\tDebug|x64 = Debug|x64");
		res.Add($"\tEndGlobalSection");
		res.Add($"\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");

		foreach (var p in projects) {
			res.Add($"\t\t{{{p.uuid.ToString().ToUpper()}}}.Debug|x64.ActiveCfg = Debug|x64");
			res.Add($"\t\t{{{p.uuid.ToString().ToUpper()}}}.Debug|x64.Build.0 = Debug|x64");
		}

		res.Add($"\tEndGlobalSection");
		res.Add($"EndGlobal");

		outFile.write(string.Join("\n", res));
	}

	private static string getProjectHeader(Project prj, ManilaDirectory root) {
		return $"Project(\"{{{CPP_PROJECT_GUID}}}\") = \"{prj.name}\", \"{new ManilaFile(prj.location, $"{prj.name}.vcxproj").getPathRelative(root.getPath())}\", \"{{{prj.uuid.ToString().ToUpper()}}}\"";
	}

	private static void generateProject(Workspace workspace, Project project, BuildConfig config) {
		var outFile = project.location.file($"{project.name}.vcxproj");
		var res = new List<string>();

		var binDir = project.getProperty("binDir");
		var objDir = project.getProperty("objDir");
		var includeDirs = project.getProperty("includeDirs");
		var libDirs = project.getProperty("libDirs");
		var srcFiles = project.getProperty("fileSets")["src"];

		res.Add($"<Project DefaultTargets=\"Build\" ToolsVersion=\"16.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
		res.Add($"  <ItemGroup>");
		res.Add($"    <ProjectConfiguration Include=\"Debug|x64\">");
		res.Add($"      <Configuration>Debug</Configuration>");
		res.Add($"      <Platform>x64</Platform>");
		res.Add($"    </ProjectConfiguration>");
		res.Add($"  </ItemGroup>");
		res.Add($"  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.default.props\" />");
		res.Add($"  <PropertyGroup Label=\"Configuration\" Condition=\"'$(Configuration)|$(Platform)'=='Debug|x64'\">");
		res.Add($"    <PreferredToolArchitecture>x64</PreferredToolArchitecture>");
		res.Add($"    <CharacterSet>Unicode</CharacterSet>");
		res.Add($"  </PropertyGroup>");
		res.Add($"  <PropertyGroup>");
		res.Add($"    <ConfigurationType>Application</ConfigurationType>");
		res.Add($"    <PlatformToolset>v143</PlatformToolset>");
		res.Add($"  </PropertyGroup>");
		res.Add($"  <PropertyGroup Condition=\"'$(Configuration)|$(Platform)'=='Debug|x64'\">");
		res.Add($"    <OutDir>{binDir.getPath()}\\</OutDir>");
		res.Add($"    <IntDir>{objDir.getPath()}\\</IntDir>");
		res.Add($"    <TargetName>{project.name}</TargetName>");
		res.Add($"  </PropertyGroup>");
		res.Add($"  <ItemDefinitionGroup Condition=\"'$(Configuration)|$(Platform)'=='Debug|x64'\">");
		res.Add($"    <ClCompile>");
		res.Add($"      <LanguageStandard>stdcpp20</LanguageStandard>");
		foreach (var d in includeDirs) {
			res.Add($"      <AdditionalIncludeDirectories>{d.getPath()}\\</AdditionalIncludeDirectories>");
		}
		res.Add($"    </ClCompile>");
		res.Add($"  </ItemDefinitionGroup>");
		res.Add($"  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.props\" />");
		res.Add($"  <ItemGroup>");
		foreach (var f in srcFiles) {
			res.Add($"    <ClCompile Include=\"{f.getPath()}\" />");
		}
		res.Add($"  </ItemGroup>");

		res.Add($"  <ItemGroup>");
		foreach (var resolver in project.depndencyResolvers) {
			var prj = resolver.resolve();

			res.Add($"    <ProjectReference Include=\"{Utils.getVCXProjFile(prj).getPathRelative(project.location)}\">");
			res.Add($"      <Project>{{{prj.uuid.ToString().ToUpper()}}}</Project>");
			res.Add($"    </ProjectReference>");
		}
		res.Add($"  </ItemGroup>");

		res.Add($"  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.Targets\" />");
		res.Add($"</Project>");
		res.Add($"");

		outFile.write(string.Join("\n", res));
	}
}
