using Microsoft.UI.Xaml;

namespace Deppenlabor.Helpers;

internal static class BindingHelper
{
    public static bool Not(bool cond1) => !cond1;

    public static bool And(bool cond1, bool cond2) =>
        // ReSharper disable once IntroduceOptionalParameters.Global
        And(cond1, cond2, true);

    public static bool And(bool cond1, bool cond2, bool invertCond1, bool invertCond2) =>
        And(invertCond1 ? !cond1 : cond1, invertCond2 ? !cond2 : cond2, true);

    public static bool And(bool cond1, bool cond2, bool cond3) => cond1 && cond2 && cond3;

    public static bool NotAnd(bool cond1, bool cond2) => !And(cond1, cond2);

    public static bool Or(bool cond1, bool cond2) =>
        // ReSharper disable once IntroduceOptionalParameters.Global
        Or(cond1, cond2, false);

    public static bool Or(bool cond1, bool cond2, bool cond3) => cond1 || cond2 || cond3;

    public static bool NotOr(bool cond1, bool cond2) => !Or(cond1, cond2);

    public static Visibility AndVisibility(bool cond1, bool cond2) =>
        And(cond1, cond2) ? Visibility.Visible : Visibility.Collapsed;

    public static Visibility NotAndVisibility(bool cond1, bool cond2) =>
        And(cond1, cond2) ? Visibility.Collapsed : Visibility.Visible;

    public static Visibility OrVisibility(bool cond1, bool cond2) =>
        Or(cond1, cond2) ? Visibility.Visible : Visibility.Collapsed;

    public static Visibility NotOrVisibility(bool cond1, bool cond2) =>
        Or(cond1, cond2) ? Visibility.Collapsed : Visibility.Visible;
}