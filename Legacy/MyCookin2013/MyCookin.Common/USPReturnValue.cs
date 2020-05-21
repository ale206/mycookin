using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyCookin.Common
{
   public class ManageUSPReturnValue
    {
        private string _ResultExecutionCode;
        private string _USPReturnValue;
        private bool _isError;

        public string ResultExecutionCode
        {
            get { return _ResultExecutionCode; }
        }

        public string USPReturnValue
        {
            get { return _USPReturnValue; }
        }

        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Returns Stored Procedure Results
        /// </summary>
        /// <param name="ResultExecutionCode">Error or Message Code such as "xx-xx-0000"</param>
        /// <param name="USPReturnValue">...</param>
        /// <param name="IsError">If an error is generated</param>
        public ManageUSPReturnValue(string ResultExecutionCode, string USPReturnValue, bool IsError)
        {
            _ResultExecutionCode = ResultExecutionCode;
            _USPReturnValue = USPReturnValue;
            _isError = IsError;
        }
        
        /// <summary>
        /// Returns Stored Procedure Results
        /// </summary>
        /// <param name="USPResult"></param>
        public ManageUSPReturnValue(DataTable USPResult)
        {
            _ResultExecutionCode = USPResult.Rows[0].Field<string>("ResultExecutionCode");
            _USPReturnValue = USPResult.Rows[0].Field<string>("USPReturnValue");
            _isError = USPResult.Rows[0].Field<bool>("isError");
        }
      
    }
}
