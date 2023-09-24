
using Manila.Scripting.API;
using Manila.Plugin.API;
using ManilaCPP;

public class ClangCompilerStorage : Storage {
	public class Data {
		/// <summary>
		/// When a file gets compiled, the checksum and file path is stored inside this Map
		/// </summary>
		public Dictionary<string, string> lastChecksum = new Dictionary<string, string>();
	}

	public ClangCompilerStorage() : base("clang_compiler") { }

	public Data? data;

	public override void deserialize(ManilaFile file) {
		data = file.deserializeJSON<Data>();
	}
	public override void serialize(ManilaFile file) {
		file.serializeJSON(data, true);
	}
}
