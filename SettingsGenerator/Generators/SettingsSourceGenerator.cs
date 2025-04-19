using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MintPlayer.SourceGenerators.Tools;

namespace SettingsGenerator.Generators;

[Generator(LanguageNames.CSharp)]
public class SettingsSourceGenerator : IncrementalGenerator
{
    public override void RegisterComparers() { }

    public override void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<Settings> settingsProvider)
    {
        var enumDeclarationsProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, ct) => node is EnumMemberDeclarationSyntax { AttributeLists.Count: > 0 } enumMemberDeclaration,
                static (context, ct) =>
                {
                    if (context.Node is EnumMemberDeclarationSyntax enumMemberDeclaration &&
                        context.SemanticModel.GetDeclaredSymbol(enumMemberDeclaration, ct) is IFieldSymbol enumFieldSymbol)
                    {
                        var settingTypeAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName("SettingsGenerator.Attributes.SettingTypeAttribute");
                        var enumSymbol = enumFieldSymbol.ContainingType;
                        var attributeData = enumFieldSymbol.GetAttributes()
                            .FirstOrDefault(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, settingTypeAttributeSymbol));

                        if (attributeData is { ConstructorArguments.Length: > 0 } &&
                            attributeData.ConstructorArguments[0].Value is INamedTypeSymbol settingTypeSymbol)
                        {
                            return new SettingInfo
                            {
                                Name = enumFieldSymbol.Name,
                                SettingType = settingTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Included)),
                            };
                        }
                    }

                    return default;
                })
            .WithComparer(SettingInfoValueComparer.Instance)
            .Collect();

        var enumDeclarationsSourceProvider = enumDeclarationsProvider
            .Combine(settingsProvider)
            .Select(static Producer (p, ct) => new SettingsProducer(p.Left, p.Right.RootNamespace!));

        context.ProduceCode(enumDeclarationsSourceProvider);
    }
}
