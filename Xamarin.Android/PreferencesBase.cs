using System;
using System.Collections.Generic;

using Android.Content;

namespace Xamarin.Android
{
    #region Value
    /// <summary>
    /// Preferences Value Base Class
    /// </summary>
    /// <typeparam name="T">Value Data Type</typeparam>
    abstract public class PreferencesValueBase<T>
    {
        /// <summary>
        /// Preferences instance
        /// </summary>
        protected PreferencesBase preferences;

        /// <summary>
        /// 定義Key
        /// </summary>
        abstract protected string key { get; }
        /// <summary>
        /// 定義預設Value
        /// </summary>
        abstract protected T defValue { get; }

        /// <summary>
        /// Get Preference Value
        /// </summary>
        /// <param name="_key">key</param>
        /// <param name="_defValue">預設value</param>
        /// <returns>value</returns>
        abstract protected T GetValueFunc(string _key, T _defValue);
        /// <summary>
        /// 寫入 Preference Value
        /// </summary>
        /// <param name="_key">key</param>
        /// <param name="_value">value</param>
        /// <returns>ISharedPreferencesEditor</returns>
        abstract protected ISharedPreferencesEditor PutValueFunc(string _key, T _value);

        /// <summary>
        /// Get Preference Value
        /// </summary>
        /// <param name="_key">key</param>
        /// <param name="_defValue">預設value</param>
        /// <returns>value</returns>
        private T GetValue(string _key, T _defValue)
        {
            return GetValueFunc(_key, _defValue);
        }
        /// <summary>
        /// 寫入 Preference Value
        /// </summary>
        /// <param name="_key">key</param>
        /// <param name="_value">value</param>
        private void PutValue(string _key, T _value)
        {
            if (!PutValueFunc(_key, _value).Commit())
            {
                throw new Exception("Preferences Commit Error! Preferences:" + preferences.name + " Key:" + _key + " Value:" + _value);
            }
        }
        /// <summary>
        /// Value存取介面
        /// </summary>
        public T Value
        {
            get
            {
                return GetValue(key, defValue);
            }
            set
            {
                PutValue(key, value);
            }
        }

        /// <summary>
        /// 建構式 傳入PreferencesBase物件
        /// </summary>
        /// <param name="_preferencesBase"></param>
        public PreferencesValueBase(PreferencesBase _preferencesBase)
        {
            preferences = _preferencesBase;
        }
    }
    //以下略......
    abstract public class PreferencesBoolean : PreferencesValueBase<bool>
    {
        public PreferencesBoolean(PreferencesBase _preferencesBase) : base(_preferencesBase) { }

        sealed protected override bool GetValueFunc(string _key, bool _defValue)
        {
            return preferences.GetBoolean(_key, _defValue);
        }

        sealed protected override ISharedPreferencesEditor PutValueFunc(string _key, bool _value)
        {
            return preferences.PutBoolean(_key, _value);
        }
    }
    abstract public class PreferencesFloat : PreferencesValueBase<float>
    {
        public PreferencesFloat(PreferencesBase _preferencesBase) : base(_preferencesBase) { }

        sealed protected override float GetValueFunc(string _key, float _defValue)
        {
            return preferences.GetFloat(_key, _defValue);
        }

        sealed protected override ISharedPreferencesEditor PutValueFunc(string _key, float _value)
        {
            return preferences.PutFloat(_key, _value);
        }
    }
    abstract public class PreferencesInt : PreferencesValueBase<int>
    {
        public PreferencesInt(PreferencesBase _preferencesBase) : base(_preferencesBase) { }

        sealed protected override int GetValueFunc(string _key, int _defValue)
        {
            return preferences.GetInt(_key, _defValue);
        }

        sealed protected override ISharedPreferencesEditor PutValueFunc(string _key, int _value)
        {
            return preferences.PutInt(_key, _value);
        }
    }
    abstract public class PreferencesLong : PreferencesValueBase<long>
    {
        public PreferencesLong(PreferencesBase _preferencesBase) : base(_preferencesBase) { }

