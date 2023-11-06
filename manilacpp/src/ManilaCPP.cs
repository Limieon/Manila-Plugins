
using Manila.Plugin.API;
using ManilaCPP.Configurators;

namespace ManilaCPP;

public class ManilaCPP : Plugin {
	public static ManilaCPP instance { get; private set; }

	public ManilaCPP() : base("manilacpp") { instance = this; }

	public override void init() {
		base.init();
		setBuildConfig(new CPPBuildConfig());

		addProjectConfigurator("application", new AppProjectConfigurator());
	}
	public override void shutdown() {
		base.shutdown();
	}
}
