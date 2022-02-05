using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelGuide.DAL.Common;
using TravelGuide.Entities.Entity;
using TravelGuide.UI.Models;

namespace TravelGuide.UI.Init
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            Member member = CurrentSession.User;
            if (member == null)
            {
                return "system";
            }
            else
            {
                return member.Username;
            }
        }
    }
}