using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TajikSpeechRecognition.Core;

namespace TajikSpeechRecognition.UI.Services
{
    public class LM
    {

        public static void Post()
        {
            HttpWebRequest requestToServerEndpoint =
            (HttpWebRequest)WebRequest.Create("http://www.speech.cs.cmu.edu/cgi-bin/tools/lmtool/run");

            string boundaryString = "----SomeRandomText";
            string fileUrl = $"{AppManager.TempDir}/tajik.txt";

            requestToServerEndpoint.Method = WebRequestMethods.Http.Post;
            requestToServerEndpoint.ContentType = "multipart/form-data; boundary=" + boundaryString;
            requestToServerEndpoint.KeepAlive = true;
            requestToServerEndpoint.Credentials = System.Net.CredentialCache.DefaultCredentials;

            MemoryStream postDataStream = new MemoryStream();
            StreamWriter postDataWriter = new StreamWriter(postDataStream);

            postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
            postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}",
            "myFileDescription",
            "A sample file description");

            postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
            postDataWriter.Write("Content-Disposition: form-data;"
            + "name=\"{0}\";"
            + "filename=\"{1}\""
            + "\r\nContent-Type: {2}\r\n\r\n",
            "myFile",
            Path.GetFileName(fileUrl),
            Path.GetExtension(fileUrl));
            postDataWriter.Flush();

            FileStream fileStream = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                postDataStream.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            postDataWriter.Write("\r\n--" + boundaryString + "--\r\n");
            postDataWriter.Flush();

            requestToServerEndpoint.ContentLength = postDataStream.Length;

            using (Stream s = requestToServerEndpoint.GetRequestStream())
            {
                postDataStream.WriteTo(s);
            }
            var resp = requestToServerEndpoint.GetResponse();
            postDataStream.Close();
        }

    }
}
