using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MqttClientWin
{
    public partial class FmMqttClient : Form
    {
        private IMqttClient mqttClient = null;

        public FmMqttClient()
        {
            InitializeComponent();
            Thread.Sleep(1000);
            Task.Run(async () => { await ConnectMqttServerAsync(); });
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <returns></returns>
        private async Task ConnectMqttServerAsync()
        {
            try
            {
                //实例化 创建客户端对象
                mqttClient = new MqttFactory().CreateMqttClient();
                mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
                mqttClient.Connected += MqttClient_Connected;
                mqttClient.Disconnected += MqttClient_Disconnected;

                await mqttClient.ConnectAsync(option());
            }
            catch (Exception ex)
            {
                Invoke((new Action(() =>
                {
                    txtReceiveMessage.AppendText($"连接到MQTT服务器失败！" + Environment.NewLine + ex.Message + Environment.NewLine);
                })));
            }
        }


        private IMqttClientOptions option()
        {
            //连接到服务器前，获取所需要的MqttClientTcpOptions 对象的信息
            var options = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString().Substring(0, 5))                    // clientid是设备id
            .WithTcpServer("127.0.0.1", 1883)              //onenet ip：183.230.40.39    port:6002
            .WithCredentials(UserName.Text, Pwd.Text)      //username为产品id       密码为鉴权信息或者APIkey
            //.WithTls(new MqttClientOptionsBuilderTlsParameters
            //{
            //    UseTls = true,
            //    AllowUntrustedCertificates = true,
            //    IgnoreCertificateChainErrors = true,
            //    IgnoreCertificateRevocationErrors = true
            //})//服务器端没有启用加密协议，这里用tls的会提示协议异常
            .WithCleanSession(false)
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(2000))
            .WithCleanSession(true)
            .Build();
            return options;
        }


        /// <summary>
        /// 服务器连接成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_Connected(object sender, EventArgs e)
        {
            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText("已连接到MQTT服务器！" + Environment.NewLine);
            })));
        }

        /// <summary>
        /// 断开服务器连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_Disconnected(object sender, EventArgs e)
        {
            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText("已断开MQTT连接！" + Environment.NewLine);
            })));
        }

        /// <summary>
        /// 接收到消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText($">> {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}{Environment.NewLine}");
            })));
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSubscribe_ClickAsync(object sender, EventArgs e)
        {
            string topic = txtSubTopic.Text.Trim();

            if (string.IsNullOrEmpty(topic))
            {
                MessageBox.Show("订阅主题不能为空！");
                return;
            }

            if (!mqttClient.IsConnected)
            {
                MessageBox.Show("MQTT客户端尚未连接！");
                return;
            }

            //mqttClient.SubscribeAsync(new List<TopicFilter> {
            //    new TopicFilter(topic, MqttQualityOfServiceLevel.AtMostOnce)
            //});

            mqttClient.SubscribeAsync(new TopicFilterBuilder()
               .WithTopic(topic)
               .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
               .Build()
            );

            txtReceiveMessage.AppendText($"已订阅[{topic}]主题" + Environment.NewLine);
            //txtSubTopic.Enabled = false;
            //btnSubscribe.Enabled = false;
        }

        /// <summary>
        /// 发布主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPublish_Click(object sender, EventArgs e)
        {
            string topic = txtPubTopic.Text.Trim();

            if (string.IsNullOrEmpty(topic))
            {
                MessageBox.Show("发布主题不能为空！");
                return;
            }

            string inputString = txtSendMessage.Text.Trim();
            //var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(inputString), MqttQualityOfServiceLevel.AtMostOnce, false);
            //mqttClient.PublishAsync(appMsg);

            mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
               .WithTopic(topic)
               .WithPayload(inputString)
               .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
               .WithRetainFlag(true)
               .Build()
            ).Wait();
        }

        private void FmMqttClient_Load(object sender, EventArgs e)
        {
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("ClientId", "123");
            //dic.Add("Topic", "ttt");
            //dic.Add("Value", "ggyy");
            //dic.Add("ServiceLevel", "1");
            //TopicLogic.SaveTopic(dic);
        }

        private void connect_Click(object sender, EventArgs e)
        {
            if (mqttClient == null)
            {
                mqttClient = new MqttFactory().CreateMqttClient();
            }
            if (!mqttClient.IsConnected)
            {
                Task.Run(async () => { await ConnectMqttServerAsync(); });
                connect.Text = "断开";
            }
            else
            {
                Task.Run(async () => { await mqttClient.DisconnectAsync(); });
                connect.Text = "连接";
            }
        }
    }
}