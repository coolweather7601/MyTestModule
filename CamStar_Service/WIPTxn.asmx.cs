using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using Camstar.XMLClient.API;

namespace CamStar_Service
{
    /// <summary>
    /// Summary description for WIPTxn
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WIPTxn : System.Web.Services.WebService
    {
        CamStar_Service.Common.func com;

        // input XML format for muti-items? discuss with Automation integration team
        [WebMethod(Description = "Reject Lot in Process. waferScribeNumber for Wafersort or NULL waferScribeNumber for Assy. (ex.WTDEMO0002, 01, KAL05, Bend, Indident, 20 )【WIP Txn】")]
        public XmlNode WIPLotRejectsInProcess(string motherLot, string waferScribeNumber, string equipment, string lossReason, string rejectCategory, string rejectsQty, string rejectCause, string rejectComment)
        {
            #region test parse XML
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
                Common.parseXML.CarCost carcost = new Common.parseXML.CarCost
                (
                    list.SelectSingleNode("ID").InnerText,
                    list.SelectSingleNode("uptime").InnerText,
                    list.SelectSingleNode("downtime").InnerText,
                    float.Parse(list.SelectSingleNode("price").InnerText)
                );
            }
            #endregion

            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_WIPLotRejectsInProcess(motherLot, waferScribeNumber, equipment, lossReason, rejectCategory, rejectsQty, rejectCause, rejectComment);

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
        [WebMethod(Description = "Reject Lot Dispose. waferScribeNumber for Wafersort or NULL waferScribeNumber for Assy. (ex.WTDEMO0002, 01, KAL05, Bend, Indident, 20 )【WIP Txn】")]
        public XmlNode WIPLotRejectsPostProcess(string motherLot, string waferScribeNumber, string lossReason, string rejectCategory, string rejectCause, string rejectComment, string rejectQty)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_WIPLotRejectsPostProcess(motherLot, waferScribeNumber, lossReason, rejectCategory, rejectCause, rejectComment, rejectQty);

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
        
        [WebMethod(Description = "Move-in Lot. equipmentKind(Item=1, WafterSort=2, BackGrind=3, Assembly=4, Test=5, Packing=6)【WIP Txn】")]
        public XmlNode MoveInLot(string lot, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_MoveInLot(lot, _equipmentKind);

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

        [WebMethod(Description = "Track-in Lot. equipmentKind(Item=1, WafterSort=2, BackGrind=3, Assembly=4, Test=5, Packing=6)【WIP Txn】")]
        public XmlNode TrackInLot(string lot,string equipment,string qty, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_TrackInLot(lot, equipment,qty, _equipmentKind);

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

        [WebMethod(Description = "Track-Out Lot. equipmentKind(Item=1, WafterSort=2, BackGrind=3, Assembly=4, Test=5, Packing=6)【WIP Txn】")]
        public XmlNode TrackOutLot(string lot, string equipment,string qty, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_TrackOutLot(lot, equipment, qty, _equipmentKind);
            
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

        [WebMethod(Description = "Move-Out Lot. equipmentKind(Item=1, WafterSort=2, BackGrind=3, Assembly=4, Test =5, Packing=6)【WIP Txn】")]
        public XmlNode MoveOutLot(string lot, string qty, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_MoveOutLot(lot, qty, _equipmentKind);

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


        //Txn
        public csiDocument Txn_WIPLotRejectsInProcess(string motherLot, string waferScribeNumber, string equipment, string lossReason, string rejectCategory, string rejectsQty, string rejectCause, string rejectComment)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = "LotRejectsInProcess";

            try
            {
                //initial
                com = new Common.func(serviceName + "Doc", serviceName);

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.namedObjectField("Equipment").dataField("__name").setValue(equipment);
                InputData.namedObjectField("ProcessType").dataField("__name").setValue("NORMAL");
                InputData.namedObjectField("Container").dataField("__name").setValue(motherLot);

                csiContainer csiCon = InputData.containerList("Details").appendItem("", "");
                csiCon.namedObjectField("LossReason").dataField("__name").setValue(lossReason);
                csiCon.namedObjectField("RejectCategory").dataField("__name").setValue(rejectCategory);

                if (string.IsNullOrEmpty(waferScribeNumber))
                {
                    //Assy
                    csiCon.dataField("RejectCause").setValue(rejectCause);
                    csiCon.dataField("RejectComment").setValue(rejectComment);
                    csiCon.dataField("RejectQty").setValue(rejectsQty);
                    csiCon.dataField("ReworkableRejectQty").setValue("0");
                    csiCon.dataField("UnidentifiableRejectQty").setValue("0");
                }
                else
                {
                    //Wafer sort
                    csiCon.dataField("WaferRejectsQty").setValue(rejectsQty);
                    csiCon.dataField("WaferRejectsType").setValue("WHOLE");//or LOCATION
                    csiCon.dataField("WaferScribeNumber").setValue(motherLot + "-" + waferScribeNumber);
                    csiCon.dataField("YieldOffWafer").setValue("false");
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
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "WIPLotReject"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "WIPLotReject"), true);
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
        
        public csiDocument Txn_WIPLotRejectsPostProcess(string motherLot, string waferScribeNumber, string lossReason, string rejectCategory, string rejectCause, string rejectComment, string rejectQty)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = "LotRejectsPostProcess";

            try
            {
                //initial
                com = new Common.func(serviceName + "Doc", serviceName);

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.namedObjectField("ProcessType").dataField("__name").setValue("NORMAL");
                InputData.namedObjectField("Container").dataField("__name").setValue(motherLot);

                csiContainer csiCon = InputData.containerList("Details").appendItem("", "");
                csiCon.namedObjectField("LossReason").dataField("__name").setValue(lossReason);
                csiCon.namedObjectField("RejectCategory").dataField("__name").setValue(rejectCategory);
                csiCon.dataField("RejectCause").setValue(rejectCause);

                if (string.IsNullOrEmpty(waferScribeNumber))
                {
                    //Assy
                    csiCon.dataField("DefectQty").setValue("0");//                                           
                    csiCon.dataField("RejectComment").setValue(rejectComment);
                    csiCon.dataField("RejectQty").setValue(rejectQty);
                    csiCon.dataField("ReworkableRejectQty").setValue("0");//
                    csiCon.dataField("UnidentifiableRejectQty").setValue("0");//
                }
                else
                {
                    //Wafersort
                    csiCon.dataField("WaferRejectsQty").setValue(rejectQty);
                    csiCon.dataField("WaferRejectsType").setValue("WHOLE");//LOCATION
                    csiCon.dataField("WaferScribeNumber").setValue(motherLot + "-" + waferScribeNumber);
                    csiCon.dataField("YieldOffWafer").setValue("true");
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
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "WIPLotReject"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "WIPLotReject"), true);
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

        public csiDocument Txn_MoveInLot(string lot, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;
            string flag = "5";

            try
            {
                switch ((int)_equipmentKind)
                {
                    case 1://Item
                        serviceName = "WaferWIPMain";
                        break;
                    case 2://WaferSort
                        serviceName = "WaferSortWIPMain";
                        break;
                    case 3://BackGrind
                        serviceName = "BackGrindWIPMain";
                        break;
                    case 4://Assembly
                        serviceName = "AssemblyMotherLotWIPMain";
                        break;
                    case 5://Test
                        serviceName = "FinalTestWIPMain";
                        break;
                    case 6://Packing
                        serviceName = "FinalTestWIPMain";
                        break;
                }

                //initial
                com = new Common.func(serviceName + "Doc", serviceName);

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.containerList("Containers").appendItem(lot, "");
                InputData.dataField("WIPFlag").setValue(flag);

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
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "MoveIn"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "MoveIn"), true);
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
        
        public csiDocument Txn_TrackInLot(string lot, string equipment,string qty, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;
            string flag = "1";

            try
            {
                //initial
                com = new Common.func(serviceName + "Doc", serviceName);

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.containerList("Containers").appendItem(lot, "");
                InputData.namedObjectField("Equipment").dataField("__name").setValue(equipment);//ex. MNT001-1
                InputData.namedObjectField("ProcessType").dataField("__name").setValue("NORMAL");

                switch ((int)_equipmentKind)
                {
                    case 1://Item
                        serviceName = "WaferWIPMain";
                        //<WafersDetails>
                        //    <__listItem>
                        //        <LotWafersItem><__Id><![CDATA[4803028000003835]]></__Id></LotWafersItem>
                        //        <Container><__name><![CDATA[N22_API009]]></__name></Container>
                        //        <WaferScribeNumber><![CDATA[N22_API009-01]]></WaferScribeNumber>
                        //        <WIPLotDetailsWafersItem><__Id><![CDATA[480abd800000235d]]></__Id></WIPLotDetailsWafersItem>
                        //   </__listItem>
                        //   <__listItem>
                        //        <LotWafersItem><__Id><![CDATA[4803028000003836]]></__Id></LotWafersItem>
                        //        <Container><__name><![CDATA[N22_API009]]></__name></Container>
                        //        <WaferScribeNumber><![CDATA[N22_API009-02]]></WaferScribeNumber>
                        //        <WIPLotDetailsWafersItem><__Id><![CDATA[480abd800000235e]]></__Id></WIPLotDetailsWafersItem>
                        //   </__listItem>
                        //</WafersDetails>
                        break;
                    case 2://WaferSort
                        serviceName = "WaferSortWIPMain";
                        //<WafersDetails>
                        //    <__listItem>
                        //      <LotWafersItem><__Id><![CDATA[4803028000001f57]]></__Id></LotWafersItem>
                        //      <Container><__name><![CDATA[WTDEMO0002]]></__name></Container>
                        //      <WaferScribeNumber><![CDATA[WTDEMO0002-01]]></WaferScribeNumber>
                        //      <WIPLotDetailsWafersItem><__Id><![CDATA[480abd800000214b]]></__Id></WIPLotDetailsWafersItem>
                        //    </__listItem>
                        //    <__listItem>
                        //      <LotWafersItem><__Id><![CDATA[4803028000001f58]]></__Id></LotWafersItem>
                        //      <Container><__name><![CDATA[WTDEMO0002]]></__name></Container>
                        //      <WaferScribeNumber><![CDATA[WTDEMO0002-02]]></WaferScribeNumber>
                        //      <WIPLotDetailsWafersItem><__Id><![CDATA[480abd800000214c]]></__Id></WIPLotDetailsWafersItem>
                        //    </__listItem>
                        //  </WafersDetails>
                        break;
                    case 3://BackGrind
                        serviceName = "BackGrindWIPMain";
                        InputData.dataField("TrackInQty").setValue(qty);
                        break;
                    case 4://Assembly
                        serviceName = "AssemblyMotherLotWIPMain";
                        InputData.dataField("TrackInQty").setValue(qty);
                        break;
                    case 5://Test
                        serviceName = "FinalTestWIPMain";
                        InputData.dataField("TrackInQty").setValue(qty);
                        break;
                    case 6://Packing
                        serviceName = "FinalTestWIPMain";
                        InputData.dataField("TrackInQty").setValue(qty);
                        break; 
                }

                InputData.dataField("WIPFlag").setValue(flag);

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
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "TrackIn"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "TrackIn"), true);
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
        
        public csiDocument Txn_TrackOutLot(string lot, string equipment,string qty, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;
            string flag = "2";

            try
            {

                //initial
                com = new Common.func(serviceName + "Doc", serviceName);

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.containerList("Containers").appendItem(lot, "");
                InputData.namedObjectField("Equipment").dataField("__name").setValue(equipment);//ex. MNT001-1
                InputData.namedObjectField("ProcessType").dataField("__name").setValue("NORMAL");
                
                switch ((int)_equipmentKind)
                {
                    case 1://Item
                        serviceName = "WaferWIPMain";
                        //<WafersDetails>
                        //    <__listItem>
                        //        <LotWafersItem><__Id><![CDATA[4803028000003835]]></__Id></LotWafersItem>
                        //        <Container><__name><![CDATA[N22_API009]]></__name></Container>
                        //        <WaferScribeNumber><![CDATA[N22_API009-01]]></WaferScribeNumber>
                        //        <WIPLotDetailsWafersItem><__Id><![CDATA[480abd800000235d]]></__Id></WIPLotDetailsWafersItem>
                        //   </__listItem>
                        //   <__listItem>
                        //        <LotWafersItem><__Id><![CDATA[4803028000003836]]></__Id></LotWafersItem>
                        //        <Container><__name><![CDATA[N22_API009]]></__name></Container>
                        //        <WaferScribeNumber><![CDATA[N22_API009-02]]></WaferScribeNumber>
                        //        <WIPLotDetailsWafersItem><__Id><![CDATA[480abd800000235e]]></__Id></WIPLotDetailsWafersItem>
                        //   </__listItem>
                        //</WafersDetails>
                        break;
                    case 2://WaferSort
                        serviceName = "WaferSortWIPMain";
                         //<WafersDetails>
                         //   <__listItem>
                         //     <LotWafersItem><__Id><![CDATA[4803028000001f57]]></__Id></LotWafersItem>
                         //     <Container><__name><![CDATA[WTDEMO0002]]></__name></Container>
                         //     <WaferScribeNumber><![CDATA[WTDEMO0002-01]]></WaferScribeNumber>
                         //     <WIPLotDetailsWafersItem><__Id><![CDATA[480abd8000002141]]></__Id></WIPLotDetailsWafersItem>
                         //   </__listItem>
                         //   <__listItem>
                         //     <LotWafersItem><__Id><![CDATA[4803028000001f58]]></__Id></LotWafersItem>
                         //     <Container><__name><![CDATA[WTDEMO0002]]></__name></Container>
                         //     <WaferScribeNumber><![CDATA[WTDEMO0002-02]]></WaferScribeNumber>
                         //     <WIPLotDetailsWafersItem><__Id><![CDATA[480abd8000002142]]></__Id></WIPLotDetailsWafersItem>
                         //   </__listItem>
                         //</WafersDetails>
                        break;
                    case 3://BackGrind
                        serviceName = "BackGrindWIPMain";
                        InputData.dataField("TrackOutQty").setValue(qty);
                        break;
                    case 4://Assembly
                        serviceName = "AssemblyMotherLotWIPMain";
                        InputData.dataField("TrackOutQty").setValue(qty);
                        break;
                    case 5://Test
                        serviceName = "FinalTestWIPMain";
                        InputData.dataField("TrackOutQty").setValue(qty);
                        break;
                    case 6://Packing
                        serviceName = "FinalTestWIPMain";
                        InputData.dataField("TrackOutQty").setValue(qty);
                        break;
                }
                InputData.dataField("WIPFlag").setValue(flag);
                
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
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "TrackOut"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "TrackOut"), true);
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
        
        public csiDocument Txn_MoveOutLot(string lot, string qty, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;
            string flag = "4";

            try
            {
                switch ((int)_equipmentKind)
                {
                    case 1://Item
                        serviceName = "WaferWIPMain";
                        break;
                    case 2://WaferSort
                        serviceName = "WaferSortWIPMain";
                        break;
                    case 3://BackGrind
                        serviceName = "BackGrindWIPMain";
                        break;
                    case 4://Assembly
                        serviceName = "AssemblyMotherLotWIPMain";
                        break;
                    case 5://Test
                        serviceName = "FinalTestWIPMain";
                        break;
                    case 6://Packing
                        serviceName = "FinalTestWIPMain";
                        break;
                }

                //initial
                com = new Common.func(serviceName + "Doc", serviceName);

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.containerList("Containers").appendItem(lot, "");
                InputData.namedObjectField("ProcessType").dataField("__name").setValue("NORMAL");
                InputData.dataField("MoveOutQty").setValue(qty);
                InputData.dataField("WIPFlag").setValue(flag);

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
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "MoveOut"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "MoveOut"), true);
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
