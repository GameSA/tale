RTX使用域控组织架构和帐号的插件
===============================

 * 需要安装腾讯RTX Server SDK，下载地址：http://rtx.tencent.com/rtx/download/

 * 修改MainWindow.cs里的域控配置：
```
        //域名
        private string _DomainName = "127.0.0.1/DC=intranet,DC=123u,DC=com";
        private string _UsersDomainName = "127.0.0.1/OU=users,OU=123u,DC=intranet,DC=123u,DC=com";
```

 * 修改app.config的域控用户名和密码：
```
<?xml version="1.0"?>
<configuration>
<startup><supportedRuntime version="v2.0.50727"/></startup>
  <appSettings>
    <add key="AD_ACCOUNT" value="xxxxxx"/>
    <add key="AD_PASSWD" value="xxxxxx"/>
  </appSettings>
</configuration>
```