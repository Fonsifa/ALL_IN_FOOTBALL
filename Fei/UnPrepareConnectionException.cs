using System;
using System.Collections.Generic;
using System.Text;

namespace Fei
{
    /// <summary>
    /// δ׼���õ������쳣
    /// </summary>
    public class UnPrepareConnectionException: Exception
    {
        public override string Message
        {
            get
            {
                return "���ӳ�ʼ������ΪNULL! (base.Message="+base.Message+")";
            }
        }
    }
}
