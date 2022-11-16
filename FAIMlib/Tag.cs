using FAIMlib.tags;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAIMlib
{
    public class Tag
    {
        public string TagType { get; set; }
        public string TagBody { get; set; }

        public object ParsedTag()
        {

            object ret = null;
            if (this.TagType == "1100")
            {
                var engine = new FileHelperEngine<tag1100>();
                var parsed = engine.ReadString(this.TagBody);
                if (parsed.Length > 0)
                {
                    ret = parsed[0];
                }
            }
            return ret;
        }
    }
}
