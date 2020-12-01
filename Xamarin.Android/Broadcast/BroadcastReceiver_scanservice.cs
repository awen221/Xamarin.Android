using Android.Content;

namespace Xamarin.Android.Broadcast
{
    /// <summary>
    /// BroadcastReceiver模式控制scanservice
    /// </summary>
    public class BroadcastReceiver_scanservice
    {
        [BroadcastReceiver(Enabled = true)]
        class scanservice_Base : BroadcastReceiver
        {
            /// <summary>
            /// 註冊scanservice用的名稱
            /// df.scanservice.result
            /// </summary>
            const string scanservice_result = "df.scanservice.result";
            /// <summary>
            /// 在ContextWrapper建立Broadcast的scanservice所用的名稱
            /// df.scanservice.toapp
            /// </summary>
            const string scanservice_toapp = "df.scanservice.toapp";

            /// <summary>
            /// 用於註冊BroadcastReceiver的ContextWrapper
            /// </summary>
            ContextWrapper contextWrapper;

            /// <summary>
            /// 介面-紀錄Registered
            /// </summary>
            public bool IsRegistered
            {
                private set;
                get;
            }

            /// <summary>
            /// 在ContextWrapper OnCreate時呼叫
            /// 指定ContextWrapper建立Broadcast
            /// </summary>
            /// <param name="context">指定ContextWrapper</param>
            public void OnCreate(ContextWrapper context, OnReceiveFunc _onReceiveFunc)
            {
                contextWrapper = context;
                Intent sendIntent = new Intent(scanservice_toapp);
                contextWrapper.SendBroadcast(sendIntent);

                onReceiveFunc = _onReceiveFunc;
            }

            /// <summary>
            /// 在ContextWrapper Register時呼叫
            /// 同步RegisterReceiver
            /// </summary>
            public void OnResume()
            {
                if (IsRegistered) return;
                contextWrapper.RegisterReceiver(this, new IntentFilter(scanservice_result));
                IsRegistered = true;
            }

            /// <summary>
            /// 在ContextWrapper Unregister時呼叫
            /// 同步UnregisterReceiver
            /// </summary>
            public void OnPause()
            {
                if (!IsRegistered) return;
                contextWrapper.UnregisterReceiver(this);
                IsRegistered = false;
            }


            public OnReceiveFunc onReceiveFunc;
            /// <summary>
            /// 覆寫OnReceive並呼叫自訂之OnScanCompleted函式
            /// </summary>
            /// <param name="context">BroadcastReceiver註冊的Context</param>
            /// <param name="intent">傳遞參數的物件</param>
            public override void OnReceive(Context context, Intent intent)
            {
                if (intent.Action == scanservice_result)
                {
                    string ScanContent;
                    ScanContent = intent.GetStringExtra("result");
                    ScanContent = ScanContent.TrimEnd(new char[] { '+' });

                    onReceiveFunc?.Invoke(ScanContent);
                }
            }
        }
        scanservice_Base scanservice = new scanservice_Base();

        /// <summary>
        /// 委派接收函式
        /// </summary>
        /// <param name="result"></param>
        public delegate void OnReceiveFunc(string result);

        /// <summary>
        /// 介面-紀錄Registered
        /// </summary>
        public bool IsRegistered => scanservice.IsRegistered;

        /// <summary>
        /// 在ContextWrapper OnCreate時呼叫
        /// 指定ContextWrapper建立Broadcast
        /// </summary>
        /// <param name="context">指定ContextWrapper</param>
        public void OnCreate(ContextWrapper context, OnReceiveFunc _onReceiveFunc)
        {
            scanservice.OnCreate(context, _onReceiveFunc);
        }

        /// <summary>
        /// 在ContextWrapper Register時呼叫
        /// 同步RegisterReceiver
        /// </summary>
        public void OnResume()
        {
            scanservice.OnResume();
        }

        /// <summary>
        /// 在ContextWrapper Unregister時呼叫
        /// 同步UnregisterReceiver
        /// </summary>
        public void OnPause()
        {
            scanservice.OnPause();
        }
    }
    
    ////EXAMPLE
    //class ActivityWithBroadcastReceiver : Activity
    //{
    //    EX_BroadcastReceiver Broadcast = new EX_BroadcastReceiver();
    //    protected override void OnCreate(Bundle savedInstanceState)
    //    {
    //        base.OnCreate(savedInstanceState);
    //        Broadcast.OnCreate(this);
    //    }
    //    protected override void OnResume()
    //    {
    //        base.OnResume();
    //        Broadcast.OnResume();
    //    }
    //    protected override void OnPause()
    //    {
    //        base.OnPause();
    //        Broadcast.OnPause();
    //    }
    //}
}