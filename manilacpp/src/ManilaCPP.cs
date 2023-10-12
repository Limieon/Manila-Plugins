
using Manila.Plugin.API;

namespace ManilaCPP;

public class ManilaCPP : Plugin {
	public static ManilaCPP instance { get; private set; }

	public ManilaCPP() : base("manilacpp") { instance = this; }

	public override void init() {
		base.init();
		setBuildConfig(new CPPBuildConfig());

		addType(typeof(API.MSBuild));
	}
	public override void shutdown() {
		base.shutdown();

		debug("Project Files:");
		foreach (var f in API.MSBuild.files) {
			debug(f.project.name);
		}
	}
}
