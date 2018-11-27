using SqlSugar;

namespace sdglsys.Entity
{
    /// <summary>
    /// ����
    /// </summary>
    public class VRoom
    {
        private System.Int32 _Id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Room_Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32 _Pid;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 Room_Building_id { get { return this._Pid; } set { this._Pid = value; } }

        private System.Int32 _Dorm_id;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 Room_Dorm_id { get { return this._Dorm_id; } set { this._Dorm_id = value; } }

        private System.String _Vid;

        /// <summary>
        ///
        /// </summary>
        public System.String Room_Vid { get { return this._Vid; } set { this._Vid = value.Trim(); } }

        private System.String _Nickname;

        /// <summary>
        ///
        /// </summary>
        public System.String Room_Nickname { get { return this._Nickname; } set { this._Nickname = value.Trim(); } }

        private System.String _Note;

        /// <summary>
        ///
        /// </summary>
        public System.String Room_Note { get { return this._Note; } set { this._Note = value.Trim(); } }

        private System.Boolean _Is_active = true;

        /// <summary>
        ///
        /// </summary>
        public System.Boolean Room_Is_active { get { return this._Is_active; } set { this._Is_active = value; } }

        /// <summary>
        /// ����¥����
        /// </summary>
        public string Room_Building_Nickname
        {
            get; set;
        }

        /// <summary>
        /// ԰������
        /// </summary>
        public string Room_Dorm_Nickname
        {
            get; set;
        }

        /// <summary>
        /// ����
        /// </summary>
        public System.SByte Number { get; set; }
    }
}