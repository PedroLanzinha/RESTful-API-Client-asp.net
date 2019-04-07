using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace restClient
{
    public enum httpVerb // httpverb = http request method in restful services
    {
    GET,    //The GET method requests a representation of the specified resource. Requests using GET should only retrieve data.
    POST,   //The POST method is used to submit an entity to the specified resource, often causing a change in state or side effects on the server.
    PUT,    //The PUT method replaces all current representations of the target resource with the request payload.
    DELETE  //The DELETE method deletes the specified resource.
    }

    class RestClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }

        public RestClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }

        public string makeRequest()
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("error code: " + response.StatusCode.ToString());
;                }
                // process the response stream (could be JSON, XML or HTML or others)

            using (Stream responseStream = response.GetResponseStream())
                {
                    if(responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }// end of streamReader
                    }
                }//end of using response stream

            }// end of using response

            return strResponseValue;
        }
    }
}
