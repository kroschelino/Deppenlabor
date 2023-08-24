using Windows.Foundation.Collections;

namespace Deppenlabor.UserSettings.Services.Interfaces;

public interface IApplicationDataContainer
{
    public IPropertySet Values { get; }
}