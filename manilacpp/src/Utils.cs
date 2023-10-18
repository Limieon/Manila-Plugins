
using Manila.Core;
using Manila.Scripting.API;

namespace ManilaCPP;

public static class Utils {
	public static ManilaFile getVCXProjFile(Project project) {
		return new ManilaFile(project.location, $"{project.name}.vcxproj");
	}
}
