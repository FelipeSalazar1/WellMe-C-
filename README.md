# WellMe - Assistente de Saúde Preventiva

Aplicação console em C# para registrar e acompanhar atividades de saúde, armazenando dados em arrays internos.

## 📋 Descrição

O WellMe é uma aplicação simples e intuitiva que permite registrar e acompanhar três tipos principais de atividades de saúde:

- **Exercício**: Registro de minutos de exercício físico
- **Água**: Registro de litros de água ingeridos
- **Sono**: Registro de horas de sono

## 🚀 Requisitos

- .NET 8.0 SDK ou superior
- Sistema operacional: Windows, Linux ou macOS

## 📦 Instalação

1. Clone ou baixe este repositório
2. Certifique-se de ter o .NET 8.0 SDK instalado em seu sistema
3. Abra um terminal na pasta do projeto

## ▶️ Como Executar

### Windows
```bash
cd WellMe
dotnet run
```

### Linux/macOS
```bash
cd WellMe
dotnet run
```

### Compilar e executar o executável
```bash
cd WellMe
dotnet build
dotnet bin/Debug/net8.0/WellMe.dll
```

## 🎯 Funcionalidades

### 1. Adicionar Registro
Permite adicionar um novo registro de atividade de saúde:
- Selecione o tipo de atividade (Exercício, Água ou Sono)
- Informe a data (ou pressione Enter para usar a data atual)
- Informe o valor (minutos, litros ou horas)

**Validações:**
- Valores numéricos não podem ser negativos
- Data deve estar no formato dd/MM/yyyy
- Tipo de atividade deve ser válido

### 2. Listar Registros
Exibe todos os registros cadastrados de forma organizada:
- Agrupados por tipo de atividade
- Ordenados por data
- Mostra data e valor de cada registro

### 3. Exibir Estatísticas
Calcula e apresenta estatísticas para cada tipo de atividade:
- **Soma total**: Soma de todos os valores registrados
- **Média**: Média aritmética dos valores
- **Quantidade**: Número de registros de cada tipo

### 4. Sair
Encerra o programa.

## 📝 Estrutura do Projeto

```
WellMe/
├── Program.cs          # Código principal da aplicação
├── WellMe.csproj      # Arquivo de configuração do projeto
└── README.md          # Este arquivo
```

## 🏗️ Arquitetura

A aplicação está organizada em:

- **Classe Program**: Contém o método `Main` e todos os métodos de interface e lógica de negócio
- **Enum TipoAtividade**: Define os tipos de atividades disponíveis
- **Classe RegistroAtividade**: Representa um registro individual de atividade

### Métodos Principais

- `AdicionarRegistro()`: Gerencia a adição de novos registros
- `ListarRegistros()`: Exibe todos os registros de forma organizada
- `ExibirEstatisticas()`: Calcula e mostra estatísticas por tipo
- `SelecionarTipoAtividade()`: Interface para seleção do tipo
- `LerData()`: Validação e leitura de data
- `LerValor()`: Validação e leitura de valores numéricos

## ✅ Validações Implementadas

- ✅ Valores numéricos não podem ser negativos
- ✅ Data deve estar no formato correto (dd/MM/yyyy)
- ✅ Tipo de atividade deve ser válido (1-3)
- ✅ Opções do menu devem ser válidas (1-4)
- ✅ Tratamento de erros em todas as entradas do usuário

## 💡 Exemplo de Uso

1. Execute o programa
2. Escolha a opção **1** para adicionar um registro
3. Selecione o tipo de atividade (1 para Exercício, 2 para Água, 3 para Sono)
4. Informe a data ou pressione Enter para hoje
5. Informe o valor
6. Use a opção **2** para ver todos os registros
7. Use a opção **3** para ver estatísticas

## 📊 Exemplo de Saída

```
╔════════════════════════════════════════════╗
║     WellMe - Assistente de Saúde Preventiva ║
╚════════════════════════════════════════════╝

MENU PRINCIPAL
──────────────
1. Adicionar registro
2. Listar registros
3. Exibir estatísticas
4. Sair

Escolha uma opção (1-4): 
```

## 🛠️ Tecnologias Utilizadas

- C# (.NET 8.0)
- Console Application

## 📄 Licença

Este projeto foi desenvolvido como trabalho acadêmico.

## 👨‍💻 Desenvolvido por

Projeto desenvolvido conforme especificações do documento de requisitos.

---

**Nota**: Os dados são armazenados em memória (arrays internos) e serão perdidos quando o programa for encerrado.

