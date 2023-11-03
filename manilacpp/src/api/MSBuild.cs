
using System.Diagnostics;
using Manila.Core;
using Manila.Scripting.API;
using ManilaCPP.Configurators;
using ManilaCPP.MSBuild;

namespace ManilaCPP.API;

public static class MSBuild {
	private static ManilaCPP plugin = ManilaCPP.instance;

	public static void generate(Workspace workspace, BuildConfig config) {
		Generator.generateSolution(workspace, config);
	}
}
