using Programmers;

// 실행 인자가 없는 경우 직접 입력
if (args.Count() == 0)
{
    Console.Write("API base uri: ");
    Config.ApiBaseAddress = Console.ReadLine();
    
    Console.Write("Bot count: ");
    Config.BotCount = int.Parse(Console.ReadLine());
}
else
{
    Config.ApiBaseAddress = args[0];
    Config.BotCount = int.Parse(args[1]);
}

List<UserBot> bots = new();
List<Task> tasks = new();

// 봇을 생성합니다.
for (int i = 0; i < Config.BotCount; i++)
{
    bots.Add(new UserBot());
}

// 봇을 동작시킵니다.
foreach (var bot in bots)
{
    tasks.Add(bot.RunAsync());
}

Task.WaitAll(tasks.ToArray());