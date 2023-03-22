using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    delegate void SetTextDelegate(string s);
    public partial class Form1 : Form
    {
        // 서버 변수 선언
        // 사용할 IP랑 Port 정의
        TcpListener chatServer = new TcpListener(IPAddress.Parse("192.168.179.119"), 2022);
        public static ArrayList clientSocketArray = new ArrayList();

        public class ClientHandler
        {
            private TextBox txtChat;
            private Socket socketClient;
            private NetworkStream netStream;
            private StreamReader strReader;
            private Form1 form1;

            public void ClientHandle_Setup(Form1 form1, Socket socketClient, TextBox txtChat)
            {
                this.txtChat = txtChat;
                this.socketClient = socketClient;
                this.netStream = new NetworkStream(socketClient);
                
                Form1.clientSocketArray.Add(socketClient);
                this.strReader = new StreamReader(netStream);
                this.form1 = form1;
            }
            public void ChatProcess()
            {
                while (true)
                {
                    try
                    {
                        string listMessage = strReader.ReadLine();
                        if(listMessage != null&&listMessage != "")
                        {
                            form1.SetText(listMessage + "\r\n");
                            byte[] bytSand_Data = Encoding.Default.GetBytes(listMessage + "\r\n");
                            lock (Form1.clientSocketArray)
                            {
                                // 접속한 모든 클라이언트에게 byte 전송
                                foreach(Socket socket in Form1.clientSocketArray)
                                {
                                    NetworkStream stream = new NetworkStream(socket);
                                    stream.Write(bytSand_Data, 0, bytSand_Data.Length);
                                }
                            }

                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error !!!! " + ex.ToString());
                        Form1.clientSocketArray.Remove(socketClient);
                        break;
                    }
                }
            }
        }
        
        public Form1()
        {
            InitializeComponent();
        }
        
        
        //무한 루프시 Client 대기
        private void AcceptClient()
        {
            Socket socketClient = null;
            while (true)
            {
                try
                { 
                    socketClient = chatServer.AcceptSocket();

                    ClientHandler clientHandler = new ClientHandler();
                    clientHandler.ClientHandle_Setup(this, socketClient, this.txtChat);
                    Thread thd_ChatProcess = new Thread(new ThreadStart(clientHandler.ChatProcess));
                    thd_ChatProcess.Start();
                }
                catch
                {
                    Form1.clientSocketArray.Remove(socketClient);
                    break;
                }
            }
        }
        public void SetText(string text)
        {
            if (this.txtChat.InvokeRequired)
            {
                SetTextDelegate d = new SetTextDelegate(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtChat.AppendText(text);
            }
        }
        private void btnServer_Click(object sender, EventArgs e)
        {
            try{
                if(lbl.Tag.ToString() == "Stop")
                {
                    // 서버 상태를 나타내는 tag가 stop이면 서버 시작
                    chatServer.Start();
                    // 서버가 시작되면 스레드로 던져줘야해서 스레드도 생성 및 시작
                    Thread waitThread = new Thread(new ThreadStart(AcceptClient));
                    waitThread.Start();

                    lbl.Text = "Server On";
                    lbl.Tag = "Start";
                    btnServer.Text = "Server Close";
                }
                else
                {
                    chatServer.Stop();
                    foreach(Socket socket in Form1.clientSocketArray)
                    {
                        socket.Close();
                    }
                    clientSocketArray.Clear();
                    lbl.Text = "Server Off";
                    lbl.Tag = "Stop";
                    btnServer.Text = "Server Open";
                }
            }
            catch (Exception ex){
                MessageBox.Show("Server Error....." + ex.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            chatServer.Stop();
        }
    }
}