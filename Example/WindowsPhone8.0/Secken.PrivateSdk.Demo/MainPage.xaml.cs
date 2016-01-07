using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Led;
using Led.Base.VmBehavior.NotifyBase;
using Led.Plugin.FileIo;
using Secken.PrivateSdk.Framework;
using Secken.PrivateSdk.Models.Request;
using Secken.PrivateSdk.Models.Response;

namespace Secken.PrivateSdk.Demo
{
    public partial class MainPage : NotifyPropertyChangedPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            PrivateSdkProvider.IsOpenAppInsight = true;
        }

        private string _thisConfigFile = "SdkPrivateSdkKey.json";
        /// <summary>
        /// ThisConfigFile
        /// </summary>
        public string ThisConfigFile
        {
            #region  ThisConfigFile
            get
            {
                return _thisConfigFile;
            }
            set
            {
                if (Equals(_thisConfigFile, value)) return;
                _thisConfigFile = value;
                NotifyPropertyChanged();
            }
            #endregion
        }

        private RequestForPrivateSdkKey _thisRequestPrivateSdkKey;
        /// <summary>
        /// ThisRequestPrivateSdkKey
        /// </summary>
        public RequestForPrivateSdkKey ThisRequestPrivateSdkKey
        {
            #region ThisRequestPrivateSdkKey
            get
            {
                return _thisRequestPrivateSdkKey;
            }
            set
            {
                if (Equals(_thisRequestPrivateSdkKey, value)) return;
                _thisRequestPrivateSdkKey = value;
            }
            #endregion
        }

        /// <summary>
        /// 洋葱 - 加载页 - 导航进入
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var thisConfigKeyStr = string.Format("/Configs/{0}", ThisConfigFile).ToResourceStr(MethodBase.GetCurrentMethod().DeclaringType);
            if (thisConfigKeyStr.IsNotNullOrEmpty())
            {
                ThisRequestPrivateSdkKey = thisConfigKeyStr.Deserialize<RequestForPrivateSdkKey>();
            }

            if (ThisRequestPrivateSdkKey == null || !ThisRequestPrivateSdkKey.IsLegal)
            {
                return;
            }

            await Task.Factory.StartNew(async () =>
            {
                var thisRequestPrivateSdkQrCode = new RequestForPrivateSdkQrCode(ThisRequestPrivateSdkKey);
                var thisResponsePrivateSdkQrCode = await PrivateSdkProvider.Current.Action<ResponseForPrivateSdkQrCode>(PrivateSdkProviderType.GetYangAuthQrCode, thisRequestPrivateSdkQrCode);
                if (thisResponsePrivateSdkQrCode != null)
                {
                    if (thisResponsePrivateSdkQrCode.IsLegal)
                    {
                        var thisRequestPrivateSdkResult = new RequestForPrivateSdkResult(ThisRequestPrivateSdkKey)
                        {
                            EventId = thisResponsePrivateSdkQrCode.EventId
                        };
                        var thisResponsePrivateSdkResult = await PrivateSdkProvider.Current.Action<ResponseForPrivateSdkResult>(PrivateSdkProviderType.CheckYangAuthResult, thisRequestPrivateSdkResult);
                        if (thisResponsePrivateSdkResult != null)
                        {
                            if (thisResponsePrivateSdkResult.IsLegal)
                            {

                            }
                        }
                    }
                }

                var thisRequestPrivateSdkPush = new RequestForPrivateSdkPush(ThisRequestPrivateSdkKey)
                {
                    UserId = "taylorshi"
                };
                var thisResponsePrivateSdkPush = await PrivateSdkProvider.Current.Action<ResponseForPrivateSdkPush>(PrivateSdkProviderType.AskYangAuthPush, thisRequestPrivateSdkPush);
                if (thisResponsePrivateSdkPush != null)
                {
                    if (thisResponsePrivateSdkPush.IsLegal)
                    {
                        var thisRequestPrivateSdkResult = new RequestForPrivateSdkResult(ThisRequestPrivateSdkKey)
                        {
                            EventId = thisResponsePrivateSdkPush.EventId
                        };
                        var thisResponsePrivateSdkResult = await PrivateSdkProvider.Current.Action<ResponseForPrivateSdkResult>(PrivateSdkProviderType.CheckYangAuthResult, thisRequestPrivateSdkResult);
                        if (thisResponsePrivateSdkResult != null)
                        {

                        }
                    }
                }
            });
        }
    }
}