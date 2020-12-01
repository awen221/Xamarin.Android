namespace Xamarin.Android
{
    abstract public class ValuesStrings_Base
    {
        static public string GetString(int resID) => global::Android.App.Application.Context.Resources.GetString(resID);
    }
}
