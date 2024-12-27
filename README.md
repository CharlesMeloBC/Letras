# API Letras
*Resumo: Esssa aplicação faz gerenciamento de musicas como cadastro, deleção e consulta*💿🎛️🎶

### Como usar 📲

**1º Passo** 
clone o codigo via git:  
```
https://github.com/CharlesMeloBC/Letras.git
```


**2º Passo**
Baixe as dependencias e pacotes da aplicação necessarios para conexão com seu banco de dados:
eu usei o SQL Server 

```
https://www.microsoft.com/pt-br/sql-server/sql-server-downloads
```
No Visual Studio baixe os seguintes pacotes do entity para fazer a comunicação com o SQL Server
```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```
**3º Passo**
configure sua Connection string exemplo:
```
  "ConnectionStrings": {
    "DefaultConetion": "Server=DESKTOP[host do seu server];Database=LetrasDeMusicas;Trusted_Connection=True;TrustServerCertificate=True"
  },
```

**4º Passo**
faça a ``migration`` para poder criar as tabelas no SQL Server 
```
dotnet ef migrations add [O NOME DE SUA TABELA]
dotnet ef database update
```
---
### POST 📤

**1º Passo**
Para alimentar o banco de dados com o cadastro de suas musicas você usará o metodo POST com seguinte endpoint
```
https://[seu host:0000]/musicas
```
configure o BODY em formato .json
```json
{
    "Name": "Modelo exemplo",
    "Letra": "Modelo exemplo, exemplo"
}

```
---
### GET 🫴

**1º Passo**
Para buscar os objestos cadastrados "Musicas", você pode estar usando o endpoint da seguinte forma:
```
https://[seu host:0000]/musicas/[Nome da canção]
```
```
https://[seu host:0000]/musicas/[ID]
```
```
https://[seu host:0000]/musicas/[Palavras ou frases contidas na canção]
```
---
### DELETE 🗑️
**1º Passo**
Para fazer a deleção de qualquer objeto cadastrado no banco basta informa o ID no endpoint:
```
https://[seu host:0000]/musicas/[ID]
```
