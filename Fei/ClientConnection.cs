using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using FeiMsgType;

namespace Fei
{
    /// <summary>
    /// �Ϳͻ������ӵ�ͨ����(�ں�Socket��ClientUser)
    /// </summary>
    class ClientConnection
    {
        #region ����ͨ�������û���Ϣ - User
        ClientUser user = null;
        /// <summary>
        /// ����ͨ�������û���Ϣ
        /// </summary>
        public ClientUser User
        {
            get { return user; }
            set { user = value; }
        } 
        #endregion
        Thread threadClient = null;
        Socket socket = null;
        string strEndpoint;
        FrmMain frmMain = null;
        /// <summary>
        /// �Ƿ��˳�-true�˳�/false����
        /// </summary>
        bool doesClose = false;

        #region ���캯��
        public ClientConnection(FrmMain frmMain, Socket socket)
        {
            InitConnection(frmMain, socket);
        }

        public ClientConnection(FrmMain frmMain, Socket socket, ClientUser user)
        {
            InitConnection(frmMain, socket);
            this.user = user;
        } 
        #endregion

        #region ��ʼ������ - InitConnection(FrmMain frmMain, Socket socket)
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="frmMain">����</param>
        /// <param name="socket">�����׽���</param>
        public void InitConnection(FrmMain frmMain, Socket socket)
        {
            if (null == frmMain || null == socket) throw new UnPrepareConnectionException();//��������������׳��Զ����쳣
            this.frmMain = frmMain;
            this.socket = socket;
            this.strEndpoint = socket.RemoteEndPoint.ToString();
            threadClient = new Thread(WatchMsg);
            threadClient.IsBackground = true;
            threadClient.Start();
        } 
        #endregion

