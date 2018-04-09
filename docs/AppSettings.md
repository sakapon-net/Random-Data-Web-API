## アプリケーション設定

### Web.config
Web.config には、 次のようなアプリケーション設定があります。

```xml
<configuration>
  <appSettings>
    <add key="app:RequireHttps" value="true" />
    <add key="app:PermanentHttps" value="false" />
  </appSettings>
</configuration>
```

それぞれの説明は次の通りです。
- `app:RequireHttps`
  - HTTPS を必須とする場合は `true` を指定します。
  - HTTP でアクセスすると、HTTPS の URL にリダイレクトされます。
- `app:PermanentHttps`
  - リダイレクト時に HTTPS を永続化する場合は `true` を指定します。
  - すなわち、HTTP ステータス コードは `true` のとき 301、`false` のとき 302 です。
