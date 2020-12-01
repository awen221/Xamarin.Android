using Android.Content;
using Android.Views;
using Android.Widget;

namespace Xamarin.Android.Dialog.AlertDialog
{
    using AlertDialog = global::Android.App.AlertDialog;
    public sealed class Processing
    {
        static AlertDialog alertDialog = null;

        static public void Show(Context _context, string _msg)
        {
            if (alertDialog != null)
            {
                alertDialog.Dispose();
                alertDialog = null;
            }

            alertDialog = new AlertDialog.Builder(_context).Create();
            alertDialog.RequestWindowFeature((int)WindowFeatures.NoTitle);

            View view = new View(_context);
            LinearLayout linearLayout = new LinearLayout(_context);
            ProgressBar progressBar = new ProgressBar(_context);
            linearLayout.AddView(progressBar);
            TextView textView = new TextView(_context);
            textView.Text = _msg;
            linearLayout.AddView(textView);

            alertDialog.SetView(linearLayout);
            linearLayout.SetGravity(GravityFlags.Center);

            //不能用倒退鍵或點窗體外部關閉
            alertDialog.SetCancelable(false);


            alertDialog.Show();
        }
        static public void Hide()
        {
            if (alertDialog == null) return;
            if (alertDialog.IsShowing == false) return;

            alertDialog.Hide();

            alertDialog.Dispose();
            alertDialog = null;
        }
    }

    abstract public class ProcessingObj
    {
        Context context { set; get; }
        abstract protected string msg { get; }
        public ProcessingObj(Context context_)
        {
            context = context_;
        }

        public void SetIsProc(bool value)
        {
            if (value)
                Processing.Show(context, msg);
            else
                Processing.Hide();
        }
    }


    //EXAMPLE：

    //public class IsProcessingObj : ProcessingObj
    //{
    //    public IsProcessingObj(Context context_) : base(context_) { }

    //    protected override string msg => "處理中";
    //}
    //bool IsProcessing { set => new IsProcessingObj(this).SetIsProc(value); }
}