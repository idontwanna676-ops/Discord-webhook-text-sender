using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        string webhookUrl = " pase webhook url here";


        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(@"
  ___  _                     _ 
 |   \(_)___ __ ___ _ _ __ _| |
 | |) | (_-</ _/ _ \ '_/ _` | |
 |___/|_/__/\__\___/_| \__,_|_|
                               
 __      __   _    _                 _ 
 \ \    / /__| |__| |_  ___  ___  __| |
  \ \/\/ / -_) '_ \ ' \/ _ \/ _ \/ _` |
   \_/\_/\___|_.__/_||_\___/\___/\__,_|
                                       
  ___                 _ 
 / __| ___ _ _  _  __| |___ _ _ 
 \__ \/ -_) ' \| |/ _` / -_) '_|
 |___/\___|_||_|_|\__,_\___|_|  
                                
         By: wannawin
");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("================================================================");
        Console.WriteLine("Type your message and press Enter to send (type 'exit' to quit):");
        Console.WriteLine("================================================================");
        Console.ResetColor();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("WebHook Text > ");
            Console.ResetColor();

            string message = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(message))
            {
                continue;
            }

            if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            var payload = new
            {
                content = message
            };

            try
            {
                string jsonPayload = JsonSerializer.Serialize(payload);
                using (var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = await client.PostAsync(webhookUrl, content);
                    response.EnsureSuccessStatusCode();
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("[✔] Message sent successfully!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[✘] Error: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}