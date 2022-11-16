using FileHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FAIMlib
{

    public class FaimRec
    {
        private ILogger _logger;

        public string? Header;
        public string ParseStatus;
        public List<Tag> Tags = new List<Tag>();

        public FaimRec(ILogger logger)
        {
            _logger = logger;
        }

        public void PopulateFromRawFaim(string rawRec)
        {
            string rec = string.Empty;
            if (rawRec.StartsWith(FaimDelim.File.Value))
            {
                this.Header = FaimDelim.File.Value;
                rec = rawRec.Substring(this.Header.Length);
            }
            if (rawRec.StartsWith(FaimDelim.API.Value))
            {
                this.Header = FaimDelim.API.Value;
                rec = rawRec.Substring(this.Header.Length);
            }
            if (this.Header == null)
            {
                _logger.Warning("No header found.");
                this.ParseStatus = "No header found.";
                return;
            }
            _logger.Information("FAIM header: " + this.Header);
            //parse Tags
            do {
                var ret = GetNextTag(rec);
                this.Tags.Add(ret.tag);
                _logger.Information("Tag added: " + ret.tag.TagType);
                var parsed = ret.tag.ParsedTag();
                rec = ret.remainder;
            } 
            while (rec != string.Empty);

            return;
        }


        private static (Tag? tag, string remainder) GetNextTag(string rec)
        {
            Tag t = new Tag();

            //int pos = 0;
            const char openTag = '{';
            const char closeTag = '}';

            int tagOpenPos = rec.IndexOf(openTag);
            {
                if (tagOpenPos != -1)
                {
                    int tagEndPos = rec.IndexOf(closeTag, tagOpenPos + 1);
                    string tag = rec.Substring(tagOpenPos+1, tagEndPos-1);

                    string rest = rec.Substring(tagEndPos + 1);
                    int nextTagPos = rest.IndexOf(openTag);
                    if (nextTagPos != -1)
                    {
                        rest = rest.Substring(0, nextTagPos);
                    }
                    //recs.Add(rec);
                    t.TagType = tag;
                    t.TagBody = rest;
                    //rest = 
                    int nextOpenTagPos = rec.IndexOf(openTag, tagEndPos + 1);
                    rec = nextOpenTagPos > 0 ? rec.Substring(nextOpenTagPos): string.Empty;
                }
            }

            return (t, rec);
        }
    }
}
