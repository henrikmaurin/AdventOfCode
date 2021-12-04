using Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayBase
    {
        protected AOCGetInput input;
        public DayBase()
        {
            string cookie = File.ReadAllText("AOCCookie.txt");
            input=new AOCGetInput(cookie);
        }
        
    }
}
