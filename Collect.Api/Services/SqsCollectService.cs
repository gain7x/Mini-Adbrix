using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using static Collect.Api.Utils.SQSHelper;

namespace Collect.Api.Services
{
    public class SqsCollectService : ICollectService
    {
        private readonly IConfiguration _configuration;

        private readonly string _queueUrl;
        private readonly AWSCredentials _credentials;

        public SqsCollectService(IConfiguration configuration)
        {
            _configuration = configuration;
            var userName = _configuration["SQS:UserName"];
            var queueName = _configuration["SQS:QueueName"];

            LoadCredentials(userName, out _credentials);
            LoadQueueUrl(queueName, out _queueUrl);
        }

        /// <summary>
        /// 사용자 자격을 불러옵니다.
        /// </summary>
        private void LoadCredentials(string userName, out AWSCredentials credentials)
        {
            credentials = GetAWSCredentials(userName);
        }

        /// <summary>
        /// SQS 큐 URL을 불러옵니다.
        /// </summary>
        private void LoadQueueUrl(string queueName, out string queueUrl)
        {
            var client = new AmazonSQSClient(_credentials);
            queueUrl = GetQueueUrl(client, queueName).Result;
        }

        /// <summary>
        /// 메시지를 SQS로 전송합니다.
        /// </summary>
        /// <exception cref="Exception">SQS 전송 실패</exception>
        public Task Collect(string message)
        {
            var client = new AmazonSQSClient(_credentials);

            return client.SendMessageAsync(new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = message
            });
        }
    }
}
