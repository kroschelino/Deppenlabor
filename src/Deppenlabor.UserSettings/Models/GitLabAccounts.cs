using System.Collections.ObjectModel;

namespace Deppenlabor.UserSettings.Models;

public partial class GitLabAccounts
{
    public ObservableCollection<GitLab> Accounts { get; set; }

    public GitLabAccounts()
    {
        Accounts = new ObservableCollection<GitLab>
        {
            new(),
        };
    }
}