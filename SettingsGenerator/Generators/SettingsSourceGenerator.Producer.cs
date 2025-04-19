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

    }
}
