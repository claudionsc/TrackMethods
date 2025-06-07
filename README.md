# 📊 TrackMethods

TrackMethods é uma biblioteca C# desenvolvida para **monitorar a execução de métodos**, registrando métricas como tempo de resposta, nome do método, descrição e usuário. Os resultados podem ser **salvos em arquivos Excel (`.xlsx`) ou retornados como JSON** para análise ou visualização.

---

## 🚀 Funcionalidades

- Medir o **tempo de execução** de métodos.
- Salvar os dados da execução em **arquivo Excel**.
- Gerar um objeto JSON com os dados da execução.
- Configuração **fluente** e personalizável.
- Destaque visual no Excel para tempos que excedem o esperado.

---

## 📦 Instalação

Você deve referenciar o projeto ou a biblioteca `TrackMethods` em sua aplicação C#. As dependências necessárias incluem:

- `ClosedXML`
- `Newtonsoft.Json`
- Suporte a `System.Web.Mvc` para `JsonResult`

---

## 🧠 Quando usar?

- Para **logar execuções** de funções críticas em produção ou desenvolvimento.
- Para **auditoria** e rastreio de chamadas por usuário ou funcionalidade.
- Para **benchmarking** de performance esperada versus real.

---

## ⚙️ Estrutura do Projeto

| Componente | Função |
|-----------|--------|
| `Tracker<T>` | Builder fluente para configurar a rastreabilidade |
| `TrackMethod` | Serviço que executa, mede e salva os logs |
| `Execution` | Fábrica que cria instâncias de `Tracker<T>` |
| `ITrackMethod` | Interface para implementação customizada |
| `SaveExcel()` | Salva os dados em um `.xlsx` |
| `GenerateJson()` | Retorna um objeto JSON com os dados |

---

## ✅ Exemplo de Uso

### 1. Inicialização

```csharp
var execution = new Execution(new TrackMethod());
```

---

### 2. Método com retorno - Gerar JSON

```csharp
int MyTestMethod()
{
    Thread.Sleep(1200); // Simula demora
    return 42;
}

var result = execution.Create<int>()
    .WithMethod("MyTestMethod")
    .WithDescription("Teste de JSON")
    .WithUser("João")
    .WithFileName("log-test")
    .WithSaveAs("json")
    .WithMilliseconds(1000)
    .Build(() => MyTestMethod());

string jsonString = execution.Create<int>().LastGeneratedJson;
Console.WriteLine(jsonString);
```

#### 🔄 Dica: Convertendo `jsonString` para um objeto JSON

```csharp
dynamic jsonObj = JsonConvert.DeserializeObject(jsonString);
Console.WriteLine(jsonObj.elapsedTime);
```

---

### 3. Método com retorno - Salvar como Excel

```csharp
string MyMethod()
{
    Thread.Sleep(500);
    return "Tudo certo!";
}

var retorno = execution.Create<string>()
    .WithMethod("MyMethod")
    .WithDescription("Execução de método com retorno")
    .WithUser("Admin")
    .WithFilePath(@"C:\logs\meuarquivo.xlsx")
    .WithFileName("meuarquivo")
    .WithSaveAs("xlsx")
    .WithMilliseconds(400)
    .Build(() => MyMethod());
```

> 💾 O arquivo será salvo em `C:\logs\meuarquivo.xlsx`.

---

### 4. Método sem retorno (`void`) — Como testar

A biblioteca exige que o método tenha retorno. Para métodos `void`, basta usar um **wrapper com `Func<bool>`**:

```csharp
void MyVoidMethod()
{
    Console.WriteLine("Executando lógica sem retorno");
    Thread.Sleep(800);
}

bool WrapperForVoid()
{
    MyVoidMethod();
    return true; // Valor dummy
}

execution.Create<bool>()
    .WithMethod("MyVoidMethod")
    .WithDescription("Teste de método void")
    .WithUser("Claudio")
    .WithFilePath(@"C:\logs\voidlog.xlsx")
    .WithFileName("voidlog")
    .WithSaveAs("xlsx")
    .WithMilliseconds(600)
    .Build(() => WrapperForVoid());
```

---

## ⚠️ Regras e Validações

- `saveAs` deve ser `"json"` ou `"xlsx"`. Outros valores lançam exceção.
- Para salvar como JSON, **não** defina `filePath`.
- Para salvar como Excel, `filePath` **deve ser informado**.
- Tempo excedente ao esperado será **destacado em vermelho no Excel**.

---

## 📌 Observações Técnicas

- A classe `TrackMethod` implementa toda a lógica com `Stopwatch` para precisão na medição.
- O JSON gerado segue o seguinte padrão:

```json
{
  "timestamp": "04-06-2025 14:25:30.123",
  "elapsedTime": "00:00:01.20",
  "methodCalled": "MyTestMethod",
  "description": "Teste",
  "user": "João",
  "fileName": "log-test",
  "milisseconds": 1000
}
```

---

## 🔧 Extensibilidade

Você pode implementar `ITrackMethod` para usar outros formatos (como banco de dados, CSV, etc.).

---

## 🧪 Testes Recomendados

- Teste métodos rápidos e lentos.
- Altere o `milisseconds` para validar o destaque no Excel.
- Experimente salvar arquivos em diferentes pastas.

---

## 📫 Contato

- 📧 Email: [claudio.nsc@hotmail.com](mailto:claudio.nsc@hotmail.com)
- 💻 GitHub: [github.com/claudionsc](https://github.com/claudionsc)