using SqlSugar;

namespace sdglsys.Entity
{
    [SugarTable("t_building")]
    /// <summary>
    /// ����¥
    /// </summary>
    public class TBuilding
    {
        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32 _Pid;
        /// <summary>
        /// ԰��ID
        /// </summary>
        public System.Int32 Pid { get { return this._Pid; } set { this._Pid = value; } }

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
        /// ״̬,1����,0��ע��
        /// </summary>
        public System.Boolean Is_active { get { return this._Is_active; } set { this._Is_active = value; } }
    }
}