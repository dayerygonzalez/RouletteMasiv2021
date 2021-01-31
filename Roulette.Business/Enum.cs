using System;
using System.Collections.Generic;

namespace Roulette.Business
{
    public class Enum
    {
        public enum StateGame
        {
            Open = 1,
            Close = 2
        }
        public enum ColorGame
        {
            Black = 1,
            Red = 2
        }
        public enum ResponseCode
        {
            Ok = 100,
            Rejected = 102,
            InternalError = 503,
            ErrorSave = 501,
        }
        public static readonly Dictionary<int, string> StateGameDes = new Dictionary<int, string>()
        {
            {1, "Opened"},
            {2, "Closed"}
        };
        public static readonly Dictionary<ResponseCode, string> ResponseMessages = new Dictionary<ResponseCode, string>()
        {
            { ResponseCode.Ok, "Successful Operation"},
            { ResponseCode.Rejected, "Operation Rejected"},
            { ResponseCode.InternalError, "Internal Error"},
            { ResponseCode.ErrorSave, "Error Save"}
        };

        public enum DocType
        {
            cc = 1, 
            nit = 2, 
            ce = 3, 
            ti = 4, 
            ps = 5, 
            cd = 6, 
            rc = 7, 
        }
    }
}
