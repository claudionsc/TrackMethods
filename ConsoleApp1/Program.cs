using ConsoleApp1;
using TrackMethods.Builders;
using TrackMethods.Services;

Teste testar = new Teste();

var builder = new Tracker<object>(new TrackMethod())
    .WithMethod("Tester")
    .WithDescription("Executa método void")
    .WithUser("admin")
    .WithFilePath(@"C:logs\meuarquivo.xlsx")
    .WithFileName("teste")
    .WithSaveAs("excel")
    .WithMilliseconds(1000);

// Wrapping o método void dentro de um lambda que retorna null
var result = builder.Build(() => {
    testar.Tester();
    return null;
});

string json = builder.LastGeneratedJson;
Console.WriteLine(json);
