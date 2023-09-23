
namespace ManilaCPP;

public static class Utils {
	public static string getBinaryFileEndingByPlatform(string platform) {
		switch (platform.ToLower()) {
			case "windows": return ".exe";
			case "linux": return "";
			case "macosx": return "";
			case "freebsd": return "";
			default: return "";
		}
	}
}
