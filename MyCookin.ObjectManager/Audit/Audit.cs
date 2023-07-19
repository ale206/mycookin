using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyCookin.Common;
using MyCookin.DAL.Audit.ds_AuditTableAdapters;

namespace MyCookin.ObjectManager.AuditManager
{
    public class Audit
    {
        #region PrivateFields

        private Guid _IDAuditEvent;
        private string _AuditEventMessage;
        private Guid _ObjectID;
        private string _ObjectType;
        private string _ObjectTxtInfo;
        private AuditEventLevel _AuditEventLevel;
        private DateTime _EventInsertedOn;
        private DateTime? _EventUpdatedOn;
        private Guid? _IDEventUpdatedBy;
        private bool _AuditEventIsOpen;

        private int _NumberOfResults;
        private string _ExecutionError;
        private int _NumberOfEveniences;
        #endregion

        #region PublicProperties

        public Guid IDAuditEvent
        {
            get { return _IDAuditEvent; }
        }
        public string AuditEventMessage
        {
            get { return _AuditEventMessage; }
            set { _AuditEventMessage = value; }
        }
        public Guid ObjectID
        {
            get { return _ObjectID; }
            set { _ObjectID = value; }
        }
        public string ObjectType
        {
            get { return _ObjectType; }
            set { _ObjectType = value; }
        }
        public string ObjectTxtInfo
        {
            get { return _ObjectTxtInfo; }
            set { _ObjectTxtInfo = value; }
        }
        public AuditEventLevel AuditEventLevel
        {
            get { return _AuditEventLevel; }
            set { _AuditEventLevel = value; }
        }
        public DateTime EventInsertedOn
        {
            get { return _EventInsertedOn; }
            set { _EventInsertedOn = value; }
        }
        public DateTime? EventUpdatedOn
        {
            get { return _EventUpdatedOn; }
            set { _EventUpdatedOn = value; }
        }
        public Guid? IDEventUpdatedBy
        {
            get { return _IDEventUpdatedBy; }
            set { _IDEventUpdatedBy = value; }
        }
        public bool AuditEventIsOpen
        {
            get { return _AuditEventIsOpen; }
            set { _AuditEventIsOpen = value; }
        }

        public string ExecutionError
        {
            get { return _ExecutionError; }
            set { _ExecutionError = value; }
        }

        public int NumberOfResults
        {
            get { return _NumberOfResults; }
            set { _NumberOfResults = value; }
        }
        
        public int NumberOfEveniences
        {
            get { return _NumberOfEveniences; }
            set { _NumberOfEveniences = value; }
        }
        #endregion

        #region Costructors

        public Audit()
        {
        }

        /// <summary>
        /// Constructor to get all audits to check
        /// </summary>
        /// <param name="ObjType"></param>
        public Audit(ObjectType ObjType, int NumberOfResults)
        {
            _ObjectType = ObjType.ToString();
            _NumberOfResults = NumberOfResults;
        }

        /// <summary>
        /// Constructor to get number of evenience
        /// </summary>
        /// <param name="ObjType"></param>
        public Audit(ObjectType ObjType)
        {
            _ObjectType = ObjType.ToString();
        }

        /// <summary>
        /// Constructor for writing in DB Audit
        /// </summary>
        /// <param name="EventMessage">Message of the event</param>
        /// <param name="IDObj">Guid of the object</param>
        /// <param name="ObjType">Object Type - From ObjectManager.cs</param>
        /// <param name="ObjTxtInfo">Additional Informations</param>
        /// <param name="EventLevel">Event Level - Enumeration in Audit.cs</param>
        /// <param name="InsertedOn">Date of Today</param>
        public Audit(string EventMessage, Guid IDObj, ObjectType ObjType, string ObjTxtInfo, AuditEventLevel EventLevel, DateTime InsertedOn)
        {
            _AuditEventMessage = EventMessage;
            _ObjectID = IDObj;
            _ObjectType = ObjType.ToString();
            _ObjectTxtInfo = ObjTxtInfo;
            _AuditEventLevel = EventLevel;
            _EventInsertedOn = InsertedOn;
        }

        public Audit(Guid IDEvent)
        {
            _IDAuditEvent = IDEvent;
            QueryAuditEventInfo();
        }

        /// <summary>
        /// To Delete rows by object ID
        /// </summary>
        /// <param name="ObjectID"></param>
        public Audit(Guid ObjectID, bool OptionalField)
        {
            _ObjectID = ObjectID;
        }
        

