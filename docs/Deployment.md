## デプロイ

### Azure Web App にデプロイする手順
GitHub にサインインして、このリポジトリを fork します。  
(Azure Web App と連携させるには、自身で所有しているリポジトリでなければならないためです。)

![](images/Deployment-1.jpg)

次に、Azure で Web App を作成します。

![](images/Deployment-2.jpg)

作成が完了したら、[デプロイ オプション] を構成します。  
ソースとして GitHub を選択すると、アカウント承認の画面が現れます。さらにリポジトリとブランチを選択します。

![](images/Deployment-3.jpg)

必要な設定はこれだけです。設定完了と同時に、ビルドおよびデプロイが開始されます。

![](images/Deployment-4.png)

これ以降も、fork したリポジトリを更新すれば、自動的にビルドおよびデプロイが実行されます。

### 参照
- [Azure と GitHub で継続的デプロイ (2017)](https://sakapon.wordpress.com/2017/12/30/azure-github-2017/)