        sealed protected override long GetValueFunc(string _key, long _defValue)
        {
            return preferences.GetLong(_key, _defValue);
        }

        sealed protected override ISharedPreferencesEditor PutValueFunc(string _key, long _value)
        {
            return preferences.PutLong(_key, _value);
        }
    }
    abstract public class PreferencesString : PreferencesValueBase<string>
    {
        public PreferencesString(PreferencesBase _preferencesBase) : base(_preferencesBase) { }

        sealed protected override string GetValueFunc(string _key, string _defValue)
        {
            return preferences.GetString(_key, _defValue);
        }

        sealed protected override ISharedPreferencesEditor PutValueFunc(string _key, string _value)
        {
            return preferences.PutString(_key, _value);
        }
    }
    abstract public class PreferencesStringSet : PreferencesValueBase<ICollection<string>>
    {
        public PreferencesStringSet(PreferencesBase _preferencesBase) : base(_preferencesBase) { }

        protected override ICollection<string> GetValueFunc(string _key, ICollection<string> _defValue)
        {
            return preferences.GetStringSet(_key, _defValue);
        }

        protected override ISharedPreferencesEditor PutValueFunc(string _key, ICollection<string> _value)
        {
            return preferences.PutStringSet(_key, _value);
        }
    }
    #endregion

    abstract public class PreferencesBase : IDisposable
    {
        /// <summary>
        /// 建構式 傳入Context
        /// </summary>
        /// <param name="_context"></param>
        public PreferencesBase(Context _context) { context = _context; }
        /// <summary>
        /// 解構式 執行Dispose()
        /// </summary>
        ~PreferencesBase() { Dispose(); }

        /// <summary>
        /// 定義Preference的名稱
        /// </summary>
        abstract public string name { get; }
        /// <summary>
        /// 定義FileCreationMode
        /// </summary>
        abstract protected FileCreationMode fileCreationMode { get; }
        /// <summary>
        /// 使用此Context取得ISharedPreferences
        /// </summary>
        Context context;

        /// <summary>
        /// 定義ISharedPreferences
        /// </summary>
        ISharedPreferences sharedPreferences { get { return context.GetSharedPreferences(name, fileCreationMode); } }
        /// <summary>
        /// 定義ISharedPreferencesEditor
        /// </summary>
        ISharedPreferencesEditor sharedPreferencesEditor { get { return sharedPreferences.Edit(); } }

        #region for IDisposable
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            sharedPreferences.Dispose();
        }
        #endregion

        #region SharedPreferencesEditor
        /// <summary>
        /// 寫入數據-同步處理
        /// </summary>
        /// <returns>返回是否寫入成功</returns>
        public bool Commit()
        {
            return sharedPreferencesEditor.Commit();
        }

        /// <summary>
        /// 寫入數據-異步處理
        /// </summary>
        public void Apply()
        {
            sharedPreferencesEditor.Apply();
        }

        /// <summary>
        /// 清除所有Value
        /// </summary>
        /// <returns>回傳執行處理後的ISharedPreferencesEditor物件</returns>
        public ISharedPreferencesEditor Clear()
        {
            return sharedPreferencesEditor.Clear();
        }
        /// <summary>
        /// 移除指定key的Value
        /// </summary>
        /// <param name="key">指定key</param>
        /// <returns>回傳執行處理後的ISharedPreferencesEditor物件</returns>
        public ISharedPreferencesEditor Remove(string key)
        {
            return sharedPreferencesEditor.Remove(key);
        }

        #region 寫入Value(各種Data Type)

        public ISharedPreferencesEditor PutString(string key, string value)
        {
            return sharedPreferencesEditor.PutString(key, value);
        }

        public ISharedPreferencesEditor PutBoolean(string key, bool value)
        {
            return sharedPreferencesEditor.PutBoolean(key, value);
        }

        public ISharedPreferencesEditor PutFloat(string key, float value)
        {
            return sharedPreferencesEditor.PutFloat(key, value);
        }

        public ISharedPreferencesEditor PutInt(string key, int value)
        {
            return sharedPreferencesEditor.PutInt(key, value);
        }

