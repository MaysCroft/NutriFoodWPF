# NutriFoodWPF

NutriFoodWPF é uma aplicação desktop desenvolvida em C# utilizando o framework WPF (.NET 8) para o gerenciamento e consulta de informações nutricionais de alimentos. O projeto segue o padrão de arquitetura **MVVM (Model-View-ViewModel)** para garantir a separação de responsabilidades e facilitar a manutenção.

## 🚀 Funcionalidades

* **Busca de Alimentos**: Interface intuitiva para pesquisar alimentos por nome.
* **Exibição Detalhada**: Tabela (DataGrid) que apresenta informações completas como calorias, porção, gorduras (totais e saturadas), proteínas, sódio, potássio, colesterol, carboidratos, fibras e açúcares.
* **Persistência em Nuvem**: Integração com o Google Cloud Firestore para armazenamento e recuperação de dados.
* **Validação via API**: Utilização de um serviço externo para validar e buscar dados nutricionais precisos antes do salvamento.
* **Banco de Dados Local**: Estrutura preparada para utilização de SQLite para cache ou armazenamento local.

## 🛠️ Tecnologias Utilizadas

* **Linguagem**: C#
* **Framework UI**: WPF (Windows Presentation Foundation) no .NET 8.0
* **Padrão de Projeto**: MVVM
* **Banco de Dados**:
* Google Cloud Firestore (Nuvem)
* SQLite (Local)


* **Principais Bibliotecas (NuGet)**:
* `CommunityToolkit.Mvvm`: Para implementação simplificada do padrão MVVM.
* `Google.Cloud.Firestore`: Para integração com Firebase.
* `RestSharp`: Para consumo de APIs REST.
* `Newtonsoft.Json` e `System.Text.Json`: Para manipulação de dados JSON.



## 📂 Estrutura do Projeto

O projeto está organizado da seguinte forma:

* **Models**: Contém as classes que representam as entidades de dados, como a classe `Alimento`.
* **Views**: Define a interface de usuário em XAML.
* **ViewModels**: Responsável pela lógica de apresentação e comandos (ex: `MainViewModel`).
* **Services**: Gerencia a comunicação com APIs externas.
* **Repositories**: Camada de acesso a dados, tratando a lógica de salvamento e recuperação no Firestore.
* **Data**: Configurações de conexão com o banco de dados local SQLite.
* **Commands**: Implementações de comandos como `RelayCommand` para interações da UI.

## ⚙️ Configuração e Instalação

### Pré-requisitos

* Visual Studio 2022 ou JetBrains Rider.
* SDK do .NET 8.0.

### Chave do Firebase

Para que a integração com o Firestore funcione, é necessário:

1. Obter o arquivo de credenciais (`.json`) no console do Firebase.
2. Renomeá-lo para `firebase-key.json`.
3. Colocá-lo na pasta `NutriFoodWPF/config_API/`.
* *Nota: O arquivo está configurado no `.csproj` para ser copiado automaticamente para o diretório de saída*.



## 📝 Como usar

1. Inicie a aplicação.
2. Na aba "Procura de Alimento", digite o nome do item desejado no campo de busca.
3. Clique em "Pesquisar" para carregar os dados nutricionais salvos ou validados.
4. Os resultados serão exibidos automaticamente na grade central.
