using SqlSugar;

namespace sdglsys.Entity
{
    [SugarTable("t_room")]
    /// <summary>
    /// ����
    /// </summary>
    public class TRoom
    {
        private System.Int32 _Id;
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32 _Pid;
        /// <summary>
        /// ����¥ID
        /// </summary>
        public System.Int32 Pid { get { return this._Pid; } set { this._Pid = value; } }

        private System.Int32 _Dorm_id;
        /// <summary>
        /// ԰��ID
        /// </summary>
        public System.Int32 Dorm_id { get { return this._Dorm_id; } set { this._Dorm_id = value; } }

        private System.String _Vid;
        /// <summary>
        /// �Զ�����
        /// </summary>
        public System.String Vid { get { return this._Vid; } set { this._Vid = value.Trim(); } }

        private System.String _Nickname;
        /// <summary>
        /// ����
        /// </summary>
        [SugarColumn(Length = 20)]
        public System.String Nickname { get { return this._Nickname; } set { this._Nickname = value.Trim(); } }

        private System.String _Note;
        /// <summary>
        /// ��ע
        /// </summary>
        public System.String Note { get { return this._Note; } set { this._Note = value.Trim(); } }

        private System.Boolean _Is_active = true;
        /// <summary>
        /// ״̬��0��ע����1���ã�Ĭ��1
        /// </summary>
        public System.Boolean Is_active { get { return this._Is_active; } set { this._Is_active = value; } }

        /// <summary>
        /// ��������
        /// </summary>
        public short Number { get => _number; set => _number = value; }

        private short _number = 0;

    }
}