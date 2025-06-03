
# TrackMethods

O **TrackMethods** é uma biblioteca .NET voltada para desenvolvedores que desejam **monitorar a execução de métodos** em suas aplicações. Ele registra automaticamente o tempo de execução de um método e permite **salvar logs em arquivos Excel (`.xlsx`) ou em formato JSON**, facilitando o rastreamento de performance e auditoria de chamadas.

## 🧠 Objetivo

Oferecer uma forma simples e customizável de:
- Medir o tempo de execução de métodos.
- Registrar chamadas de forma organizada.
- Aplicar condições visuais (formatação) quando o tempo de execução ultrapassar um limite definido.
- Exportar os registros como planilhas ou objetos JSON.

## 📦 Instalação

Instale o pacote no seu projeto:

```bash
dotnet add package TrackMethods
```

Ou, se estiver usando o Visual Studio:
- Clique com o botão direito no projeto → **Gerenciar Pacotes NuGet**
- Pesquise por `TrackMethods` e instale

## ✅ Como Usar

### 1. **Instanciar o rastreador**

```csharp
using TrackMethods.Services;

var tracker = new Execution(new TrackMethod());
```

### 2. **Criar seu método de teste**

```csharp
public class MinhaClasse
{
    public string MeuMetodo()
    {
        Thread.Sleep(6000); // Simulando um processo
        return "Finalizado";
    }
}
```

### 3. **Rastrear e salvar o resultado**

#### 🔹 Salvar como JSON:

```csharp
var resultado = tracker.ExecutionTracker(
    () => new MinhaClasse().MeuMetodo(),
    methodCalled: "MeuMetodo",
    description: "Simulação de processamento",
    user: "admin",
    fileName: "log.json",
    saveAs: "json",
    milisseconds: 5000
);

Console.WriteLine(tracker.LastGeneratedJson);
```

#### 🔹 Salvar como Excel:

```csharp
var resultado = tracker.ExecutionTracker(
    () => new MinhaClasse().MeuMetodo(),
    methodCalled: "MeuMetodo",
    description: "Teste com Excel",
    user: "admin",
    filePath: @"C:\logs\meuarquivo.xlsx",
    fileName: "log_excel.xlsx",
    saveAs: "xlsx",
    milisseconds: 5000
);
```

## 🔍 O que cada método faz?

### `TrackExecution<T>()`

Executa uma função e mede o tempo de execução. Dependendo da opção (`json` ou `xlsx`), salva os dados de log em JSON ou Excel.

### `SaveExcel()`

Salva os dados monitorados em um arquivo `.xlsx`, destacando automaticamente as linhas em **vermelho** se o tempo de execução for maior que o limite definido.

### `GenerateJson()`

Gera um objeto JSON com os dados da execução (timestamp, tempo, usuário, descrição, etc.) e armazena no `LastGeneratedJson`.

### `LastGeneratedJson`

Retorna a última string JSON gerada pela execução.

## 💡 Casos de Uso

- Monitorar **performance de métodos críticos**.
- Gerar logs automáticos para **auditoria interna**.
- Validar se determinados métodos estão excedendo tempos aceitáveis.
- Automatizar testes de **tempo de resposta** e documentar resultados.

## 📁 Estrutura Esperada

- `filePath`: caminho absoluto para salvar o Excel.
- `fileName`: nome do arquivo (sem necessidade de incluir `.json` ou `.xlsx`).
- `saveAs`: `"json"` ou `"xlsx"` — define o formato de saída.
- `milisseconds`: limite de tempo. Se ultrapassado, será destacado no Excel.

## ⚠️ Observações

- Para JSON, **não inclua `filePath`**.
- Para Excel, `filePath` **é obrigatório**.
- O sistema usa `Stopwatch` para cronometrar com precisão.

## 🛠 Tecnologias Usadas

- [.NET 6+](https://dotnet.microsoft.com/)
- [ClosedXML](https://github.com/ClosedXML/ClosedXML)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)

## 📜 Licença

Este projeto é de código aberto. Você pode adaptá-lo, usá-lo em seus projetos pessoais ou comerciais. Para registro formal de direitos autorais, veja abaixo.

## ✉️ Contato

Criado por **@claudionsc**  
[GitHub](https://github.com/claudionsc/TrackMethods)
