
using Manila.Core;

namespace ManilaCPP.Clang;

public class ClangBuildConfig : BuildConfig {
	public enum Architecture {
		x86,
		x64
	}
	public static Architecture architectureFromString(string str) {
		if (str.ToLower() == "x86") return Architecture.x86;
		if (str.ToLower() == "x64") return Architecture.x64;
		throw new ArgumentException("String '" + str + "' could not be converted to " + typeof(Architecture).FullName + "!");
	}

	public string config;
	public Architecture arch;

	public ClangBuildConfig() : base() { }
}
