using SqlSugar;
using System;

namespace sdglsys.Entity
{
    [SugarTable("t_log")]
    /// <summary>
    /// ��־
    /// </summary>
    public class TLog
    {
        private System.Int32 _Id;
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "��־ID")]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }
        
        private System.String _Login_name;
        /// <summary>
        /// �û���
        /// </summary>
        [SugarColumn(Length = 15,ColumnDescription = "�û���", ColumnDataType = "varchar")]
        public System.String Login_name { get { return this._Login_name; } set { this._Login_name = value.Trim(); } }
        
        private System.String _Ip;
        /// <summary>
        /// ����IP
        /// </summary>
        [SugarColumn(Length = 20, ColumnDescription = "����IP")]
        public System.String Ip { get { return this._Ip; } set { this._Ip = value.Trim(); } }

        private System.String _Info;
        /// <summary>
        /// ��־��Ϣ
        /// </summary>
        [SugarColumn(ColumnDataType = "text", ColumnDescription = "��־��Ϣ")]
        public System.String Info { get { return this._Info; } set { this._Info = value.Trim(); } }

        private System.DateTime _Log_date = DateTime.Now;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [SugarColumn(ColumnDescription ="����ʱ��")]
        public System.DateTime Log_date { get { return this._Log_date; } set { this._Log_date = value; } }
    }
}