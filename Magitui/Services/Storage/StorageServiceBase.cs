using Magitui.Extensions;

namespace Magitui.Services.Storage;

public class StorageServiceBase
{
    protected async Task<string> GetAsync(string key, string defaultValue = null)
    {
        if (DeviceInfo.DeviceType == DeviceType.Physical)
        {
            defaultValue = await SecureStorage.GetAsync(key) ?? defaultValue;
        }
        else
        {
            try
            {
                var property = Preferences.Get(key, defaultValue);
                if (property == null) return String.Empty;
                var value = property.ToString();
                if (value.IsNotNullOrWhiteSpace()) defaultValue = value;
            }
            catch (KeyNotFoundException)
            {
                // do nothing
                return string.Empty;
            }
        }

        return defaultValue;
    }

    protected async Task<bool> RemoveAsync(string key)
    {

        if (DeviceInfo.DeviceType == DeviceType.Physical)
            await Task.FromResult(SecureStorage.Remove(key));

        if (!Preferences.ContainsKey(key)) await Task.FromResult(false);
        Preferences.Remove(key);
        await Task.FromResult(true);

        return false;
    }

    protected async Task SetAsync(string key, string value)
    {

        if (key.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(key));
        if (value.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(value));
        if (DeviceInfo.DeviceType == DeviceType.Physical)
            await SecureStorage.SetAsync(key, value);
        else
            Preferences.Set(key, value);
    }
}