        /// <summary>
        /// To Update an Audit Event
        /// </summary>
        /// <param name="IDAuditEvent"></param>
        /// <param name="IDEventUpdatedBy"></param>
        /// <param name="AuditEventIsOpen"></param>
        public Audit(Guid IDAuditEvent, Guid IDEventUpdatedBy, bool AuditEventIsOpen)
        {
            _IDAuditEvent = IDAuditEvent;
            _IDEventUpdatedBy = IDEventUpdatedBy;
            _AuditEventIsOpen = AuditEventIsOpen;
        }
        #endregion

        #region Methods

        #region QueryAuditEventInfo
        private void QueryAuditEventInfo()
        {
            GetAuditEventDAL auditEventDAL = new GetAuditEventDAL();
            DataTable dtAuditEvent = auditEventDAL.GetAuditEventByID(_IDAuditEvent);

            if (dtAuditEvent.Rows.Count > 0)
            {
                _AuditEventMessage = dtAuditEvent.Rows[0].Field<string>("AuditEventMessage");
                _ObjectID = dtAuditEvent.Rows[0].Field<Guid>("ObjectID");
                _ObjectType = dtAuditEvent.Rows[0].Field<string>("ObjectType");
                _ObjectTxtInfo = dtAuditEvent.Rows[0].Field<string>("ObjectTxtInfo");
                _AuditEventLevel = (AuditEventLevel)dtAuditEvent.Rows[0].Field<int>("AuditEventLevel");
                _EventInsertedOn = dtAuditEvent.Rows[0].Field<DateTime>("EventInsertedOn");
                _EventUpdatedOn = dtAuditEvent.Rows[0].Field<DateTime?>("EventUpdatedOn");
                _IDEventUpdatedBy = dtAuditEvent.Rows[0].Field<Guid?>("IDEventUpdatedBy");
                _AuditEventIsOpen = dtAuditEvent.Rows[0].Field<bool>("AuditEventIsOpen");
            }

        }
        #endregion

        #region AddEvent
        /// <summary>
        /// Insert Event to DB Audit
        /// </summary>
        /// <returns></returns>
        public ManageUSPReturnValue AddEvent()
        {
            
            ManageAuditEventDAL _auditEvent = new ManageAuditEventDAL();
            ManageUSPReturnValue _return;
            _return = new ManageUSPReturnValue(_auditEvent.USP_AddAuditEvent(_AuditEventMessage, _ObjectID,
                                                        _ObjectType, _ObjectTxtInfo, Convert.ToInt32(_AuditEventLevel), _EventInsertedOn, null, null, true));
            _IDAuditEvent = new Guid(_return.USPReturnValue);
            return _return;
        }
        #endregion

        #region UpdateEvent
        public ManageUSPReturnValue UpdateEvent()
        {
            ManageAuditEventDAL _auditEvent = new ManageAuditEventDAL();
            ManageUSPReturnValue _return;
            _return = new ManageUSPReturnValue(_auditEvent.USP_UpdateAuditEvent(_IDAuditEvent, DateTime.UtcNow, _IDEventUpdatedBy, _AuditEventIsOpen));
            return _return;
        }
        #endregion

        #region DeleteByObjectID
        public string DeleteByObjectID()
        {
            string ExecutionResult = "";

            try
            {
                GetAuditEventDAL taAuditEvent = new GetAuditEventDAL();
                taAuditEvent.DeleteByObjectID(_ObjectID);
            }
            catch (Exception ex)
            {
                ExecutionResult = ex.Message;
            }

            return ExecutionResult;
        }
        #endregion


        #region
        public int NumberOfEventsToCheck()
        {
            int number = 0;

            try
            {
                GetAuditEventDAL auditEventDAL = new GetAuditEventDAL();
                number = Convert.ToInt32(auditEventDAL.GetNumberOfEventsToCheck(_ObjectType));
            }
            catch
            { 
                
            }

            return number;

        }
        #endregion

