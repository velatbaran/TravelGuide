﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelGuide.Entities.Messages;

namespace TravelGuide.BL.Results
{
    public class BLResults<T> where T : class
    {
        public List<ErrorMessageObj> Errors { get; set; }
        public T Result { get; set; }

        public BLResults()
        {
            Errors = new List<ErrorMessageObj>();
        }

        public void AddError(ErrorMessageCode code,string message)
        {
            Errors.Add(new ErrorMessageObj() { Code = code, Message = message });
        }
    }
}
