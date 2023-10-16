
using Manila.Core;
using Manila.Scripting.API;

namespace ManilaCPP.MSBuild;

public class ProjectFile {
	public readonly Project project;

	public List<ManilaFile> srcFiles;
	public List<ManilaDirectory> includeDirs;
	public List<ManilaDirectory> libDirs;

	public ManilaDirectory objDir { get; private set; }
	public ManilaDirectory binDir { get; private set; }

	public List<string> configs;
	public List<string> platforms;

	public ProjectFile(Project project, ManilaDirectory objDir, ManilaDirectory binDir) {
		this.project = project;
		this.objDir = objDir;
		this.binDir = binDir;

		srcFiles = new List<ManilaFile>();
		includeDirs = new List<ManilaDirectory>();
		libDirs = new List<ManilaDirectory>();

		configs = new List<string>();
		platforms = new List<string>();
	}

	public void generate(CPPBuildConfig config) {
		List<string> lines = new List<string>();
		string arch = Compiler.BuildConfigConverter.arch(config._arch);

		lines.Add($"<Project DefaultTargets=\"Build\" ToolsVersion=\"16.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
		lines.Add($"  <ItemGroup>");
		lines.Add($"    <ProjectConfiguration Include=\"{config.config}|{arch}\">");
		lines.Add($"      <Configuration>{config.config}</Configuration>");
		lines.Add($"      <Platform>{Compiler.FromGeneric.architecture(arch)}</Platform>");
		lines.Add($"    </ProjectConfiguration>");
		lines.Add($"  </ItemGroup>");
		lines.Add($"  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.default.props\" />");
		lines.Add($"  <PropertyGroup Label=\"Configuration\" Condition=\"'$(Configuration)|$(Platform)'=='{config.config}|{arch}'\">");
		lines.Add($"    <PreferredToolArchitecture>x64</PreferredToolArchitecture>");
		lines.Add($"    <CharacterSet>Unicode</CharacterSet>");
		lines.Add($"  </PropertyGroup>");
		lines.Add($"  <PropertyGroup>");
		lines.Add($"    <ConfigurationType>Application</ConfigurationType>");
		lines.Add($"    <PlatformToolset>v143</PlatformToolset>");
		lines.Add($"  </PropertyGroup>");
		lines.Add($"  <PropertyGroup Condition=\"'$(Configuration)|$(Platform)'=='{config.config}|{arch}'\">");
		lines.Add($"    <OutDir>{binDir.getPath()}\\</OutDir>");
		lines.Add($"    <IntDir>{objDir.getPath()}\\</IntDir>");
		lines.Add($"    <TargetName>{project.name}</TargetName>");
		lines.Add($"  </PropertyGroup>");
		lines.Add($"  <ItemDefinitionGroup Condition=\"'$(Configuration)|$(Platform)'=='{config.config}|{arch}'\">");
		lines.Add($"    <ClCompile>");
		lines.Add($"      <LanguageStandard>stdcpp20</LanguageStandard>");
		foreach (var d in includeDirs) {
			lines.Add($"      <AdditionalIncludeDirectories>{d.getPath()}\\</AdditionalIncludeDirectories>");
		}
		lines.Add($"    </ClCompile>");
		lines.Add($"  </ItemDefinitionGroup>");
		lines.Add($"  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.props\" />");
		lines.Add($"  <ItemGroup>");
		foreach (var f in srcFiles) {
			lines.Add($"    <ClCompile Include=\"{f.getPath()}\" />");
		}
		lines.Add($"  </ItemGroup>");
		lines.Add($"  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.Targets\" />");
		lines.Add($"</Project>");
		lines.Add($"");

		var file = new ManilaFile(project.location, $"{project.name}.vcxproj");
		ManilaCPP.instance.debug("Path: " + file.getPath());
		file.write(string.Join("\n", lines));
	}
}
