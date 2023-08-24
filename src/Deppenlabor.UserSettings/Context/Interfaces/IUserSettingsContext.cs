using Deppenlabor.UserSettings.Models;

namespace Deppenlabor.UserSettings.Context.Interfaces;

public interface IUserSettingsContext
{
    GitLabAccounts GitLabAccounts { get; set; }

    Task SaveChanges();
}