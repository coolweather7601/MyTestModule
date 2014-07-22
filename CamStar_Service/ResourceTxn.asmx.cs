using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Camstar.XMLClient.API;
using System.Xml;

namespace CamStar_Service
{
    /// <summary>
    /// Summary description for ResourceTxn
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ResourceTxn : System.Web.Services.WebService
    {
        CamStar_Service.Common.func com;


        [WebMethod(Description = "Equipment set status; equipmentKind(Item = 1, WafterSort = 2, BackGrind = 3, Assembly = 4, Test = 5).(ex. WST001, PRD, DOWN)【Resource Txn】")]
        public XmlNode EquipmentSetStaus(string resource, string resourceStatusCode, string resourceStatusReason, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_EquipmentSetStaus(resource, resourceStatusCode, resourceStatusReason, _equipmentKind);

            if (com.CheckForErrors(ResponseDocument) == "completion")
            {
                resultMsg = string.Format(@"<Code>0</Code>");
            }
            else
            {
                resultMsg = string.Format(@"<Code>-1</Code> 
                                            <Message>{0}</Message>", com.CheckForErrors(ResponseDocument));
            }
            xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>{0}</ResultMessage>", resultMsg));
            return (xmlDocumentObject);
        }

        // input XML format for muti-items? discuss with Automation integration team
        [WebMethod(Description = "Equipment setup; equipmentKind(Item = 1, WafterSort = 2, BackGrind = 3, Assembly = 4, Test = 5)(ex. WST001, 190007001|190007002|190007003)【Resource Txn】")]
        public XmlNode EquipmentSetup(string resource, string tool, string physicalLocation, string physicalPosition, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_EquipmentSetup(resource, tool, physicalLocation, physicalPosition, _equipmentKind);

            if (com.CheckForErrors(ResponseDocument) == "completion")
            {
                resultMsg = string.Format(@"<Code>0</Code>");
            }
            else
            {
                resultMsg = string.Format(@"<Code>-1</Code> 
                                            <Message>{0}</Message>", com.CheckForErrors(ResponseDocument));
            }
            xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>{0}</ResultMessage>", resultMsg));
            return (xmlDocumentObject);
        }

        // input XML format for muti-items? discuss with Automation integration team
        [WebMethod]
        public XmlNode PartControl_InventoryRequest(string resource, string requestOrder)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_PartControl_InventoryRequest(resource, requestOrder);

            if (com.CheckForErrors(ResponseDocument) == "completion")
            {
                resultMsg = string.Format(@"<Code>0</Code>");
            }
            else
            {
                resultMsg = string.Format(@"<ResultMessage>  
                                              <Code>-1</Code> 
                                              <Message>{0}</Message> 
                                            </ResultMessage>", com.CheckForErrors(ResponseDocument));
            }
            xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>{0}</ResultMessage>", resultMsg));
            return (xmlDocumentObject);
        }


        //Txn
        public csiDocument Txn_EquipmentSetStaus(string resource, string resourceStatusCode, string resourceStatusReason, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;

            try
            {
                switch ((int)_equipmentKind)
                {
                    case 1://Item
                        serviceName = "WaferEquipmentSetStatus";
                        break;
                    case 2://WaferSort
                        serviceName = "WaferSortEquipmentSetStatus";
                        break;
                    case 3://BackGrind
                        serviceName = "BackGrindEquipmentSetStatus";
                        break;
                    case 4://Assembly
                        serviceName = "AssemblyEquipmentSetStatus";
                        break;
                    case 5://Test
                        serviceName = "TestEquipmentSetStatus";
                        break;
                }

                //initial
                com = new Common.func(serviceName + "Doc", serviceName);

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.namedObjectField("Resource").dataField("__name").setValue(resource);
                InputData.namedObjectField("ResourceStatusCode").dataField("__name").setValue(resourceStatusCode);
                InputData.namedObjectField("ResourceStatusReason").dataField("__name").setValue(resourceStatusReason);

                //Execute the service.
                com.gService.setExecute();

                //Request the completion message from the XML Application Server.
                com.gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                csiDocument ResponseDocument = com.gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", _equipmentKind.ToString() + "EquipmentSetStatus"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", _equipmentKind.ToString() + "EquipmentSetStatus"), true);
                }
                com.PrintDoc(com.gDocument.asXML(), true);
                com.PrintDoc(ResponseDocument.asXML(), false);

                return ResponseDocument;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public csiDocument Txn_EquipmentSetup(string resource, string tool, string physicalLocation, string physicalPosition, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;

            try
            {
                switch ((int)_equipmentKind)
                {
                    case 1://Item
                        serviceName = "WaferEquipmentSetup";
                        break;
                    case 2://WaferSort
                        serviceName = "WaferSortEquipmentSetup";
                        break;
                    case 3://BackGrind
                        serviceName = "BackGrindEquipmentSetup";
                        break;
                    case 4://Assembly
                        serviceName = "AssemblyEquipmentSetup";
                        break;
                    case 5://Test
                        serviceName = "TestEquipmentSetup";
                        break;
                }

                //initial
                com = new Common.func(serviceName + "Doc", serviceName);

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.namedObjectField("Resource").dataField("__name").setValue(resource);
                InputData.namedObjectField("PhysicalLocation").dataField("__name").setValue(physicalLocation);
                InputData.namedObjectField("PhysicalPosition").dataField("__name").setValue(physicalPosition);


                string[] tools = tool.Split('|');
                foreach (string t in tools)
                {
                    InputData.containerList("Tools").appendItem(t, "");
                }

                //Execute the service.
                com.gService.setExecute();

                //Request the completion message from the XML Application Server.
                com.gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                csiDocument ResponseDocument = com.gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", _equipmentKind.ToString() + "EquipmentSetup"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", _equipmentKind.ToString() + "EquipmentSetup"), true);
                }
                com.PrintDoc(com.gDocument.asXML(), true);
                com.PrintDoc(ResponseDocument.asXML(), false);

                return ResponseDocument;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public csiDocument Txn_PartControl_InventoryRequest(string resource, string requestOrder)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = "PartRequestAssign";

            try
            {
                //initial
                com = new Common.func(serviceName + "Doc", serviceName);

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.namedObjectField("Resource").dataField("__name").setValue(resource);//ex. AW-197
                InputData.dataField("AutoComplete").setValue("false");
                InputData.namedObjectField("JobOrder").dataField("__name").setValue("");
                InputData.namedObjectField("RequestOrder").dataField("__name").setValue(requestOrder);//ex. 2014 180-0000000012
                InputData.dataField("RequestType").setValue("1");
                InputData.dataField("RequireAcknowledgeEmail").setValue("false");

                //list item 1
                csiContainerList csiList = InputData.containerList("ServiceDetails");
                csiContainer csiCon = csiList.appendItem("", "");
                csiCon.dataField("PartName").setValue("XXX");
                csiCon.dataField("PartQty").setValue("5");
                csiCon.dataField("RequestPartQty").setValue("3");
                csiNamedObject csiSpec = csiCon.namedObjectField("MaterialPart");
                csiSpec.dataField("__name").setValue("壓板");
                csiSpec.dataField("__rev").setValue("1");
                csiSpec.dataField("__useROR").setValue("false");

                //list item 2
                csiContainer csiCon_2 = csiList.appendItem("", "");
                csiCon_2.dataField("PartName").setValue("YYY");
                csiCon_2.dataField("PartQty").setValue("6");
                csiCon_2.dataField("RequestPartQty").setValue("1");
                csiNamedObject csiSpec_2 = csiCon_2.namedObjectField("MaterialPart");
                csiSpec_2.dataField("__name").setValue("爐面");
                csiSpec_2.dataField("__rev").setValue("1");
                csiSpec_2.dataField("__useROR").setValue("false");

                //Execute the service.
                com.gService.setExecute();

                //Request the completion message from the XML Application Server.
                com.gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                csiDocument ResponseDocument = com.gDocument.submit();


                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "PartRequestAssign"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "PartRequestAssign"), true);
                }
                com.PrintDoc(com.gDocument.asXML(), true);
                com.PrintDoc(ResponseDocument.asXML(), false);

                return ResponseDocument;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
