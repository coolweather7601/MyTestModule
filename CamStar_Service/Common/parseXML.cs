using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace CamStar_Service.Common
{
    public class parseXML
    {
        public XmlDocument xmlDoc;
        public XmlNodeList getSelectNodesList(string xmlObj, string selectDes)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlObj);

            XmlNodeList objNodeList = xmlDoc.SelectNodes(selectDes);
            return objNodeList;
        }

        public XmlNodeList getSelectChildNodesList(string xmlObj, string selectDes)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlObj);

            XmlNodeList objNodeList = xmlDoc.SelectSingleNode(selectDes).ChildNodes;
            return objNodeList;
        }

        public XmlNode getSelectNode(string xmlObj, string selectDes)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlObj);

            XmlNode objNode = xmlDoc.SelectSingleNode(selectDes);
            return objNode;
        }


        #region test parse XML
        public class CarCost
        {
            public CarCost(string id, string uptime, string downtime, float price)
            {
                this.ID = id;
                this.UpTime = uptime;
                this.DownTime = downtime;
                this.Price = price;
            }
            public string ID { get; set; }
            public string UpTime { get; set; }
            public string DownTime { get; set; }
            public float Price { get; set; }
        }

        public void test() //getSelectNodesList & getSelectNode
        {
            
            string testStr = string.Format(@"<doc>
                                                <name>1</name>
                                                <address>2</address>
                                                <phone>3</phone>
                                                <id>4</id>
                                            </doc>");

            string testStr2 = string.Format(@"<doc>
                                                <a>
                                                    <people>
                                                        <name>1</name>
                                                        <address>2</address>
                                                        <phone>3</phone>
                                                        <id>4</id>
                                                    </people>
                                                    <people>
                                                        <name>11</name>
                                                        <address>22</address>
                                                        <phone>33</phone>
                                                        <id>44</id>
                                                    </people>
                                                </a>
                                              </doc>");


            Common.parseXML par = new Common.parseXML();
            XmlNode node = par.getSelectNode(testStr, "/doc/name");
            string name = node.InnerText;

            XmlNodeList nodeList = par.getSelectNodesList(testStr2, "/doc/a/people");
            foreach (XmlNode n in nodeList)
            {
                String StrNodeName = n.Name.ToString();
                foreach (XmlAttribute Attr in n.Attributes)
                {
                    String StrAttr = Attr.Name.ToString();
                    String StrValue = n.Attributes[Attr.Name.ToString()].Value;
                    String StrInnerText = n.InnerText;
                }
            }
           
        }
        public void test2()//getSelectChildNodesList
        {
            string testStr = string.Format(@"<Car>
                                              <carcost>
                                                <ID>20130821133126</ID>
                                                <uptime>60</uptime>
                                                <downtime>30</downtime>
                                                <price>0.4</price>
                                              </carcost>
                                              <carcost>
                                                <ID>20130821014316</ID>
                                                <uptime>120</uptime>
                                                <downtime>60</downtime>
                                                <price>0.3</price>
                                              </carcost>
                                              <carcost>
                                                <ID>20130822043127</ID>
                                                <uptime>30</uptime>
                                                <downtime>0</downtime>
                                                <price>0.5</price>
                                              </carcost>
                                              <carcost>
                                                <ID>20130822043341</ID>
                                                <uptime>120以上！</uptime>
                                                <downtime>120</downtime>
                                                <price>0.2</price>
                                              </carcost>
                                            </Car>");
            Common.parseXML par = new Common.parseXML();
            XmlNodeList xmlNodeList = par.getSelectChildNodesList(testStr, "Car");
            foreach (XmlNode list in xmlNodeList)
            {
                CarCost carcost = new CarCost
                (
                    list.SelectSingleNode("ID").InnerText,
                    list.SelectSingleNode("uptime").InnerText,
                    list.SelectSingleNode("downtime").InnerText,
                    float.Parse(list.SelectSingleNode("price").InnerText)
                );
            }
        }
        #endregion
    }
}