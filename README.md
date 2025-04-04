# 📦Gestão do Aramazém 📦

## Demonstração

### Gestor de Equipamentos
![](https://i.imgur.com/Z3Scst9.gif)

### Gestor de Chamados

![](https://i.imgur.com/NOP9mpF.gif)

## Introdução

Aplicativo para gerenciamento de um armazém, permitindo o controle de equipamentos e chamados. Épossível cadastrar, editar, remover e visualizar essas informações de forma prática.

## Funcionalidades

- **Cadastro de Equipamentos:** Adicione novos equipamentos ao sistema com suas respectivas informações.
	- **Nome** 
	- **Fabricante** 
	- **Preço de Aquisição** 
	- **Data Adquirida** 
- **Edição de Equipamentos:** Altere os dados de equipamentos já cadastrados.
- **Remoção de Equipamentos:** Exclua equipamentos que não são mais necessários.
- **Visualização de Equipamentos:** Consulte a lista de equipamentos registrados.
- **Abertura de Chamados:** Crie chamados relacionados a equipamentos.
- **Edição de Chamados:** Atualize as informações de chamados existentes.
- **Encerramento/Exclusão de Chamados:** Finalize ou remova chamados do sistema.
- **Visualização de Chamados:** Veja os chamados abertos e quantos dias se passaram desde sua abertura.

## Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

```
dotnet restore
```

4. Em seguida, compile a solução utilizando o comando:
   
```
dotnet build --configuration Release
```

5. Para executar o projeto compilando em tempo real
   
```
dotnet run --project GestaoDeEquipamentos.ConsoleApp
```

6. Para executar o arquivo compilado, navegue até a pasta `./GestaoDeEquipamentos.ConsoleApp/bin/Release/net8.0/` e execute o arquivo:
   
```
GestaoDeEquipamentos.ConsoleApp.exe
```

## Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.