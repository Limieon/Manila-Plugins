
using Manila;
using Manila.Plugin.API;

using ManilaCPP.Clang;

namespace ManilaCPP;

public class ManilaCPP: Plugin {
	public static ManilaCPP instance { get; private set; }

	public ManilaCPP(): base("manilacpp") {
		instance = this;
	}

	public override void init() {
		debug("Initializing...");
		addType(typeof(Clang.Clang));
	}
	public override void shutdown() {
		debug("Shutting down...");
	}
}
