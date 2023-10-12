
using Manila.Core;

namespace ManilaCPP;

public class CPPBuildConfig : BuildConfig {
	public List<string> arch_VALID = new List<string>(new string[] {
		"x64",
		"x86"
	});

	public string arch = "x86";
	public string config = "Debug";
}
