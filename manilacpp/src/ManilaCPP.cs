
using Manila.Plugin.API;

namespace ManilaCPP;

public class ManilaCPP : Plugin {
	public static ManilaCPP instance { get; private set; }

	public ManilaCPP() : base("manilacpp") { instance = this; }

	public override void init() {
		base.init();
		setBuildConfig(new CPPBuildConfig());

		addType(typeof(API.MSBuild));

		addProjectConfigurator("application", new ApplicationProjectConfigurator());
	}
	public override void shutdown() {
		base.shutdown();
	}
}
