namespace Deppenlabor.UserSettings.Services.Interfaces;

public interface IUserSettingsService
{
    bool LoadSetting(Type type, object defaultSetting);
    void SaveSetting(Type type, object value);
}