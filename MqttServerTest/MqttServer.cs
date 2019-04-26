using MQTTnet;
using MQTTnet.Diagnostics;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MqttServerTest
{
    public static class MqttServer
    {
        private static IMqttServer mqttServer = null;

        


        public static async Task StopAsync()
        {
            await mqttServer?.StopAsync();
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
}
