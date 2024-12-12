using Xunit;
using System;
using System.IO;
using System.Reflection;

namespace YoutubePlaylistDownloader.Tests
{
    public class DiagnosticTests
    {
        [Fact]
        public void CheckProjectStructure()
        {
            // Verifica se o assembly principal pode ser carregado
            var mainAssembly = Assembly.GetExecutingAssembly();
            Console.WriteLine($"Assembly atual: {mainAssembly.FullName}");

            // Verifica se o diretório do projeto existe
            var projectDir = Directory.GetCurrentDirectory();
            Console.WriteLine($"Diretório atual: {projectDir}");

            // Lista todos os assemblies carregados
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            Console.WriteLine("Assemblies carregados:");
            foreach (var assembly in loadedAssemblies)
            {
                Console.WriteLine($"- {assembly.FullName}");
            }

            // Verifica se os arquivos críticos existem
            var criticalFiles = new[]
            {
                "YoutubePlaylistDownloader.dll",
                "YoutubePlaylistDownloader.Tests.dll"
            };

            Console.WriteLine("Verificando arquivos críticos:");
            foreach (var file in criticalFiles)
            {
                var exists = File.Exists(Path.Combine(projectDir, file));
                Console.WriteLine($"- {file}: {(exists ? "Existe" : "Não existe")}");
            }

            // Verifica referências do projeto
            try 
            {
                var mainProjectType = Type.GetType("YoutubePlaylistDownloader.MainWindow, YoutubePlaylistDownloader");
                Console.WriteLine($"MainWindow type: {(mainProjectType != null ? "Encontrado" : "Não encontrado")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar MainWindow: {ex.Message}");
            }

            // Força o teste a passar para ver o output
            Assert.True(true);
        }
    }
} 