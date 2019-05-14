using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public class FileHandler
{     private string root = @"data";
      private string tempName = "test";

      public void saveObject(Object obj,String name)
      {   if(name!=null) tempName = name;
          String dir = Path.Combine(Directory.GetCurrentDirectory(),root);
          if (!Directory.Exists(dir))
          {   Directory.CreateDirectory(dir);
          }

          // auto generate file name
          String filename = Path.Combine(dir,tempName+".data");
          for(int i=0;File.Exists(filename);i++)
          {   filename = Path.Combine(dir,tempName+i+".data");
          }

          IFormatter formatter = new BinaryFormatter();
          Stream stream = new FileStream(filename,FileMode.Create,FileAccess.Write);
          formatter.Serialize(stream, obj);
          stream.Close();
       }

       public Object readObject(string name)
       {    string dir = Path.Combine(Directory.GetCurrentDirectory(),root);
            if (!Directory.Exists(dir))
            {   return null;
            }
            string filename = Path.Combine(dir,name+".data");
            if(!File.Exists(filename)) return null;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename,FileMode.Open,FileAccess.Read);
            Object objnew = formatter.Deserialize(stream);
            stream.Close();
            return objnew;
        }

        public static void Main(string[] s)
        {   BasicPropertys test = new BasicPropertys(0,0,0);
            FileHandler handler = new FileHandler();
            handler.saveObject(test,null);
            BasicPropertys read = (BasicPropertys) handler.readObject("test");
            Debug.Assert(read.Frontend==1);
            Console.WriteLine(read.Frontend==1);
        }

}
