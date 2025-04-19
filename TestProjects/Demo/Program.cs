using SettingsGenerator.Attributes;

Console.WriteLine("Hello, World!");

public static class Settings
{
    public static SettingsSaver<SettingType1> Name1()
    {
        return new SettingsSaver<SettingType1>();
    }
    public static SettingsSaver<SettingType2> Name2()
    {
        return new SettingsSaver<SettingType2>();
    }
    public static SettingsSaver<SettingType3> Name3()
    {
        return new SettingsSaver<SettingType3>();
    }
}

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