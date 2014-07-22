using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.ServiceModel.Web;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface IService2
    {
        [OperationContract, XmlSerializerFormat]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "xml/{id}")]
        XmlNode TestXmlObj();

        [OperationContract]
        string TestHelloWorld();

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "/Add?num1={a}&num2={b}")]
        WcfService1.Service2.Addition AddNumbers(string a, string b);



        [OperationContract]
        [WebInvoke(Method = "Post", UriTemplate = "XMLTest")]
        string TestXmlObjToString();

    }
}
