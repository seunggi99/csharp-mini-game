using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;

using System.Net.Sockets;
using System.Threading;
using System.IO;


namespace csharp_mini_game
{
    delegate void SetTextDelegate(string s);
    public partial class Chat : Form
    {
        public Chat()
        {
            InitializeComponent();
        }
        TcpClient tcpClient = null;

        NetworkStream ntwStream = null;

        //서버와 채팅을 실행

        ChatHandler chatHandler = new ChatHandler();

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "입장")
            {
                try
                {
                    tcpClient = new TcpClient();
                    tcpClient.Connect(IPAddress.Parse("192.168.179.119"), 2022);
                    ntwStream = tcpClient.GetStream();


                    chatHandler.Setup(this, ntwStream, this.txtChatMsg);
                    Thread chatThread = new Thread(new ThreadStart(chatHandler.ChatProcess));
                    chatThread.Start();
                    Message_Snd("<" + Login.loginnickname + "> 님께서 접속 하셨습니다.", true);
                    btnConnect.Text = "나가기";
                }
                catch (System.Exception Ex)
                {
                    MessageBox.Show("Server 오류발생 또는 Start 되지 않았거나\n\n" + Ex.Message, "Client");
                }
            }

            else
            {
                Message_Snd("<" + Login.loginnickname + "> 님께서 접속해제 하셨습니다.", false);
                btnConnect.Text = "입장";
                chatHandler.ChatClose();
                ntwStream.Close();
                tcpClient.Close();
                MessageBox.Show("접속 해제되었습니다.");
            }
        }

        private void Message_Snd(string lstMessage, Boolean Msg)
        {
            try
            {
                //보낼 데이터를 읽어 Default 형식의 바이트 스트림으로 변환 해서 전송
                string dataToSend = lstMessage + "\r\n";
                byte[] data = Encoding.UTF8.GetBytes(dataToSend);
                ntwStream.Write(data, 0, data.Length);
            }

            catch (Exception Ex)
            {
                if (Msg == true)
                {
                    MessageBox.Show("서버가 Start 되지 않았거나\n\n" + Ex.Message, "Client");
                    btnConnect.Text = "입장";
                    chatHandler.ChatClose();
                    ntwStream.Close();
                    tcpClient.Close();
                }
            }
        }

        public void SetText(string text)
        {
            if (this.txtChatMsg.InvokeRequired)
            {
                SetTextDelegate d = new SetTextDelegate(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtChatMsg.AppendText(text);
            }
        }

        private void txtMsg_KeyPress(object sender, KeyPressEventArgs e)

        {
            if (e.KeyChar == 13)
            {
                //서버에 접속이 된 경우에만 메시지를 서버로  보냄
                if (btnConnect.Text == "나가기")
                {
                    Message_Snd("<" + Login.loginnickname + "> " + txtMsg.Text, true);

                }
                txtMsg.Text = "";
                e.Handled = true;  //이벤트처리중지, KeyUp or Click등
            }
        }

    }


    public class ChatHandler
    {
        private TextBox txtChatMsg;
        private NetworkStream netStream;
        private StreamReader strReader;
        private Chat chat;


        public void Setup(Chat chat, NetworkStream netStream, TextBox txtChatMsg)
        {
            this.txtChatMsg = txtChatMsg;
            this.netStream = netStream;
            this.chat = chat;
            this.netStream = netStream;
            this.strReader = new StreamReader(netStream);
        }

        public void ChatClose()
        {
            netStream.Close();
            strReader.Close();
        }

        public void ChatProcess()
        {
            while (true)
            {
                try
                {
                    //문자열을 받음
                    string lstMessage = strReader.ReadLine();

                    if (lstMessage != null && lstMessage != "")
                    {
                        //SetText 메서드에서 델리게이트를 이용하여 서버에서 넘어오는 메시지를 쓴다.
                        chat.SetText(lstMessage + "\r\n");
                    }
                }
                catch (System.Exception)
                {
                    break;
                }
            }
        }

    }


}
