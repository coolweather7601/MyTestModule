using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    public class Service2 : IService2
    {
        public XmlNode TestXmlObj()
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                          <Code>-1</Code> 
                                                          <Message>2</Message> 
                                                      </ResultMessage>"));
            return (xmlDocumentObject);
        }

        public string TestHelloWorld()
        {
            return "Hello World!";
        }


        public Addition AddNumbers(string a, string b)
        {
            Addition objadd = new Addition();
            objadd.num1 = a;
            objadd.num2 = b;
            objadd.num3 = (Convert.ToInt32(a) + Convert.ToInt32(b)).ToString();
            return objadd;
        }

        [DataContract(Name = "Add")]
        public class Addition
        {
            private string _num1;
            private string _num2;
            private string _num3;

            [DataMember(Name = "FirstNumber", Order = 1)]
            public string num1
            {
                set { _num1 = value; }
                get { return _num1; }
            }
            [DataMember(Name = "SecondNumber", Order = 2)]
            public string num2
            {
                set { _num2 = value; }
                get { return _num2; }
            }
            [DataMember(Name = "Result", Order = 3)]
            public string num3
            {
                set { _num3 = value; }
                get { return _num3; }
            }
        }

        public string TestXmlObjToString()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml("<c><a>A</a> <b>B</b></c>");
            return xDoc.OuterXml;
        }


    }
}
