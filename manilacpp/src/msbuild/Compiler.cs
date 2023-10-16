
using Manila.Core;

namespace ManilaCPP.MSBuild;

public static class Compiler {
	public static class Arguments {
		public enum Verbosity {
			QUIET, MINIMAL, NORMAL, DETAILED, DIAGNOSTIC
		}

		public static string verbosity(Verbosity v) {
			switch (v) {
				case Verbosity.QUIET: return "-verbosity:quiet";
				case Verbosity.MINIMAL: return "-verbosity:minimal";
				case Verbosity.NORMAL: return "-verbosity:normal";
				case Verbosity.DETAILED: return "-verbosity:detailed";
				case Verbosity.DIAGNOSTIC: return "-verbosity:diagnostic";
			}
			return "";
		}
		public static string detailedSummary(bool v = true) {
			return v ? "-detailedSummary:true" : "-detailedSummary:false";
		}
		public static string maxCpuCount(int v = 1) {
			return "-maxCpuCount:" + v;
		}
		public static string config(string config) {
			return "/property:Configuration=" + config;
		}
		public static string platform(CPPBuildConfig.Arch arch) {
			return "/property:Platform=" + BuildConfigConverter.arch(arch);
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

	public static string arguments(CPPBuildConfig config, API.MSBuild.Flags flags) {
		List<string> args = new List<string>(new string[] {
			Arguments.verbosity(Arguments.Verbosity.MINIMAL),
			Arguments.detailedSummary(false),
			Arguments.maxCpuCount(),
			Arguments.config(config.config),
			Arguments.platform(config._arch),
			"-nologo"
		});

		return string.Join(" ", args);
	}
}
