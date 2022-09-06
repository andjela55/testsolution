using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DictionaryService
    {
        private Dictionary<string, string> pathMap;
        public DictionaryService()
        {
            pathMap = new Dictionary<string, string>();
        }
        public void AddElementToDictionary(string key, string value)
        {
            if (!pathMap.ContainsKey(key))
            {
                pathMap.Add(key, value);

            }
            else
            {
                pathMap[key] = value;
            }
        }
        public string GetPath(string key)
        {
            return pathMap[key];
        }
    }
}