        #region GetAuditEventToCheck
        public List<Audit> GetAuditEventToCheck()
        {
            GetAuditEventDAL auditEventDAL = new GetAuditEventDAL();
            DataTable dtAuditEvent = auditEventDAL.GetAuditEventsToCheck(_NumberOfResults, _ObjectType);

            List<Audit> AuditList = new List<Audit>();

            try
            {
                if (dtAuditEvent.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAuditEvent.Rows.Count; i++)
                    {
                        AuditList.Add(
                            new Audit()
                            {
                                _IDAuditEvent = dtAuditEvent.Rows[i].Field<Guid>("IDAuditEvent"),
                                _AuditEventMessage = dtAuditEvent.Rows[i].Field<string>("AuditEventMessage"),
                                _ObjectID = dtAuditEvent.Rows[i].Field<Guid>("ObjectID"),
                                _ObjectType = dtAuditEvent.Rows[i].Field<string>("ObjectType"),
                                _ObjectTxtInfo = dtAuditEvent.Rows[i].Field<string>("ObjectTxtInfo"),
                                _AuditEventLevel = (AuditEventLevel)dtAuditEvent.Rows[i].Field<int>("AuditEventLevel"),
                                _EventInsertedOn = dtAuditEvent.Rows[i].Field<DateTime>("EventInsertedOn"),
                                _EventUpdatedOn = dtAuditEvent.Rows[i].Field<DateTime?>("EventUpdatedOn"),                               
                                _IDEventUpdatedBy = dtAuditEvent.Rows[i].Field<Guid?>("IDEventUpdatedBy"),
                                _AuditEventIsOpen = dtAuditEvent.Rows[i].Field<bool>("AuditEventIsOpen"),
                                _ExecutionError = ""
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                AuditList.Add(
                    new Audit()
                    {
                        _ExecutionError = ex.Message
                    });
            }

            return AuditList;
        }
        #endregion

        #region GetNumberOfEveniences
        public List<Audit> GetNumberOfEveniences()
        {
            GetAuditEventDAL auditEventDAL = new GetAuditEventDAL();
            DataTable dtAuditEvent = auditEventDAL.GetObjectIDNumberOfEveniences(_ObjectType);

            List<Audit> AuditList = new List<Audit>();

            try
            {
                if (dtAuditEvent.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAuditEvent.Rows.Count; i++)
                    {
                        AuditList.Add(
                            new Audit()
                            {
                                _ObjectID = dtAuditEvent.Rows[i].Field<Guid>("ObjectID"),
                                _NumberOfEveniences = dtAuditEvent.Rows[i].Field<int>("NumberOfEveniences"),
                                _ExecutionError = ""
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                AuditList.Add(
                    new Audit()
                    {
                        _ExecutionError = ex.Message
                    });
            }

            return AuditList;
        }
        #endregion

        #endregion
    }

    public class AutoAuditConfig
    {
        #region PrivateFields
        
        private int _IDAutoAuditConfig;
        private string _ObjectType;
        private AuditEventLevel _AuditEventLevel;
        private bool _EnableAutoAudit;

        #endregion

        #region PublicProperties

        public int IDAutoAuditConfig
        {
            get { return _IDAutoAuditConfig; }
        }
        public string ObjectType
        {
            get { return _ObjectType; }
            set { _ObjectType = value; }
        }
        public AuditEventLevel AuditEventLevel
        {
            get { return _AuditEventLevel; }
            set { _AuditEventLevel = value; }
        }
        public bool EnableAutoAudit
        {
            get { return _EnableAutoAudit; }
            set { _EnableAutoAudit = value; }
        }

        #endregion

        #region Costructors

        public AutoAuditConfig(ObjectType objectType)
        {
            _ObjectType = objectType.ToString();
            QueryAutoAuditConfigInfo();
        }

        #endregion

        #region Methods

        private void QueryAutoAuditConfigInfo()
        {
            GetAutoAuditConfigDAL autoAuditConfigDAL = new GetAutoAuditConfigDAL();
            DataTable dtAutoAuditConfigDAL = autoAuditConfigDAL.GetAutoAuditConfigByObjectType(_ObjectType);

            if (dtAutoAuditConfigDAL.Rows.Count > 0)
            {
                _IDAutoAuditConfig = dtAutoAuditConfigDAL.Rows[0].Field<int>("IDAutoAuditConfig");
                _AuditEventLevel = (AuditEventLevel)dtAutoAuditConfigDAL.Rows[0].Field<int>("AuditEventLevel");
                _EnableAutoAudit = dtAutoAuditConfigDAL.Rows[0].Field<bool>("EnableAutoAudit");
            }
        }

        #endregion
    }

    public enum AuditEventLevel:int
    {
        Hight=1,
        Medium=2,
        Low=3
    }
}
