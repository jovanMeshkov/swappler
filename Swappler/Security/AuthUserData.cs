using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Security
{
    public class AuthUserData
    {
        public string SessionId { get; set; }
        public long UserId { get; set; }

        public AuthUserData() { }

        public AuthUserData(string sessionId, long userId)
        {
            SessionId = sessionId;
            UserId = userId;
        }

        public bool Import(string csvData)
        {
            try
            {
                string[] csvDataSplit = csvData.Split(',');

                SessionId = csvDataSplit[0];

                UserId = long.Parse(csvDataSplit[1]);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string Export()
        {
            return SessionId + "," + UserId;
        }
    }
}