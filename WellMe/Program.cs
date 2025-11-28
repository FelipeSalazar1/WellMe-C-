using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WellMe
{
    class Program
    {
        private static List<RegistroAtividade> registros = new List<RegistroAtividade>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ExibirBemVindo();
            
            bool continuar = true;
            while (continuar)
            {
                ExibirMenu();
                int opcao = LerOpcaoMenu();
                
                switch (opcao)
                {
                    case 1:
                        AdicionarRegistro();
                        break;
                    case 2:
                        ListarRegistros();
                        break;
                    case 3:
                        ExibirEstatisticas();
                        break;
                    case 4:
                        continuar = false;
                        Console.WriteLine("\nObrigado por usar o WellMe! Até logo!");
                        break;
                    default:
                        Console.WriteLine("\n⚠️  Opção inválida! Por favor, escolha uma opção entre 1 e 4.");
                        break;
                }
                
                if (continuar)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void ExibirBemVindo()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║     WellMe - Assistente de Saúde Preventiva ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine();
        }

        static void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║     WellMe - Assistente de Saúde Preventiva ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("MENU PRINCIPAL");
            Console.WriteLine("──────────────");
            Console.WriteLine("1. Adicionar registro");
            Console.WriteLine("2. Listar registros");
            Console.WriteLine("3. Exibir estatísticas");
            Console.WriteLine("4. Sair");
            Console.WriteLine();
            Console.Write("Escolha uma opção (1-4): ");
        }

        static int LerOpcaoMenu()
        {
            string? entrada = Console.ReadLine();
            
            if (int.TryParse(entrada, out int opcao) && opcao >= 1 && opcao <= 4)
            {
                return opcao;
            }
            
            return -1;
        }

        static void AdicionarRegistro()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║          ADICIONAR NOVO REGISTRO          ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine();

            TipoAtividade tipo = SelecionarTipoAtividade();
            if (tipo == TipoAtividade.Invalido)
            {
                Console.WriteLine("\n⚠️  Operação cancelada.");
                return;
            }

            DateTime data = LerData();
            if (data == DateTime.MinValue)
            {
                Console.WriteLine("\n⚠️  Operação cancelada.");
                return;
            }

            double valor = LerValor(tipo);
            if (valor < 0)
            {
                Console.WriteLine("\n⚠️  Operação cancelada.");
                return;
            }

            RegistroAtividade registro = new RegistroAtividade(tipo, data, valor);
            registros.Add(registro);

            Console.WriteLine();
            Console.WriteLine("✅ Registro adicionado com sucesso!");
            Console.WriteLine($"   Tipo: {ObterNomeTipoAtividade(tipo)}");
            Console.WriteLine($"   Data: {data:dd/MM/yyyy}");
            Console.WriteLine($"   Valor: {valor} {ObterUnidadeMedida(tipo)}");
        }

        static TipoAtividade SelecionarTipoAtividade()
        {
            Console.WriteLine("Tipos de atividade disponíveis:");
            Console.WriteLine("1. Exercício (minutos)");
            Console.WriteLine("2. Água (litros)");
            Console.WriteLine("3. Sono (horas)");
            Console.WriteLine("0. Cancelar");
            Console.WriteLine();
            Console.Write("Escolha o tipo (1-3): ");

            string? entrada = Console.ReadLine();
            
            if (int.TryParse(entrada, out int opcao))
            {
                return opcao switch
                {
                    1 => TipoAtividade.Exercicio,
                    2 => TipoAtividade.Agua,
                    3 => TipoAtividade.Sono,
                    0 => TipoAtividade.Invalido,
                    _ => TipoAtividade.Invalido
                };
            }

            return TipoAtividade.Invalido;
        }

        static DateTime LerData()
        {
            Console.Write("Informe a data (dd/MM/yyyy) ou pressione Enter para hoje: ");
            string? entrada = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada))
            {
                return DateTime.Today;
            }

            if (DateTime.TryParseExact(entrada, "dd/MM/yyyy", CultureInfo.InvariantCulture, 
                DateTimeStyles.None, out DateTime data))
            {
                return data;
            }

            Console.WriteLine("⚠️  Data inválida! Use o formato dd/MM/yyyy.");
            return DateTime.MinValue;
        }

        static double LerValor(TipoAtividade tipo)
        {
            string unidade = ObterUnidadeMedida(tipo);
            Console.Write($"Informe o valor ({unidade}): ");

            string? entrada = Console.ReadLine();

            if (double.TryParse(entrada, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor))
            {
                if (valor < 0)
                {
                    Console.WriteLine("⚠️  O valor não pode ser negativo!");
                    return -1;
                }

                return valor;
            }

            Console.WriteLine("⚠️  Valor inválido! Informe um número válido.");
            return -1;
        }

        static void ListarRegistros()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║            LISTAGEM DE REGISTROS          ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine();

            if (registros.Count == 0)
            {
                Console.WriteLine("📋 Nenhum registro cadastrado ainda.");
                Console.WriteLine("   Use a opção 1 do menu para adicionar registros.");
                return;
            }

            var registrosPorTipo = registros.GroupBy(r => r.Tipo)
                                            .OrderBy(g => g.Key);

            foreach (var grupo in registrosPorTipo)
            {
                Console.WriteLine($"\n{new string('─', 50)}");
                Console.WriteLine($"📊 {ObterNomeTipoAtividade(grupo.Key).ToUpper()}");
                Console.WriteLine(new string('─', 50));

                var registrosOrdenados = grupo.OrderBy(r => r.Data).ThenBy(r => r.Data.TimeOfDay);
                
                foreach (var registro in registrosOrdenados)
                {
                    Console.WriteLine($"   Data: {registro.Data:dd/MM/yyyy} | " +
                                    $"Valor: {registro.Valor} {ObterUnidadeMedida(registro.Tipo)}");
                }
            }

            Console.WriteLine($"\n{new string('─', 50)}");
            Console.WriteLine($"Total de registros: {registros.Count}");
        }

        static void ExibirEstatisticas()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║              ESTATÍSTICAS                 ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine();

            if (registros.Count == 0)
            {
                Console.WriteLine("📊 Nenhum registro cadastrado para calcular estatísticas.");
                Console.WriteLine("   Use a opção 1 do menu para adicionar registros.");
                return;
            }

            var estatisticasPorTipo = registros.GroupBy(r => r.Tipo)
                                               .Select(g => new
                                               {
                                                   Tipo = g.Key,
                                                   Total = g.Sum(r => r.Valor),
                                                   Media = g.Average(r => r.Valor),
                                                   Quantidade = g.Count()
                                               })
                                               .OrderBy(e => e.Tipo);

            foreach (var estatistica in estatisticasPorTipo)
            {
                string nomeTipo = ObterNomeTipoAtividade(estatistica.Tipo);
                string unidade = ObterUnidadeMedida(estatistica.Tipo);

                Console.WriteLine($"\n{new string('═', 50)}");
                Console.WriteLine($"📈 {nomeTipo.ToUpper()}");
                Console.WriteLine(new string('═', 50));
                Console.WriteLine($"   Total: {estatistica.Total:F2} {unidade}");
                Console.WriteLine($"   Média: {estatistica.Media:F2} {unidade}");
                Console.WriteLine($"   Registros: {estatistica.Quantidade}");
            }

            Console.WriteLine($"\n{new string('═', 50)}");
            Console.WriteLine($"Total geral de registros: {registros.Count}");
        }

        static string ObterNomeTipoAtividade(TipoAtividade tipo)
        {
            return tipo switch
            {
                TipoAtividade.Exercicio => "Exercício",
                TipoAtividade.Agua => "Água",
                TipoAtividade.Sono => "Sono",
                _ => "Desconhecido"
            };
        }

        static string ObterUnidadeMedida(TipoAtividade tipo)
        {
            return tipo switch
            {
                TipoAtividade.Exercicio => "minutos",
                TipoAtividade.Agua => "litros",
                TipoAtividade.Sono => "horas",
                _ => ""
            };
        }
    }

    enum TipoAtividade
    {
        Exercicio = 1,
        Agua = 2,
        Sono = 3,
        Invalido = 0
    }

    class RegistroAtividade
    {
        public TipoAtividade Tipo { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }

        public RegistroAtividade(TipoAtividade tipo, DateTime data, double valor)
        {
            Tipo = tipo;
            Data = data;
            Valor = valor;
        }
    }
}
