
using Manila.Plugin.API;

namespace ManilaCPP;

public class ManilaCPP: Plugin {
	public ManilaCPP(): base("manilacpp") {
	}

	public override void init() {
		print("Initializing...");
	}
	public override void shutdown() {
		print("Shutting down...");
	}
}
