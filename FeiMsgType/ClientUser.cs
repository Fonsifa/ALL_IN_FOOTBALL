using System;
using System.Collections.Generic;
using System.Text;

namespace FeiMsgType
{
    /// <summary>
    /// �û���Ϣ��
    /// </summary>
    public class ClientUser
    {
        public ClientUser() { }
        public ClientUser(string uId,string uName)
        {
            id = uId;
            name = uName;
        }

        #region �Ƿ񱻽�ֹ���� - IsForbidTalk
        private bool isForbidTalk = false;
        /// <summary>
        /// �Ƿ񱻽�ֹ����(�Զ������û�״̬����)
        /// </summary>
        public bool IsForbidTalk
        {
            get { return isForbidTalk; }
            set
            {
                statu = (value ? UserStatu.BeForbid : UserStatu.OK);
                isForbidTalk = value;
            }
        } 
        #endregion

        #region �û�ID -ID
        private string id = string.Empty;
        /// <summary>
        /// �û�ID
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        } 
        #endregion

        #region �û��� - Name
        private string name=string.Empty;
        /// <summary>
        /// �û���
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        } 
        #endregion

        #region ��¼ʱ�� - LoginTime
        private DateTime loginTime;
        /// <summary>
        /// ��¼ʱ��
        /// </summary>
        public DateTime LoginTime
        {
            get { return loginTime; }
            set { loginTime = value; }
        } 
        #endregion

        #region �û�״̬(Ĭ������) - Statu
        private UserStatu statu= UserStatu.OK;
        /// <summary>
        /// �û�״̬(Ĭ������) - �������״̬�ַ���������ʹ������StatuString
        /// </summary>
        public UserStatu Statu
        {
            get { return statu; }
            set { statu = value; }
        } 
        #endregion

        #region �û�״̬�ַ���(Ĭ������) - StatuString
        /// <summary>
        /// �û�״̬(Ĭ������)
        /// </summary>
        public string StatuString
        {
            get 
            {
                return statu == UserStatu.OK ? "����" : "������";
            }
        }
        #endregion

        #region ����ID - FaceId
        private int faceId=1;
        /// <summary>
        /// ����ID
        /// </summary>
        public int FaceId
        {
            get { return faceId; }
            set { faceId = value; }
        } 
        #endregion

        #region �Ա� - Gender
        private bool gender=false;
        /// <summary>
        /// �Ա�
        /// </summary>
        public bool Gender
        {
            get { return gender; }
            set { gender = value; }
        } 
        #endregion

        #region ���� - Age
        private int age=18;
        /// <summary>
        /// ����
        /// </summary>
        public int Age
        {
            get { return age; }
            set { age = value; }
        } 
        #endregion

        #region �������� - Email
        private string email = string.Empty;
        /// <summary>
        /// ��������
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        } 
        #endregion

        #region ��ע - Remark
        private string remark = string.Empty;
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        } 
        #endregion
    }
}
