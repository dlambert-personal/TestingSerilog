using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAIMlib
{
    public class FaimDelim
    {
        private FaimDelim(string value) { Value = value; }
        public string Value { get; private set; }

        public static FaimDelim API { get { return new FaimDelim("YFT811  "); } }
        public static FaimDelim File { get { return new FaimDelim("XFT811  "); } }

    }
    
    public enum Severity 
    { 
        information,
        warning,
        error
    }
    //public class Message
    //{
    //    public int Number;
    //    public Severity Severity;
    //    public string Message = "";
    //    public string Description = "";
    //}

    //public WarnBadHeader = 
 
    internal class FaimEnums
    {
    }
}
