
using Manila.Core;
using Manila.Core.Exceptions;

namespace ManilaCPP;

public class CPPBuildConfig : BuildConfig {
	public enum Arch {
		X86, X64
	}

	public static Arch? archFromString(string s) {
		switch (s.ToLower()) {
			case "x64": return Arch.X64;
			case "x86": return Arch.X86;
		}
		throw new WrongConfigValueException(s, new string[] { "x64", "x86" });
	}

	public Arch _arch = Arch.X86;
	public string arch;
	public string config = "Debug";
}
