using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Collect.Api.Utils
{
    public static class SQSHelper
    {
        /// <summary>
         /// AWS에서 사용자 자격을 획득합니다.
         /// </summary>
         /// <exception cref="Exception">획득 실패</exception>
        public static AWSCredentials GetAWSCredentials(string profileName)
        {
            var chain = new CredentialProfileStoreChain();
            AWSCredentials credentials;

            if (!chain.TryGetAWSCredentials("sqs-user", out credentials))
            {
                throw new Exception("Credentials not found.");
            }

            return credentials;
        }

        /// <summary>
        /// SQS 큐 URL을 획득합니다.
        /// </summary>
        /// <exception cref="Exception">획득 실패</exception>
        public static async Task<string> GetQueueUrl(IAmazonSQS client, string queueName)
        {
            var response = await client.GetQueueUrlAsync(new GetQueueUrlRequest
            {
                QueueName = queueName
            });

            return response.QueueUrl;
        }
    }
}
