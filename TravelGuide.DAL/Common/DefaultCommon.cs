using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelGuide.DAL.Common
{
    public class DefaultCommon : ICommon
    {
        public string GetUsername()
        {
            return "system";
        }
    }
}
