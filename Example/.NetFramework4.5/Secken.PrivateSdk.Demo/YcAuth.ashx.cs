using System;
using System.Threading.Tasks;
using System.Web;
using Secken.PrivateSdk.Framework;
using Secken.PrivateSdk.Models;
using Secken.PrivateSdk.Models.Request;
using Secken.PrivateSdk.Models.Response;

namespace Secken.PrivateSdk.Demo
{
    /// <summary>
    /// YcAuth 的摘要说明
    /// </summary>
    public class YcAuth : HttpTaskAsyncHandler
    {
        // 需要去洋葱私有云管理平台新建一个权限，创建完成之后，将对应的PowerId+PowerKey填过来，以及补充您私有云管理平台的访问地址
        private RequestForPrivateSdkKey _thisRequestPrivateSdkKey = new RequestForPrivateSdkKey
        {
            PowerId = "",
            PowerKey = "",
            PowerHost = "http://your.domain.com"
        };
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
        /// 这个根据自己业务来，Demo中用它来做登录的Cookie Token
        /// </summary>
        private static string _nowToken = "";

        private static string _nowLoginCookieKey = "Login";

        public async override Task ProcessRequestAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string resposeStr = "";
            var action = context.Request[ParaForPrivateSdk.ActionKeyName];
            switch (action)
            {
                case ParaForPrivateSdk.ActionForGetYcAuthQrCode:
                    {
                        #region ActionForGetYcAuthQrCode

                        // 准备请求参数类
                        var thisRequestPrivateSdkQrCode = new RequestForPrivateSdkQrCode(ThisRequestPrivateSdkKey);
                        // 获取二维码内容的方法
                        var thisResponsePrivateSdkQrCode = await PrivateSdkProvider.Current.Action<ResponseForPrivateSdkQrCode>(PrivateSdkProviderType.GetYangAuthQrCode, thisRequestPrivateSdkQrCode);
                        // 获取二维码内容的结果
                        if (thisResponsePrivateSdkQrCode != null)
                        {
                            if (thisResponsePrivateSdkQrCode.IsLegal)
                            {
                                // 根据需要返回
                                resposeStr = thisResponsePrivateSdkQrCode.Serialize();
                            }
                        }

                        #endregion
                    }
                    break;

                case ParaForPrivateSdk.ActionForCheckYcAuthResult:
                    {
                        #region ActionForCheckYcAuthResult

                        var thisRequestEventId = context.Request[ParaForPrivateSdk.EventIdKeyName];
                        if (!string.IsNullOrEmpty(thisRequestEventId))
                        {
                            // 准备请求参数类
                            var thisRequestPrivateSdkResult = new RequestForPrivateSdkResult(ThisRequestPrivateSdkKey)
                            {
                                EventId = thisRequestEventId
                            };
                            // 调用查询事件结果的方法
                            var thisResponsePrivateSdkResult = await PrivateSdkProvider.Current.Action<ResponseForPrivateSdkResult>(PrivateSdkProviderType.CheckYangAuthResult, thisRequestPrivateSdkResult);
                            // 调用查询事件结果的结果
                            if (thisResponsePrivateSdkResult != null)
                            {
                                if (thisResponsePrivateSdkResult.IsLegal)
                                {
                                    // 如果这个UserId和库里面绑定的UserId一致，那就表示可以让他通过
                                    // 如果这个UserId在库里面查询不到，就可以理解为这是绑定流程。
                                    if (Equals(thisResponsePrivateSdkResult.UserId, ""))
                                    {
                                        // 根据需要返回
                                        _nowToken = Guid.NewGuid().ToString();
                                        context.Response.SetCookie(new HttpCookie(_nowLoginCookieKey, _nowToken));
                                        resposeStr = thisResponsePrivateSdkResult.Serialize();
                                    }
                                    else
                                    {
                                        thisResponsePrivateSdkResult.Code = ParaForPrivateSdk.CodeForIllegalForPermission;
                                        thisResponsePrivateSdkResult.UserId = null;
                                        resposeStr = thisResponsePrivateSdkResult.Serialize();
                                    }
                                }
                                else
                                {
                                    thisResponsePrivateSdkResult.Code = ParaForPrivateSdk.CodeForIllegalForReturn;
                                    resposeStr = thisResponsePrivateSdkResult.Serialize();
                                }
                            }
                        }

                        #endregion
                    }
                    break;
                case ParaForPrivateSdk.ActionForAskYangAuthPush:
                    {
                        #region ActionForAskYangAuthPush

                        var thisRequestUserId = context.Request[ParaForPrivateSdk.UserIdKeyName];
                        if (!string.IsNullOrEmpty(thisRequestUserId))
                        {
                            // 准备请求参数类
                            var thisRequestPrivateSdkPush = new RequestForPrivateSdkPush(ThisRequestPrivateSdkKey)
                            {
                                UserId = thisRequestUserId
                            };
                            // 发起推送验证的方法
                            var thisResponsePrivateSdkPush = await PrivateSdkProvider.Current.Action<ResponseForPrivateSdkPush>(PrivateSdkProviderType.AskYangAuthPush, thisRequestPrivateSdkPush);
                            // 发起推送验证的结果
                            if (thisResponsePrivateSdkPush != null)
                            {
                                if (thisResponsePrivateSdkPush.IsLegal)
                                {
                                    // 根据需要返回
                                    resposeStr = thisResponsePrivateSdkPush.Serialize();
                                }
                            }
                        }

                        #endregion
                    }
                    break;

            }

            context.Response.Write(resposeStr);
        }
    }
}