        #region -�����ͻ�����Ϣ - WatchMsg()
        private void WatchMsg()
        {
            string strMsgRec = string.Empty;
            while (!doesClose)
            {
                try
                {
                    byte[] byteMsgRec = new byte[1024 * 1024 * 4];
                    int length = socket.Receive(byteMsgRec, byteMsgRec.Length, SocketFlags.None);
                    if (length > 0)
                    {
                        strMsgRec = Encoding.UTF8.GetString(byteMsgRec, 1, length - 1);//��ȡ��־�������Ϣ�ַ���
                        if (byteMsgRec[0] == (byte)MsgType.UserMsg)//�û���Ϣ
                        {
                            MsgInfo msgInfo = new MsgInfo(strMsgRec);
                            if (msgInfo.UId != string.Empty)//��Ч��Ϣ
                            {
                                if (msgInfo.ToUid != string.Empty && msgInfo.IsPrivate)//��������˲�Ϊ�ն�����˽����Ϣ
                                {
                                    frmMain.SendMsgUserToSingle(msgInfo.ToUid, msgInfo.ToTransString(), MsgType.UserMsg);
                                }
                                else//��������˵
                                {
                                    frmMain.SendMsgUserToEveryoneButOne(msgInfo.ToTransString(), msgInfo.UId, MsgType.UserMsg);
                                }
                            }
                            ShowMsg(msgInfo.ToSayInServerTextBox());
                        }
                        else if (byteMsgRec[0] == (byte)MsgType.UserShake)//���嶶��
                        {
                            string[] arrInfo = strMsgRec.Split(',');//fromUid,toUid
                            frmMain.SendMsgUserToSingle(arrInfo[1], arrInfo[0], MsgType.UserShake);
                            ShowMsg("�û���" + arrInfo[0] + "����" + arrInfo[1] + "�������˴��嶶������ )");
                        }
                        else if (byteMsgRec[0] == (byte)MsgType.UserFileSend)//�û��������ļ�
                        {
                            string[] arrFileInfo = strMsgRec.Split(',');//������Uid,������Uid,�ļ�·��
                            frmMain.SendMsgUserToSingle(arrFileInfo[1], strMsgRec, MsgType.UserFileSend);
                            ShowMsg("�û���" + arrFileInfo[0] + "����" + arrFileInfo[1] + "���������ļ�...");
                        }
                        else if (byteMsgRec[0] == (byte)MsgType.UserFileRec)//�û���������ļ�
                        {
                            string[] arrFileInfo = strMsgRec.Split(',');//fromUid,toUid,filePath
                            frmMain.SendMsgUserToSingle(arrFileInfo[0], strMsgRec, MsgType.UserFileRec);
                            ShowMsg("�û���" + arrFileInfo[1] + "��ͬ���˴ӡ�" + arrFileInfo[0] + "�������ļ�...");
                        }
                        else if (byteMsgRec[0] == (byte)MsgType.UserFileRefuse)//�û��ܾ������ļ�
                        {
                            string[] arrFileInfo = strMsgRec.Split(',');//fromUid,toUid,filePath
                            frmMain.SendMsgUserToSingle(arrFileInfo[0], strMsgRec, MsgType.UserFileRefuse);
                            ShowMsg("�û���" + arrFileInfo[1] + "��ͬ���˴ӡ�" + arrFileInfo[0] + "�������ļ�...");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (socket != null)
                    {
                        frmMain.RemoveListItem(strEndpoint);
                        ShowErr("�ͻ��ˡ�" + strEndpoint + "���˳�������!", ex);
                        ShowMsg("�û���" + strEndpoint + "���˳��ˣ�");
                        frmMain.SendUserQuitMsgToEveryone(this.strEndpoint);//���û��˳���Ϣ����ÿһ�������û�
                    }
                    break;
                }
            }
        }
        #endregion

        #region ���������û��б� - SendMsgOnlineList(string onlineList)
        /// <summary>
        /// ���������û��б�
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsgOnlineList(string onlineList)
        {
            SendMsgType(onlineList, MsgType.OnlineList);
        }
        #endregion

        #region ����ϵͳ��Ϣ - SendMsgSys(string msg)
        /// <summary>
        /// ����ϵͳ��Ϣ
        /// </summary>
        /// <param name="userID">�û�ID </param>
        public void SendMsgSys(string msg)
        {
            SendMsgType(msg, MsgType.SysMsg);
        }
        #endregion

        #region ����ϵͳ��Ϣ(�û��˳�) - SendMsgSys()
        /// <summary>
        /// ����ϵͳ��Ϣ(�û��˳�) 
        /// </summary>
        public void SendSysMsgUserQuit(string uId)
        {
            SendMsgType(uId, MsgType.SysMsgUserQuit);
        }
        #endregion

        #region ����ϵͳ��Ϣ(�������˳�) - SendSysMsgQuit()
        /// <summary>
        /// ����ϵͳ��Ϣ(�������˳�) 
        /// </summary>
        public void SendSysMsgQuit()
        {
            SendMsgType(this.strEndpoint, MsgType.SysQuit);
        }
        #endregion

        #region ����ϵͳ��Ϣ(���߸�������) - SendSysMsgOnlineLimit()
        /// <summary>
        /// ����ϵͳ��Ϣ(���߸�������) 
        /// </summary>
        public void SendSysMsgOnlineLimit()
        {
            SendMsgType("�����Ҹ����ѴＫ�ޣ������ʱ�����ԡ� : )��", MsgType.SysMsgUserQuit);
        }
        #endregion

        #region ���Ͳ�ͬ���͵���Ϣ - SendMsgType(string msg,MsgType type)
        /// <summary>
        /// ���Ͳ�ͬ���͵���Ϣ
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        /// <param name="type">����</param>
        public void SendMsgType(string msg,MsgType type)
        {
            try
            {
                byte[] msgSendByte = Encoding.UTF8.GetBytes(msg);
                byte[] finalByte = new byte[msgSendByte.Length + 1];
                finalByte[0] = (byte)type;
                Buffer.BlockCopy(msgSendByte, 0, finalByte, 1, msgSendByte.Length);
                socket.Send(finalByte);
            }
            catch (Exception ex)
            {
                ShowErr("SendMsg(string msg)", ex);
            }
        } 
        #endregion

        #region �����ļ� - SendFile(string fileName)
        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="fileName">�ļ�·��</param>
        public void SendFile(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open);
                byte[] byteFile = new byte[1024 * 1024 * 5];
                int length = fs.Read(byteFile, 0, byteFile.Length);
                if (length > 0)
                {
                    byte[] byteFinalFile = new byte[length + 1];
                    byteFinalFile[0] = (byte)MsgType.UserFile;
                    Buffer.BlockCopy(byteFile, 0, byteFinalFile, 1, length);
                    socket.Send(byteFinalFile);
                }
            }
            catch (Exception ex)
            {
                ShowErr("SendFile(string fileName)", ex);
            }
            finally
            {
                fs.Close();
            }
        } 
        #endregion

        #region ���Ͷ��� - SendShake()
        /// <summary>
        /// ���Ͷ���
        /// </summary>
        public void SendShake()
        {
            try
            {
                byte[] msgSendByte = Encoding.UTF8.GetBytes(this.strEndpoint);
                byte[] finalByte = new byte[msgSendByte.Length + 1];
                finalByte[0] = (int)MsgType.UserShake;
                Buffer.BlockCopy(msgSendByte, 0, finalByte, 1, msgSendByte.Length);
                socket.Send(finalByte);
            }
            catch (Exception ex)
            {
                ShowErr("SendShake()", ex);
            }
        }
        #endregion

        #region �ر���ͻ������� - Close()
        /// <summary>
        /// �ر���ͻ�������
        /// </summary>
        public void Close()
        {
            SendMsgType("�Բ��𣬷�����˳�����", MsgType.SysMsg);
            doesClose = true;
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        } 
        #endregion

        public void AbortThread()
        {
            threadClient.Abort();
        }

        #region -�ڴ����������ʾ��Ϣ
        private void ShowErr(string msg, Exception ex)
        {
            frmMain.ShowErr(msg, ex);
        }

        private void ShowMsg(string msg)
        {
            frmMain.ShowMsg(msg);
        }
        private void ShowLog(string msg)
        {
            frmMain.ShowLog(msg);
        } 
        #endregion
    }
}