        public ISharedPreferencesEditor PutLong(string key, long value)
        {
            return sharedPreferencesEditor.PutLong(key, value);
        }

        public ISharedPreferencesEditor PutStringSet(string key, ICollection<string> values)
        {
            return sharedPreferencesEditor.PutStringSet(key, values);
        }

        #endregion
        #endregion

        #region ISharedPreferences
        /// <summary>
        /// 檢查是否包含指定key的值
        /// </summary>
        /// <param name="key">指定key</param>
        /// <returns>bool</returns>
        public bool Contains(string key) { return sharedPreferences.Contains(key); }

        /// <summary>
        /// get ISharedPreferencesEditor from ISharedPreferences
        /// </summary>
        /// <returns>ISharedPreferencesEditor</returns>
        public ISharedPreferencesEditor Edit() { return sharedPreferences.Edit(); }

        #region 讀取Value(各種Data Type)

        public bool GetBoolean(string key, bool defValue)
        {
            return sharedPreferences.GetBoolean(key, defValue);
        }

        public string GetString(string key, string defValue)
        {
            return sharedPreferences.GetString(key, defValue);
        }

        public float GetFloat(string key, float defValue)
        {
            return sharedPreferences.GetFloat(key, defValue);
        }

        public int GetInt(string key, int defValue)
        {
            return sharedPreferences.GetInt(key, defValue);
        }

        public long GetLong(string key, long defValue)
        {
            return sharedPreferences.GetLong(key, defValue);
        }

        public ICollection<string> GetStringSet(string key, ICollection<string> defValues)
        {
            return sharedPreferences.GetStringSet(key, defValues);
        }

        public void RegisterOnSharedPreferenceChangeListener(ISharedPreferencesOnSharedPreferenceChangeListener listener)
        {
            throw new NotImplementedException();
        }

        public void UnregisterOnSharedPreferenceChangeListener(ISharedPreferencesOnSharedPreferenceChangeListener listener)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// get sharedPreferences.All
        /// </summary>
        public IDictionary<string, object> All { get { return sharedPreferences.All; } }

        /// <summary>
        /// get sharedPreferences.Handle
        /// </summary>
        public IntPtr Handle { get { return sharedPreferences.Handle; } }

        #endregion
    }

    //EXAMPLE
    //class EXAMPLE : PreferencesBase
    //{
    //    public override string name { get { return "EXAMPLE"; } }
    //    protected override FileCreationMode fileCreationMode { get { return FileCreationMode.Private; } }
    //    public EXAMPLE(Context _context) : base(_context) { }

    //    #region Value
    //    class PreferencesString_Value : PreferencesString
    //    {
    //        public PreferencesString_Value(PreferencesBase _preferencesBase) : base(_preferencesBase) { }
    //        protected override string key => "Value";
    //        protected override string defValue => string.Empty;
    //    }
    //    PreferencesString_Value String_Value => new PreferencesString_Value(this);
    //    public string Value
    //    {
    //        get { return String_Value.Value; }
    //        set { String_Value.Value = value; }
    //    }
    //    #endregion
    //}

    abstract class Preferences_String: PreferencesBase
    {
        protected sealed override FileCreationMode fileCreationMode { get { return FileCreationMode.Private; } }
        public Preferences_String(Context _context) : base(_context) { }

        abstract protected string key { get; }
        abstract protected string defValue { get; }

        #region Value
        class PreferencesString_Value : PreferencesString
        {
            public PreferencesString_Value(PreferencesBase _preferencesBase) : base(_preferencesBase) { }
            protected override string key => key;
            protected override string defValue => defValue;
        }
        PreferencesString_Value String_Value => new PreferencesString_Value(this);
        public string Value
        {
            get { return String_Value.Value; }
            set { String_Value.Value = value; }
        }
        #endregion
    }

    //class Preferences_String_Example : Preferences_String
    //{
    //    public Preferences_String_Example(Context _context) : base(_context)
    //    {
    //    }

    //    public override string name => "Example";

    //    protected override string key => "key";

    //    protected override string defValue => "defValue";
    //}

}