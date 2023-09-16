
using System.Diagnostics;
using ClangSharp.Interop;
using Manila.Plugin.API;

namespace ManilaCPP.Clang;

public class Flags {
	public string name { get; set; }
}

public static class Clang {
	public static Flags flags() { return new Flags(); }

	public static void compile(Flags flags) {
		ManilaCPP.instance.info("Compiling", flags.name + "...");
	}
}
