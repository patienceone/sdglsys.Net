using SqlSugar;

namespace sdglsys.Entity
{
    /// <summary>
    /// ϵͳ��ɫ��
    /// </summary>
    public class VUser
    {
        private System.Int32 _Id;
        /// <summary>
        /// ID
        /// </summary>
        public System.Int32 User_Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _Login_name;
        /// <summary>
        /// ��¼��
        /// </summary>
        public System.String User_Login_name { get { return this._Login_name; } set { this._Login_name = value.Trim(); } }

        private System.String _Nickname;
        /// <summary>
        /// ����
        /// </summary>
        public System.String User_Nickname { get { return this._Nickname; } set { this._Nickname = value.Trim(); } }

        private System.String _Pwd;
        /// <summary>
        /// ����
        /// </summary>
        public System.String User_Pwd { get { return this._Pwd; } set { this._Pwd = value.Trim(); } }

        private System.String _Note;
        /// <summary>
        /// ��ע
        /// </summary>
        public System.String User_Note { get { return this._Note; } set { this._Note = value.Trim(); } }

        private System.String _Phone;
        /// <summary>
        /// ��ϵ��ʽ
        /// </summary>
        public System.String User_Phone { get { return this._Phone; } set { this._Phone = value.Trim(); } }

        private System.Int32 _Pid;
        /// <summary>
        /// ����԰��ID
        /// </summary>
        public System.Int32 User_Dorm_id { get { return this._Pid; } set { this._Pid = value; } }
        /// <summary>
        /// ԰������
        /// </summary>
        public System.String User_Dorm_Nickname { get; set; }

        private System.Int32 _Role;
        /// <summary>
        /// Ȩ�ޣ�1�����Ǽ�Ա��2���ᣨ԰��������Ա��3ϵͳ����Ա
        /// </summary>
        public System.Int32 User_Role { get { return this._Role; } set { this._Role = value; } }
        /// <summary>
        /// Ȩ�ޣ�1�����Ǽ�Ա��2���ᣨ԰��������Ա��3ϵͳ����Ա
        /// </summary>
        public System.String User_RoleName { get {
                return User_Role == 3 ? "ϵͳ����Ա" : User_Role == 2 ? "԰������Ա" : "�����Ǽ�Ա";
            } set {

            } }

        private System.DateTime _Reg_date= System.DateTime.Now;
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public System.DateTime User_Reg_date { get { return this._Reg_date; } set { this._Reg_date = value; } }

        private System.Boolean _Is_active = true;
        /// <summary>
        /// ״̬
        /// </summary>
        public System.Boolean User_Is_active { get { return this._Is_active; } set { this._Is_active = value; } }
    }
}