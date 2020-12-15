# Google Map 
谷歌地图标记管理

开发环境：

- 后端：.Net Core 3.1（C#）
- 前端：bootstrap 4、jQuery
- PostgreSQL 12 数据库（不支持MySQL）

数据连接配置文件：appsettings.json、appsettings.Development.json

运行应用程序，自动对空库生成数据表，如果数据库存在数据表，不会执行初始化过程。

网站默认管理员：admin，默认密码12345678

## 实现功能

- 用户注册管理。

- 后台标记添加。

- 通过POST API批量添加。

## 系统设置

- 初次使用，必须使用admin账号登录，填写Google API的Key，该Key可以在Google Map网站申请。

## API调用接口

每个注册用户可以登录后台查看调用网站接口的API Key。

调用网址：http://域名/Api/InsertMarker，发送的是POST请求。

POST请求需要传递一个JSON对象：

```json
{
    "Key":"网站开发者密钥",
    "Lat": 纬度（浮点数）,
    "Lng": 经度（浮点数）,
    "address":"地址",
    "explain":"描述",
    "title":"标题"
}
```

调用该接口会返回一个JSON对象：

```json
{
    "Success":  成功为true 失败为false,
    "Message": "失败的错误信息"
}
```

