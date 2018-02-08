using FCK.Studio.Dal;

namespace FCK.Studio.Core
{
    public class FCKBase
    {
        public DataBaseContent db = new DataBaseContent();
        public DataBaseReadContent dbr = new DataBaseReadContent();
        /// <summary>
        /// 租户ID
        /// </summary>
        public int RegisterID = Common.Utility.cInt(Common.Utility.GetSessionValue("RegisterId"));

        public FCKBase()
        {
            
        }

        
    }
}
