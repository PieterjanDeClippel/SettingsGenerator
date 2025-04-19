using SettingsGenerator.Attributes;

Console.WriteLine("Hello, World!");

Demo.Settings.Name1.Save(new SettingType1());
SettingType1? setting1 = Demo.Settings.Name1.Load();

Demo.Settings.Name2.Save(new SettingType2());
SettingType2? setting2 = Demo.Settings.Name2.Load();

Demo.Settings.Name3.Save(new SettingType3());
SettingType3? setting3 = Demo.Settings.Name3.Load();

public enum ESettingNames
{
    [SettingType(typeof(SettingType1))] Name1,
    [SettingType(typeof(SettingType2))] Name2,
    [SettingType(typeof(SettingType3))] Name3,
}

public class SettingType1
{

}

public class SettingType2
{

}

public class SettingType3
{

}