using Android.App;
using Android.Views;
using Android.Widget;

namespace Xamarin.Android
{
    abstract public class BaseAdapterArray<T> : BaseAdapter
    {
        T[] T_Array;
        protected Activity context;
        int ItemLayout;

        public BaseAdapterArray(Activity context, T[] t_Array, int itemLayout) : base()
        {
            this.context = context;
            this.T_Array = t_Array;
            ItemLayout = itemLayout;
        }

        public override Java.Lang.Object GetItem(int position) { return position; }

        public override long GetItemId(int position) { return position; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = T_Array[position];
            var view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(ItemLayout, null);
            }

            OnGetView(view, item);

            return view;
        }

        abstract public void OnGetView(View itemView, T item);

        //Fill in cound here, currently 0
        public override int Count { get { return T_Array.Length; } }
    }

    ////EXAMPLE
    //class EX_Item_Adapter : BaseAdapterArray<string>
    //{
    //    public EX_Item_Adapter(Activity context, string[] t_Array, int itemLayout) : base(context, t_Array, itemLayout) { }

    //    public override void OnGetView(View itemView, string item)
    //    {
    //        //TODO...
    //        //itemView.FindViewById<TextView>(Resource.Id.XXX).Text = item.XXX;
    //    }
    //}
}