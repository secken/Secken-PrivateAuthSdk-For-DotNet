# Secken Private Manage Cloud Server SDK For DotNet

## 简介（Description）
Secken.YangCong.PrivateSdk是Secken官方提供了一套用于和洋葱内网验证服务交互的SDK组件，通过使用它，您可以简化集成Secken服务的流程并降低开发成本。

密码就要大声说出来，开启无密时代，让密码下岗
洋葱是一个基于云和用户生物特征的身份验证服务。网站通过集成洋葱，可以快速实现二维码登录，或在支付、授权等关键业务环节使用指纹、声纹或人脸识别功能，从而彻底抛弃传统的账号密码体系。对个人用户而言，访问集成洋葱服务的网站将无需注册和记住账号密码，直接使用生物特征验证提高了交易安全性，无需担心账号被盗。洋葱还兼容Google验证体系，支持国内外多家网站的登录令牌统一管理。

【联系我们】<br/>
官网：https://www.yangcong.com<br/>
微信：yangcongAPP<br/>
微信群：http://t.cn/RLGDwMJ<br/>
QQ群：154697540<br/>
微博：http://weibo.com/secken<br/>
帮助：https://www.yangcong.com/help<br/>
合作：010-64772882 / market@secken.com<br/>
支持：support@secken.com<br/>
帮助文档：https://www.yangcong.com/help<br/>
项目地址：https://github.com/secken/Secken-PrivateAuthSdk-For-DotNet<br/>
Nuget地址：https://www.nuget.org/packages/Secken.YangCong.PrivateSdk<br/>
<br/>
洋葱SDK产品服务端SDK主要包含四个方法：
* 获取二维码的方法（GetYangAuthQrCode），用于获取二维码内容和实现绑定。
* 请求推送验证的方法（AskYangAuthPush），用于发起对用户的推送验证操作。
* 查询事件结果的方法（CheckYangAuthResult），用于查询二维码登录或者推送验证的结果。

## 安装使用（Install & Get Started）

To install Secken.YangCong.PrivateSdk, Import these packages

```
PM> Install-Package Secken.YangCong.PrivateSdk
```
## 更新发布（Update & Release Notes）

```
【1.1.0】更新内容：
1、完成了.Net4.5版接口封装。
2、完成了Wp8.0版接口封装。
```

## 要求和配置（Require & Config）
```
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
```

## 获取二维码内容并发起验证事件（Get YangAuth QrCode）
```
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
	}
}
```

GetYangAuthQrCode接口包含一个必传参数，ThisRequestPrivateSdkKey; 

|    状态码   | 		状态详情 		  |
|:----------:|:-----------------:|
|  200       |       成功         |
|  400       |       上传参数错误  |
|  403       |       签名错误                |
|  404       |       应用不存在                |
|  407       |       请求超时                |
|  500       |       系统错误                |
|  609       |       ip地址被禁                |

## 查询验证事件的结果（Check YangAuth Result）
```
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
```
CheckYangAuthResult接口包含一个必传参数，EventId。

|    状态码   | 		状态详情 		  |
|:----------:|:-----------------:|
|  200       |       成功         |
|  201       |       事件已被处理                |
|  400       |       上传参数错误  |
|  403       |       签名错误                |
|  404       |       应用不存在                |
|  407       |       请求超时                |
|  500       |       系统错误                |
|  601       |       用户拒绝                |
|  602       |       用户还未操作                |
|  604       |       事件不存在                |
|  606       |       callback已被设置                |
|  609       |       ip地址被禁                |

## 发起推送验证事件（Ask YangAuth Push）
```
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
	}
}
```
AskYangAuthPush接口包含两个必传参数：UserId。

|    状态码   | 		状态详情 		  |
|:----------:|:-----------------:|
|  200       |       成功         |
|  400       |       上传参数错误  |
|  403       |       签名错误                |
|  404       |       应用不存在                |
|  407       |       请求超时                |
|  500       |       系统错误                |
|  608       |       验证token不存在           |
|  609       |       ip地址被禁                |