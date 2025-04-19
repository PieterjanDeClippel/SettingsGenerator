using System;

namespace SettingsGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SettingTypeAttribute : Attribute
    {
        public SettingTypeAttribute(Type settingType)
        {
        }
    }
}
