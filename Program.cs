using System;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;

class Program
{
    static void Main()
    {
        // Nome do arquivo de log
        string nomeArquivoLog = "processos_log.log";

        // Cria o arquivo de log ou abre em modo de adição (append)
        StreamWriter arquivoLog = File.AppendText(nomeArquivoLog);

        if (arquivoLog != null)
        {
            // Obtém a data e hora atual
            string dataHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Obtém o endereço IP local
            string enderecoIP = GetLocalIPAddress();

            // Escreve a data, hora e endereço IP no arquivo de log
            arquivoLog.WriteLine("Data e Hora: " + dataHora);
            arquivoLog.WriteLine("Endereço IP Local: " + enderecoIP);
            arquivoLog.WriteLine("----------------------------------------------");

            // Verifica se o processo Discord.exe está em execução
            Process[] processes = Process.GetProcessesByName("Discord");
            bool discordExecutando = (processes.Length > 0);

            // Verifica se o processo steam.exe está em execução
            processes = Process.GetProcessesByName("steam");
            bool steamExecutando = (processes.Length > 0);

            // Escreve os resultados no arquivo de log
            if (discordExecutando && steamExecutando)
            {
                arquivoLog.WriteLine("Ambos os processos estão em execução: Discord.exe e steam.exe");
            }
            else if (discordExecutando)
            {
                arquivoLog.WriteLine("Discord.exe está em execução, mas steam.exe não está");
            }
            else if (steamExecutando)
            {
                arquivoLog.WriteLine("steam.exe está em execução, mas Discord.exe não está");
            }
            else
            {
                arquivoLog.WriteLine("Nenhum dos processos está em execução: Discord.exe e steam.exe");
            }

            // Fecha o arquivo de log
            arquivoLog.Close();

            // Exibe uma mensagem informando que os processos e o endereço IP foram registrados com sucesso
            Console.WriteLine("Os processos e o endereço IP local foram registrados no arquivo de log: " + nomeArquivoLog);
        }
        else
        {
            // Exibe uma mensagem de erro se não for possível abrir o arquivo de log
            Console.WriteLine("Erro ao abrir o arquivo de log: " + nomeArquivoLog);
        }
    }

    // Função para obter o endereço IP local
    static string GetLocalIPAddress()
    {
        string localIP = "N/A";
        NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface networkInterface in networkInterfaces)
        {
            if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            {
                IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

                foreach (UnicastIPAddressInformation ipAddress in ipProperties.UnicastAddresses)
                {
                    if (ipAddress.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        localIP = ipAddress.Address.ToString();
                        break;
                    }
                }
            }
        }

        return localIP;
    }
}
