using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAIMlib
{
    /// <summary>
    /// Designe to read files with one or more FAIM records.
    /// </summary>
    public class FileReader
    {
        private ILogger _logger;
        private List<string> fileMetaData;
        private List<string> rawRecs;
        public List<FaimRec> FaimRecs; 

        //public FileReader()
        //{ }
        public FileReader(ILogger logger)
        {
            _logger = logger;
            fileMetaData = new List<string>();
        }
        public void ParseRawRecs(string filepath)
        {
            if (!File.Exists(filepath))
            {
                string err = String.Format("No file found at [{0}]", filepath);
                _logger.Error(err);
                throw new FileNotFoundException(err);
            }

            fileMetaData.Clear();
            var text = ReadFile(filepath).Result;
            var len = text.Length;
            _logger.Information("File read at " + filepath);
            fileMetaData.Add("File read at " + filepath);

            string delim = "";
            if (text.StartsWith(FaimDelim.File.Value))
            {
                delim = FaimDelim.File.Value;
            }
            if (text.StartsWith(FaimDelim.API.Value))
            {
                delim = FaimDelim.API.Value;
            }
            // if delim is still space, throw error
            _logger.Information("Using delim " + delim);
            fileMetaData.Add("Using delim " + delim);

            rawRecs = new List<string>();
            int pos = 0;
            do
            {
                int nextpos = text.IndexOf(delim, pos + 1);
                {
                    if (nextpos != -1)
                    {
                        string rec = text.Substring(pos, nextpos - pos);
                        rawRecs.Add(rec);
                        pos = nextpos;
                    }
                    else { pos = len; };
                };
            } while (pos < len);

            FaimRecs = new List<FaimRec>();
            _logger.Information("Records read: " + rawRecs.Count().ToString());
            foreach(var rec in rawRecs)
            {
                var f = new FaimRec(_logger);
                f.PopulateFromRawFaim(rec);
                FaimRecs.Add(f);
            }
        }

        private async Task<string> ReadFile(string filepath)
        {
            string result = "";
            try
            {
                using (var sr = new StreamReader(filepath))
                {
                    result = await sr.ReadToEndAsync();
                }
            }
            catch (FileNotFoundException ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
