using MQTTnet;
using MQTTnet.Diagnostics;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace MqttServerTest
{
    class Program
    {
        private static IMqttServer mqttServer = null;

        static void Main(string[] args)
        {
            //MqttNetGlobalLogger.LogMessagePublished += (s,e)=>
            //{
            //    var trace = $">> [{e.TraceMessage.Timestamp:O}] [{e.TraceMessage.ThreadId}] [{e.TraceMessage.Source}] [{e.TraceMessage.Level}]: {e.TraceMessage.Message}";
            //    if (e.TraceMessage.Exception != null)
            //    {
            //        trace += Environment.NewLine + e.TraceMessage.Exception.ToString();
            //    }

            //    Console.WriteLine(trace);
            //};
            //StartMqttServerAsync().Wait();
            Task.Run(async () => { await StartMqttServerAsync(); });


            while (true)
            {
                var inputString = Console.ReadLine().ToLower().Trim();

                if (inputString == "exit")
                {
                    mqttServer?.StopAsync();
                    Console.WriteLine("MQTT服务已停止！");
                    break;
                }
                else if (inputString == "clients")
                {
                    foreach (var item in mqttServer.GetClientSessionsStatus())
                    {
                        Console.WriteLine($"客户端标识：{item.ClientId}，协议版本：{item.ProtocolVersion}");
                    }
                }
                else
                {
                    Console.WriteLine($"命令[{inputString}]无效！");
                }
            }
        }


        public static async Task StartMqttServerAsync()
        {
            if (mqttServer == null)
            {
                try
                {
                    var options = new MqttServerOptionsBuilder().WithConnectionValidator(context =>
                    {
                        //验证MQTT客户端
                        if (!string.IsNullOrEmpty(context.ClientId))
                        {
                            //if (context.Username != "u001" || context.Password != "p001")
                            //{
                            //    context.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                            //}
                            context.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
                        }
                        else
                        {
                            context.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
                        }
                    })
                    .WithConnectionBacklog(2000)//设置连接数
                    .WithDefaultEndpointPort(1883)//默认监听端口是1883 这里设置成1884
                    .WithPersistentSessions()//使用持续会话
                    .WithEncryptionSslProtocol(SslProtocols.Tls12)
                    .WithStorage(new RetainedMessageHandler())//存储的实现,保留的应用程序消息
                    //拦截消息,扩展来自客户端的所有消息的时间戳
                    .WithApplicationMessageInterceptor(context =>
                    {
                        if (MqttTopicFilterComparer.IsMatch(context.ApplicationMessage.Topic, "/myTopic/WithTimestamp/#"))
                        {
                            context.ApplicationMessage.Payload = Encoding.UTF8.GetBytes(DateTime.Now.ToString("O"));
                            Console.WriteLine("此消息被被处理");
                        }
                    })

                    //拦截订阅
                    .WithSubscriptionInterceptor(context =>
                    {
                        if (context.TopicFilter.Topic.StartsWith("admin/foo/bar") && context.ClientId != "theAdmin")
                        {
                            context.AcceptSubscription = false;
                        }

                        if (context.TopicFilter.Topic.StartsWith("the/secret/stuff") && context.ClientId != "Imperator")
                        {
                            context.AcceptSubscription = false;
                            context.CloseConnection = true;
                        }
                    })
                    .Build();

                    //使用证书
                    //var certificate = new X509Certificate2(@"C:\certs\test\test.cer", "", X509KeyStorageFlags.Exportable);
                    //options.TlsEndpointOptions.Certificate = certificate.Export(X509ContentType.Pfx);
                    //options.TlsEndpointOptions.IsEnabled = true;

                    mqttServer = new MqttFactory().CreateMqttServer();

                    mqttServer.ApplicationMessageReceived += MqttServer_ApplicationMessageReceived;
                    //mqttServer.ClientSubscribedTopic += MqttServer_ClientSubscribedTopic;//订阅事件
                    //mqttServer.ClientUnsubscribedTopic += MqttServer_ClientUnsubscribedTopic;//取消订阅事件

                    mqttServer.ClientConnected += MqttServer_ClientConnected;
                    mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;
                    await Task.Run(async () => { await mqttServer.StartAsync(options); });

                    //mqttServer.StartAsync(options).Wait();
                    Console.WriteLine("MQTT服务启动成功！");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }

        private static void MqttServer_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.ClientId}]已连接............");
        }

        private static void MqttServer_ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.ClientId}]已断开连接！..........");
        }

        private static void MqttServer_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.ClientId}]>> 主题：{e.ApplicationMessage.Topic} 负荷：{Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} Qos：{e.ApplicationMessage.QualityOfServiceLevel} 保留：{e.ApplicationMessage.Retain}");
            
        }

        private static void OnLogMessagePublished(object sender, MqttNetLogMessagePublishedEventArgs e)
        {
            /*Console.WriteLine($">> 线程ID：{e.ThreadId} 来源：{e.Source} 跟踪级别：{e.Level} 消息: {e.Message}");

            if (e.Exception != null)
            {
                Console.WriteLine(e.Exception);
            }*/

            //if (_useHandler)
            //{
            //    eventArgs.TraceMessage.ToString();
            //}
        }
    }


    /// <summary>
    /// 服务器支持保留的MQTT消息。这些消息会在连接并订阅时保留并发送给客户端。
    /// 它还支持保存所有保留的消息并在服务器启动后加载它们。这需要实现一个接口。
    /// 以下代码显示如何将保留的消息序列化为JSON：
    /// </summary>
    public class RetainedMessageHandler : IMqttServerStorage
    {
        //存储的实现：//此代码使用JSON库“Newtonsoft.Json
        private const string Filename = "C:\\RetainedMessages.json";

        public Task SaveRetainedMessagesAsync(IList<MqttApplicationMessage> messages)
        {
            //var json = File.ReadAllText(Filename);
            //var jsonObject = JsonConvert.DeserializeObject<List<MqttApplicationMessage>>(json ?? "") ?? new List<MqttApplicationMessage>();
            //foreach (var item in jsonObject)
            //{
            //    messages.Add(item);
            //}
            //File.WriteAllText(Filename, JsonConvert.SerializeObject(messages));
            return Task.FromResult(0);
        }

        public Task<IList<MqttApplicationMessage>> LoadRetainedMessagesAsync()
        {
            IList<MqttApplicationMessage> retainedMessages;
            if (File.Exists(Filename))
            {
                var json = File.ReadAllText(Filename);
                retainedMessages = JsonConvert.DeserializeObject<List<MqttApplicationMessage>>(json);
            }
            else
            {
                retainedMessages = new List<MqttApplicationMessage>();
            }

            return Task.FromResult(retainedMessages);
        }
    }
    
}