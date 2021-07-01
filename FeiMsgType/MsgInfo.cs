using System;
using System.Collections.Generic;
using System.Text;

namespace FeiMsgType
{
    /// <summary>
    /// ��Ϣ-�����Ϣ(myUid,toUid,privateTalk,mood|msg)
    /// </summary>
    [Serializable]
    public class MsgInfo
    {
        public MsgInfo() { }

        /// <summary>
        /// ����Ϣ�ַ���ת�� MsgInfo ���� - �����û�������Ϣʱʹ��
        /// </summary>
        /// <param name="msgOri">��Ϣ�ַ���</param>
        public MsgInfo(string msgOri)
        {
            string[] msgInfo = msgOri.Split('|');
            content = msgInfo[1];
            msgInfo = msgInfo[0].Split(',');
            if (msgInfo.Length > 1)
            {
                uId = msgInfo[0];
                toUid = msgInfo[1];
                isPrivate = bool.Parse(msgInfo[2]);
                mood = msgInfo[3];
            }
        }

        #region +����ڿͻ��˴���ʾ�ĸ�ʽ���ַ��� - ToSayInClient()
        /// <summary>
        /// ����ڿͻ��˴���ʾ�ĸ�ʽ���ַ���
        /// </summary>
        /// <returns></returns>
        public string ToSayInClient(string hostID)
        {
            return "��" + (UId.Equals(hostID) ? "��" : UId) + "���ԡ�" + (ToUid == string.Empty ? "������" : hostID.Equals(ToUid) ? "��" : ToUid) + "��" + (IsPrivate ? "���ĵ� " : "") + Mood + " ˵��" + Content;
        } 
        #endregion

        #region +����ڷ���˴���ʾ�ĸ�ʽ���ַ��� - ToSayInServerTextBox()
        /// <summary>
        /// ����ڷ���˴���ʾ�ĸ�ʽ���ַ���
        /// </summary>
        /// <returns></returns>
        public string ToSayInServerTextBox()
        {
            return "��" + UId + "���ԡ�" + (ToUid == string.Empty ? "������" : ToUid) + "��" + (IsPrivate ? "���ĵ� " : "") + Mood + " ˵��" + Content;
        }
        #endregion

        #region +���ش�����ĸ�ʽ���ַ���(��ʽ��������ID��������ID���Ƿ�˽�ģ�����|����) - ToTransString()
        /// <summary>
        /// ��ô�����ĸ�ʽ���ַ���(��ʽ��������ID,������ID,�Ƿ�˽��,����|����)
        /// </summary>
        /// <returns>������ID,������ID,�Ƿ�˽��,���� | ����</returns>
        public string ToTransString()
        {
            return UId + "," + ToUid + "," + IsPrivate+ "," + Mood + "|" + Content;
        } 
        #endregion

        #region +��ô������͵Ĵ����͵���Ϣ�ֽ����� - ToByteArrTypeMsg(string originalStr, MsgType type)
        /// <summary>
        /// ��ô������͵Ĵ����͵���Ϣ�ֽ�����
        /// </summary>
        /// <param name="originalStr">�ַ���</param>
        /// <param name="type">��Ϣ����</param>
        /// <returns></returns>
        public static byte[] ToByteArrTypeMsg(string originalStr, MsgType type)
        {
            byte[] orgByte = Encoding.UTF8.GetBytes(originalStr);
            byte[] finalByte = new byte[orgByte.Length + 1];
            finalByte[0] = (byte)type;
            Buffer.BlockCopy(orgByte, 0, finalByte, 1, orgByte.Length);
            return finalByte;
        }
        #endregion

        #region ��Ϣ����
        private MsgType type;
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public MsgType Type
        {
            get { return type; }
            set { type = value; }
        } 
        #endregion

        #region ��Ϣ������ID
        private string uId = string.Empty;
        /// <summary>
        /// ��Ϣ������ID
        /// </summary>
        public string UId
        {
            get { return uId; }
            set { uId = value; }
        } 
        #endregion

        #region ������ID
        private string toUid=string.Empty;
        /// <summary>
        /// ������ID
        /// </summary>
        public string ToUid
        {
            get { return toUid; }
            set { toUid = value; }
        } 
        #endregion

        #region �Ƿ�˽��
        private bool isPrivate;
        /// <summary>
        /// �Ƿ�˽��
        /// </summary>
        public bool IsPrivate
        {
            get { return isPrivate; }
            set { isPrivate = value; }
        } 
        #endregion

        #region ����
        private string mood = string.Empty;
        /// <summary>
        /// ����
        /// </summary>
        public string Mood
        {
            get { return mood; }
            set { mood = value; }
        } 
        #endregion

        #region ��Ϣ����
        private string content = string.Empty;
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        } 
        #endregion
    }
}
