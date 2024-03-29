﻿using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace OpenLibrary.Common.Helpers
{
    public static class Settings
    {
        private const string _user = "user";
        private const string _token = "token";
        private const string _isLogin = "isLogin";
        private const string _docDetail = "docDetail";
        private const string _reviewS = "reviewS";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;
        public static string ReviewS
        {
            get => AppSettings.GetValueOrDefault(_reviewS, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_reviewS, value);
        }

        public static string DocDetail
        {
            get => AppSettings.GetValueOrDefault(_docDetail, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_docDetail, value);
        }

        public static string User
        {
            get => AppSettings.GetValueOrDefault(_user, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_user, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }
    }
}
