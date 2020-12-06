using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FunctionApp1
{
	public static class Function1
	{
		[FunctionName("Function1")]
		[StorageAccount("StorageAccountConnection")]
		[ServiceBusAccount("ServiceBusConnection")]
		public static IActionResult Run(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", "post",
				Route = "todoitems/{partitionKey}/{rowKey}")]HttpRequest req,
			[Table(tableName: "MyTable", partitionKey: "{partitionKey}", rowKey: "{rowKey}")] ToDoItem toDoItem,
			[Queue(queueName: "MyQueue")] ICollector<string> desQueue,
			[ServiceBus(queueOrTopicName: "MyServiceBusQueue")] ICollector<string> serviceBusQueue,
			ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			if (toDoItem == null)
			{
				log.LogInformation($"ToDo item not found");
			}
			else
			{
				log.LogInformation($"Found ToDo item, Description={toDoItem.Description}");
				desQueue.Add(JsonSerializer.Serialize<ToDoItem>(toDoItem));
				serviceBusQueue.Add(JsonSerializer.Serialize<ToDoItem>(toDoItem));
			}
			return new OkResult();
		}
	}

	public class ToDoItem
	{
		public string Id { get; set; }
		public string PartitionKey { get; set; }
		public string Description { get; set; }
	}
}
