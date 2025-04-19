using MintPlayer.SourceGenerators.Tools;
using System.CodeDom.Compiler;

namespace SettingsGenerator.Generators;

public class SettingsProducer : Producer
{
    private readonly IEnumerable<SettingInfo> settingInfos;
    public SettingsProducer(IEnumerable<SettingInfo> settingInfos, string rootNamespace) : base(rootNamespace, "Settings.cs")
    {
        this.settingInfos = settingInfos;
    }

    protected override void ProduceSource(IndentedTextWriter writer, CancellationToken cancellationToken)
    {
        writer.WriteLine(Header);
        writer.WriteLine($"namespace {RootNamespace}");
        writer.WriteLine("{");
        writer.Indent++;

        writer.WriteLine("public static class Settings");
        writer.WriteLine("{");
        writer.Indent++;

        foreach (var setting in settingInfos)
        {
            writer.WriteLine($$"""public SettingsSaver<{{setting.SettingType}}> {{setting.Name}} { get; } = new SettingsSaver<{{setting.SettingType}}>();""");
        }

        writer.Indent--;
        writer.WriteLine("}");


        writer.WriteLine("public class SettingsSaver<TSetting>");
        writer.WriteLine("{");
        writer.Indent++;

        writer.WriteLine("public void Save(TSetting setting)");
        writer.WriteLine("{");
        writer.Indent++;

        writer.WriteLine("// Save logic here");

        writer.Indent--;
        writer.WriteLine("}");

        writer.WriteLine("public TSetting? Load()");
        writer.WriteLine("{");
        writer.Indent++;

        writer.WriteLine("// Load logic here");
        writer.WriteLine("return default(TSetting);");

        writer.Indent--;
        writer.WriteLine("}");

        writer.Indent--;
        writer.WriteLine("}");

        writer.Indent--;
        writer.WriteLine("}");
    }
}
