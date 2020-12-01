using Android.App;

namespace Xamarin.Android.Dialog.AlertDialog
{
    using AlertDialog = global::Android.App.AlertDialog;

    public class Func
    {
        static public AlertDialog Prompt(Activity activity, string title, string Message, string ButtonName)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(activity);
            alert.SetTitle(title);
            alert.SetMessage(Message);

            alert.SetPositiveButton(ButtonName,
                    (senderAlert, args) =>
                    {
                        //Toast.MakeText(activity, ButtonName, ToastLength.Short).Show();
                    }
                );

            AlertDialog dialog = alert.Create();
            if (!activity.IsFinishing)
            {
                dialog.Show();
            }

            return dialog;
        }
        //EXAMPLE
        //AlertDialog alertDialog = Func.Prompt(this,"title","message","ok");
        //alertDialog.DismissEvent += delegate { //todo......};
    }
}