
using Manila.Scripting.API;

namespace ManilaCPP.MSBuild;

public class SolutionFile {
	public string name { get; private set; }

	public List<ManilaFile> projectFiles;

	public SolutionFile(string name) {
		this.name = name;

		projectFiles = new List<ManilaFile>();
	}
}
