
using Manila.Core;

namespace ManilaCPP.MSBuild;

public static class Compiler {
	public static class FromGeneric {
		public static string architecture(string arch) {
			switch (arch) {
				case "x86": return "Win32";
				case "Win32": return "Win32";
				case "x64": return "x64";
			}
			return "";
		}
	}

	public static class BuildConfigConverter {
		public static string arch(CPPBuildConfig.Arch arch) {
			return
				arch == CPPBuildConfig.Arch.X86 ? "Win32" :
				arch == CPPBuildConfig.Arch.X64 ? "x64" : ""
			;
		}
	}
}
