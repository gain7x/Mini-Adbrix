using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using CollectWorker.Services;
using Shared.Dtos;
using Shared.Models;
using System.Text.Json;
using static Amazon.Lambda.SQSEvents.SQSEvent;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CollectWorker;

public class Function
{
    private const string LoginEventType = "Login";
    private readonly IRepository _repository;

    public Function()
    {
        var connString = Environment.GetEnvironmentVariable("DB_CONN_STR")
            ?? throw new ArgumentNullException("DB_CONN_STR not found.");

        _repository = new MySqlRepository(connString);
    }

    /// <summary>
    /// ���� �ڵ鷯�Դϴ�.
    /// </summary>
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        List<UserEventDto> dtos = GetUserEvents(evnt.Records);

        context.Logger.LogInformation($"DTO Count: {dtos.Count()}");

        try
        {
            await Task.WhenAll(CollectTasks(dtos, context.Logger));
        }
        catch (Exception ex)
        {
            context.Logger.LogError(ex.Message);
        }
    }

    /// <summary>
    /// DTO�� �𵨷� ��ȯ �� DB�� �����մϴ�.
    /// </summary>
    private async Task CollectTasks(List<UserEventDto> dtos, ILambdaLogger logger)
    {
        foreach (var dto in dtos)
        {
            try
            {
                if (IsLoginEvent(dto))
                    await CreateUser(dto);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to create User: {ex.Message}");
            }

            try
            {
                await CollectEvent(dto);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save Event: {ex.Message}");
            }
        }
    }

    private bool IsLoginEvent(UserEventDto dto) => dto.EventType == LoginEventType;

    private Task CreateUser(UserEventDto dto)
    {
        User user = GetUserFromDto(dto);
        return _repository.SaveAsync(user);
    }

    private Task CollectEvent(UserEventDto dto)
    {
        Event eventItem = GetEventFromDto(dto);
        return _repository.SaveAsync(eventItem);
    }

    /// <summary>
    /// ���� API�� SQS�� ������ ���� DTO�� ����ϴ�.
    /// </summary>
    private List<UserEventDto> GetUserEvents(List<SQSMessage> records)
    {
        List<UserEventDto> userEvents = new();
        
        foreach (var message in records)
        {
            UserEventDto? dto = JsonSerializer.Deserialize<UserEventDto>(message.Body);

            if (dto != null)
            {
                userEvents.Add(dto);
            }
        }

        return userEvents;
    }

    /// <summary>
    /// DTO�� Ȯ���Ͽ� User�� ����ϴ�.
    /// </summary>
    private User GetUserFromDto(UserEventDto dto) => new(dto.UserId);

    /// <summary>
    /// DTO�� Ȯ���Ͽ� Event�� ����ϴ�.
    /// </summary>
    private Event GetEventFromDto(UserEventDto dto) => new(
        dto.EventId,
        dto.EventType,
        dto.Parameters,
        dto.UserId);
}