
using Manila.Core;

namespace ManilaCPP.Clang;

public class ClangBuildConfig : BuildConfig {
	public static readonly List<string> ARCH_VALID = new List<string>(new string[] {
		"x86",
		"x64"
	});

	public string config;
	public string arch = "x86";

	public ClangBuildConfig() : base() { }
}
