/*namespace APIAgendamentos.Logger;

public class LogHelper
{
    public static void GravaLog(string caminhoArquvo, string mensagem)
    {
        // Verifica se o diretório existe, se não, cria
        var directory = Path.GetDirectoryName(caminhoArquvo);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Escreve a mensagem no arquivo de log
        using (StreamWriter writer = new StreamWriter(caminhoArquvo, true, Encoding.ASCII))
        {
            writer.WriteLine($"{DateTime.Now}: {mensagem}");
        }
    }
}*/
