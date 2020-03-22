using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bin2019.Misc
{   
    /// <summary>
    /// socket通讯类
    /// </summary>
    public class SocketClient
    {
        public Socket client { get; set; }

        public SocketClient()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), Envior.PRINT_PORT));             
            Console.WriteLine("客户端已经开启");
        }


        /// <summary>
        /// 向特定ip的主机的端口发送数据报
        /// </summary>
        public void sendMsg(string msg)
        {
            EndPoint point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000);
            this.client.SendTo(Encoding.UTF8.GetBytes(msg), point);
        }

    }
}
