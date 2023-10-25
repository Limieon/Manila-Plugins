
using Manila.Core;
using Manila.Plugin.API;
using Manila.Scripting.API;
using Manila.Utils;
using Microsoft.ClearScript;

namespace ManilaCPP.Templates;

public class TemplateApplication : ScriptTemplate {
	public TemplateApplication() : base(ManilaCPP.instance, "application") {
	}

	public override Project getProject() {
		ManilaCPP.instance.debug("Applying application template...");

		var project = (Project) ScriptManager.currentScriptInstance;
		project.registerProperty("binDir", typeof(ManilaDirectory));
		project.registerProperty("objDir", typeof(ManilaDirectory));

		project.registerPropertyFunction<ScriptObject>("files", (fileGlobs) => {
			var arr = ScriptUtils.toArray<string>(fileGlobs);

			if (!project.properties.ContainsKey("srcFiles")) project.properties["srcFiles"] = new List<string>();
			((List<string>) project.properties["srcFiles"]).AddRange(arr);
		});

		return project;
	}
}
