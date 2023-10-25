
using Manila.Plugin.API;
using ManilaCPP.Templates;

namespace ManilaCPP;

public class ManilaCPP : Plugin {
	public static ManilaCPP instance { get; private set; }

	public ManilaCPP() : base("manilacpp") { instance = this; }

	public override void init() {
		base.init();
		setBuildConfig(new CPPBuildConfig());

		addType(typeof(API.MSBuild));

		addScriptTemplate(new TemplateApplication());
	}
	public override void shutdown() {
		base.shutdown();
	}
}
