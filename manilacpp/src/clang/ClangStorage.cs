
using Manila.Scripting.API;
using Manila.Plugin.API;
using ManilaCPP;

public class ClangStorage : Storage {
	public class Data {
		public string? name = null;
	}

	public ClangStorage() : base("clang") { }

	public Data? data;

	public override void deserialize(ManilaFile file) {
		data = file.deserializeJSON<Data>();
	}
	public override void serialize(ManilaFile file) {
		file.serializeJSON(data, true);
	}
}
