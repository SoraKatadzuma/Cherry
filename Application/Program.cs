using System.Text;
using Cherry.Application.Commands;
using Semver;
using Spectre.Console;
using Spectre.Console.Cli;

const int    kMajor      = 0;
const int    kMinor      = 1;
const int    kPatch      = 0;
const string kPrerelease = "alpha";
const string kMetaInfo   = "dev";
const string kTextLogo   = """
 ::::::::  :::    ::: :::::::::: :::::::::  :::::::::  :::   :::
:+:    :+: :+:    :+: :+:        :+:    :+: :+:    :+: :+:   :+:
+:+        +:+    +:+ +:+        +:+    +:+ +:+    +:+  +:+ +:+ 
+#+        +#++:++#++ +#++:++#   +#++:++#:  +#++:++#:    +#++:  
+#+        +#+    +#+ +#+        +#+    +#+ +#+    +#+    +#+   
#+#    #+# #+#    #+# #+#        #+#    #+# #+#    #+#    #+#   
 ########  ###    ### ########## ###    ### ###    ###    ###   
""";

AnsiConsole.WriteLine(Banner());

var app = new CommandApp();
app.Configure(config => {
    config.AddCommand<BuildCommand>("build");
    config.AddCommand<ConfigureCommand>("config");
    config.AddCommand<InitializeCommand>("init");
    config.AddCommand<InstallCommand>("install");
    config.AddCommand<InterpretCommand>("interpret");
    config.AddCommand<PackageCommand>("package");
});
app.Run(args);

static string Version() {
    return SemVersion.ParsedFrom(kMajor, kMinor, kPatch, kPrerelease, kMetaInfo)
                     .ToString();
}

static string Banner() {
    var stringBuilder  = new StringBuilder();
    var versionPadding = kTextLogo.IndexOf('\n');
    var versionString  = Version().PadLeft(versionPadding - 4);
    stringBuilder.AppendLine(kTextLogo);
    stringBuilder.AppendLine(versionString);
    return stringBuilder.ToString();
}