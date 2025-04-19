using MintPlayer.ValueComparerGenerator.Attributes;

namespace SettingsGenerator.Generators;

[AutoValueComparer]
public partial class SettingInfo
{
    public string? Name { get; set; }
    public string? SettingType { get; set; }
}
