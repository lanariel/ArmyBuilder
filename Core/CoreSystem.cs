using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Core
{
    static public class CoreSystem
    {
        static BinaryFormatter binaryFormatter = new BinaryFormatter();
        public static void Save(Army a, string p)
        {
            FileStream fs = new FileStream(p, FileMode.Create);
            binaryFormatter.Serialize(fs, a);
            fs.Flush();
            fs.Close();
        }

        public static Army Load(string p)
        {
            FileStream fs = new FileStream(p, FileMode.Open);
            object o = binaryFormatter.Deserialize(fs);
            if (o is Army)
                return (Army)o;

            throw new FormatException();
        }
    }
}
