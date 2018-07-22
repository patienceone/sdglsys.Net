using SqlSugar;

namespace sdglsys.Entity
{
    [SugarTable("t_dorm")]
    /// <summary>
    /// ԰��
    /// </summary>
    public class TDorm
    {
        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _Nickname;
        /// <summary>
        /// ����
        /// </summary>
        [SugarColumn(Length = 20)]
        public System.String Nickname { get { return this._Nickname; } set { this._Nickname = value.Trim(); } }

        private System.Boolean _Type=true;
        /// <summary>
        /// ���ͣ�0Ů,1�У�Ĭ��1
        /// </summary>
        public System.Boolean Type { get { return this._Type; } set { this._Type = value; } }

        private System.String _Note;
        /// <summary>
        /// ��ע
        /// </summary>
        public System.String Note { get { return this._Note; } set { this._Note = value.Trim(); } }

        private System.Boolean _Is_active = true;
        /// <summary>
        /// ״̬��0��ע����1������Ĭ��1
        /// </summary>
        public System.Boolean Is_active { get { return this._Is_active; } set { this._Is_active = value; } }
    }
}