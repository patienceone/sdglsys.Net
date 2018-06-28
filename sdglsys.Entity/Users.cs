using SqlSugar;

namespace sdglsys.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Users
    {
        private System.Int32 _Id;
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _Login_name;
        /// <summary>
        /// ��¼��
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public System.String Login_name { get { return this._Login_name; } set { this._Login_name = value?.Trim(); } }

        private System.String _Nickname;
        /// <summary>
        /// ����
        /// </summary>
        public System.String Nickname { get { return this._Nickname; } set { this._Nickname = value?.Trim(); } }

        private System.String _Pwd;
        /// <summary>
        /// ����
        /// </summary>
        public System.String Pwd { get { return this._Pwd; } set { this._Pwd = value?.Trim(); } }

        private System.String _Note;
        /// <summary>
        /// ��ע
        /// </summary>
        public System.String Note { get { return this._Note; } set { this._Note = value?.Trim(); } }

        private System.String _Phone;
        /// <summary>
        /// 
        /// </summary>
        public System.String Phone { get { return this._Phone; } set { this._Phone = value?.Trim(); } }

        private System.Int32? _Pid;
        /// <summary>
        /// ����԰��
        /// </summary>
        public System.Int32? Pid { get { return this._Pid; } set { this._Pid = value; } }

        private System.Int32? _Role;
        /// <summary>
        /// Ȩ��
        /// </summary>
        public System.Int32? Role { get { return this._Role; } set { this._Role = value; } }

        private System.DateTime? _Reg_date= System.DateTime.Now;
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public System.DateTime? Reg_date { get { return this._Reg_date; } set { this._Reg_date = value; } }

        private System.Boolean? _Is_active = true;
        /// <summary>
        /// ״̬
        /// </summary>
        public System.Boolean? Is_active { get { return this._Is_active; } set { this._Is_active = value; } }
    }
}