using Android.App;
using Android.Content;

namespace Xamarin.Android.Dialog.ProgressDialog
{
    using ProgressDialog = global::Android.App.ProgressDialog;
    /// <summary>
    /// 用ProgressDialog建立的Processing類別(ProgressDialog已過期，不建議使用)
    /// </summary>
    //public sealed class Processing
    //{
    //    const string defualt_message = "Processing...";
    //    ProgressDialog progressDialog{ set; get; }

    //    public Processing(Context context, string message = "", string title = "")
    //    {
    //        Init(context, message, title);
    //    }

    //    void Init(Context context, string message, string title)
    //    {
    //        progressDialog = new ProgressDialog(context);

    //        if (message == string.Empty)
    //        {
    //            message = defualt_message;
    //        }

    //        progressDialog.Indeterminate = true;
    //        progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
    //        progressDialog.SetMessage(message);
    //        progressDialog.SetTitle(title);

    //        progressDialog.SetCancelable(false);
    //        progressDialog.Hide();
    //    }

    //    public void Show()
    //    {
    //        progressDialog.Show();
    //    }
    //    public void Hide()
    //    {
    //        progressDialog.Hide();
    //    }

    //    public bool IsShowing => progressDialog.IsShowing;
    //}
}