using System;
using System.Collections.Generic;
using System.Text;

namespace FeiMsgType
{
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// �û���Ϣ
        /// </summary>
        UserMsg=0,
        /// <summary>
        /// �û��ļ�
        /// </summary>
        UserFile = 1,
        /// <summary>
        /// ϵͳ��Ϣ-�û��˳�
        /// </summary>
        SysMsgUserQuit = 2,
        /// <summary>
        /// ������Ϣ
        /// </summary>
        ConnectMsg=3,
        /// <summary>
        /// ��������
        /// </summary>
        UserShake=4,
        /// <summary>
        /// �����б�
        /// </summary>
        OnlineList=5,
        /// <summary>
        /// ���߸�������
        /// </summary>
        SysMsgOnlineLimit=6,
        /// <summary>
        /// ϵͳ��Ϣ
        /// </summary>
        SysMsg=7,
        /// <summary>
        /// �������˳�
        /// </summary>
        SysQuit=8,
        /// <summary>
        /// �û������ļ�
        /// </summary>
        UserFileSend=9,
        /// <summary>
        /// �û������ļ�
        /// </summary>
        UserFileRec = 10,
        /// <summary>
        /// �û��ܾ������ļ�
        /// </summary>
        UserFileRefuse=11,
        /// <summary>
        /// �߳�������
        /// </summary>
        SysBeKickOut=12,
        /// <summary>
        /// ���˱��߳���������
        /// </summary>
        SysKickedOne = 13,
        /// <summary>
        /// ��ֹĳ�˷���
        /// </summary>
        SysBeForbid = 14,
        /// <summary>
        /// ���˱���ֹ������
        /// </summary>
        SysForbidOne=15,
        /// <summary>
        /// �������
        /// </summary>
        SysNoForbid=16,
        /// <summary>
        /// ���˱����������
        /// </summary>
        SysNoForbidOne=17,
        /// <summary>
        /// ����
        /// </summary>
        SysBeWarning=18,
        /// <summary>
        /// ���˱�������
        /// </summary>
        SysWarningOne=19
    }
}
