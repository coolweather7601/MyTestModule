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
    /// Summary description for Ad_HocTxn
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Ad_HocTxn : System.Web.Services.WebService
    {
        CamStar_Service.Common.func com;

        [WebMethod(Description = "View Container LotModifyAttrs.【Ad-Hoc Txn】")]
        public XmlNode LotModifyAttrs(string lot)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                csiDocument ResponseDocument = Txn_LotModifyAttrs(lot);

                if (com.CheckForErrors(ResponseDocument) == "completion")
                {
                    csiResponseData responseData = ResponseDocument.getService().responseData();
                    csiContainer SelectionContainer = responseData.getResponseFieldByName("SelectionContainer").asContainer();
                    string SelectionContainer_name = SelectionContainer.getName();
                    string SelectionContainer_level = SelectionContainer.getLevel();

                    csiSubentityList ServiceAttrsDetailsSelection = responseData.getResponseFieldByName("ServiceAttrsDetailsSelection").asSubentityList();//SubentityObjRef
                    string listStr = "";
                    foreach (csiSubentity item in ServiceAttrsDetailsSelection.getListItems())
                    {
                        string Attribute = item.getField("Attribute").asNamedObject().getRef();
                        string AttributeValue = item.getField("AttributeValue").asDataField().getValue();
                        string FieldType = item.getField("FieldType").asDataField().getValue();
                        if (!string.IsNullOrEmpty(Attribute) || !string.IsNullOrEmpty(AttributeValue) || !string.IsNullOrEmpty(FieldType))
                            listStr += string.Format(@"<Attribute>
                                                        <AttributeName>{0}</AttributeName>
                                                        <AttributeValue>{1}</AttributeValue>
                                                        <FieldType>{2}</FieldType>
                                                       </Attribute>", Attribute, AttributeValue, FieldType);
                    }

                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                                <responseData>
                                                                    <SelectionContainer>
                                                                        <name>{0}</name>
                                                                        <level>{1}</level>
                                                                    </SelectionContainer>
                                                                    <ServiceAttrsDetailsSelection>
                                                                    {2}
                                                                    </ServiceAttrsDetailsSelection>
                                                                </responseData>
                                                              </ResultMessage>", SelectionContainer_name,
                                                                                 SelectionContainer_level,
                                                                                 listStr));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", com.CheckForErrors(ResponseDocument)));
                    return (xmlDocumentObject);
                }

            }
            catch (Exception e)
            {
                xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", e.ToString()));
                return (xmlDocumentObject);
            }
        }

        [WebMethod(Description = "Puts given Container on hold(ex. N25_AP01, HOLDLOC001, AA, TEST)【Ad-Hoc Txn】")]
        public XmlNode LotHold(string container, string holdLocation, string holdReason, string comment)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_LotHold(container, holdLocation, holdReason, comment);

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

        [WebMethod(Description = "Puts given Container on FutureLotHold(ex. N25_AP01, SQA, 1, HOLDLOC003, ADD, 5, TEST_CCOMMENT)【Ad-Hoc Txn】")]
        public XmlNode LotFutureHoldSetup(string container, string spec, string specRev, string holdLocation, string holdReason, string expectedHoldDays, string comment)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_LotFutureHoldSetup(container, spec, specRev, holdLocation, holdReason, expectedHoldDays, comment);

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

        [WebMethod(Description = "View Container LotModify Scribe Items.【Ad-Hoc Txn】")]
        public XmlNode LotWafersScribeItems(string lot)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                csiDocument ResponseDocument = Txn_LotWafersScribeItems(lot);

                if (com.CheckForErrors(ResponseDocument) == "completion")
                {
                    csiResponseData responseData = ResponseDocument.getService().responseData();
                    csiContainer SelectionContainer = responseData.getResponseFieldByName("SelectionContainer").asContainer();
                    string SelectionContainer_name = SelectionContainer.getName();
                    string SelectionContainer_level = SelectionContainer.getLevel();

                    string Qty = responseData.getResponseFieldByName("Qty").asDataField().getValue();
                    string Qty2 = responseData.getResponseFieldByName("Qty2").asDataField().getValue();

                    csiSubentityList ServiceAttrsDetailsSelection = responseData.getResponseFieldByName("LotWafers").asSubentityList();//SubentityObjRef
                    string listStr = "";
                    foreach (csiSubentity item in ServiceAttrsDetailsSelection.getListItems())
                    {
                        string WaferScribeNumber = item.getField("WaferScribeNumber").asDataField().getValue();
                        if (!string.IsNullOrEmpty(WaferScribeNumber))
                            listStr += string.Format(@"<WaferScribeNumber>{0}</WaferScribeNumber>", WaferScribeNumber);
                    }

                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                                <responseData>
                                                                    <SelectionContainer>
                                                                        <name>{0}</name>
                                                                        <level>{1}</level>
                                                                    </SelectionContainer>
                                                                    <LotWafers>
                                                                    {2}
                                                                    </LotWafers>
                                                                </responseData>
                                                              </ResultMessage>", SelectionContainer_name,
                                                                                 SelectionContainer_level,
                                                                                 listStr));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", com.CheckForErrors(ResponseDocument)));
                    return (xmlDocumentObject);
                }

            }
            catch (Exception e)
            {
                xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                            <Code>-1</Code> 
                                                            <Message>{0}</Message> 
                                                          </ResultMessage>", e.ToString()));
                return (xmlDocumentObject);
            }
        }

        //logic 


        //Txn
        public csiDocument Txn_LotWafersScribeItems(string lot)
        {
            try
            {
                //initial
                com = new Common.func("LotModifyWafersDoc", "LotModifyWafers");

                //Create a input object
                csiObject InputData = com.gService.inputData();
                InputData.dataField("SelectionId").setValue(lot);

                //perform
                com.gService.perform("ResolveSelectionId");

                //Create a request object
                csiRequestData requestData = com.gService.requestData();
                requestData.requestField("SelectionContainer");
                requestData.requestField("Qty");
                requestData.requestField("Qty2");

                //LotWafers
                requestData.requestField("LotWafers.InstanceID");
                requestData.requestField("LotWafers.Brightness");
                requestData.requestField("LotWafers.BrokenPieces");
                requestData.requestField("LotWafers.ChipGrade");
                requestData.requestField("LotWafers.Color");
                requestData.requestField("LotWafers.DieSize");
                requestData.requestField("LotWafers.FilmId");
                requestData.requestField("LotWafers.Flux");
                requestData.requestField("LotWafers.GoodQty");
                requestData.requestField("LotWafers.Grade");
                requestData.requestField("LotWafers.NDPW");
                requestData.requestField("LotWafers.OxideThickness");
                requestData.requestField("LotWafers.ProductionGrade");
                requestData.requestField("LotWafers.RequireDataCollection");
                requestData.requestField("LotWafers.RequireTracking");
                requestData.requestField("LotWafers.ResortNoteCode");
                requestData.requestField("LotWafers.SortComments");
                requestData.requestField("LotWafers.SortEquipment");
                requestData.requestField("LotWafers.SortIQCComments");
                requestData.requestField("LotWafers.SortIQCResult");
                requestData.requestField("LotWafers.SortNoteCode");
                requestData.requestField("LotWafers.SortSQAComments");
                requestData.requestField("LotWafers.SortSQAResult");
                requestData.requestField("LotWafers.TxnTimestamp");
                requestData.requestField("LotWafers.Username");
                requestData.requestField("LotWafers.VendorLotNumber");
                requestData.requestField("LotWafers.VendorName");
                requestData.requestField("LotWafers.VF");
                requestData.requestField("LotWafers.WaferBatchId");
                requestData.requestField("LotWafers.WaferNumber");
                requestData.requestField("LotWafers.WaferProduct");
                requestData.requestField("LotWafers.WaferScribeNumber");
                requestData.requestField("LotWafers.WaferSequence");
                requestData.requestField("LotWafers.WaferSize");
                requestData.requestField("LotWafers.WaferThickness");


                //Request the completion message from the XML Application Server.
                com.gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                csiDocument ResponseDocument = com.gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "LotModifyWafers"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "LotModifyWafers"), true);
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

        public csiDocument Txn_LotModifyAttrs(string lot)
        {
            try
            {
                //initial
                com = new Common.func("LotModifyAttrsDoc", "LotModifyAttrs");

                //Create a input object
                csiObject InputData = com.gService.inputData();
                InputData.dataField("SelectionId").setValue(lot);

                //perform
                com.gService.perform("ResolveSelectionId");

                //Create a request object
                csiRequestData requestData = com.gService.requestData();
                requestData.requestField("SelectionContainer");

                //ServiceAttrsDetailsSelection
                requestData.requestField("ServiceAttrsDetailsSelection.AccessLevel");
                requestData.requestField("ServiceAttrsDetailsSelection.AlternateName1");
                requestData.requestField("ServiceAttrsDetailsSelection.AlternateName2");
                requestData.requestField("ServiceAttrsDetailsSelection.Attribute");
                requestData.requestField("ServiceAttrsDetailsSelection.AttributeName");
                requestData.requestField("ServiceAttrsDetailsSelection.AttributeRevision");
                requestData.requestField("ServiceAttrsDetailsSelection.AttributeValue");
                requestData.requestField("ServiceAttrsDetailsSelection.FieldType");
                requestData.requestField("ServiceAttrsDetailsSelection.IsRequired");
                requestData.requestField("ServiceAttrsDetailsSelection.ObjectTypeName");
                requestData.requestField("ServiceAttrsDetailsSelection.ServiceAttrsSetupName");

                //ServiceAttrsDetailsSelection.ValidValues
                requestData.requestField("ServiceAttrsDetailsSelection.ValidValues.AttributeRevision");
                requestData.requestField("ServiceAttrsDetailsSelection.ValidValues.AttributeValue");



                //Request the completion message from the XML Application Server.
                com.gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                csiDocument ResponseDocument = com.gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "LotModifyAttrs"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "LotModifyAttrs"), true);
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

        public csiDocument Txn_LotFutureHoldSetup(string container, string spec, string specRev, string holdLocation, string holdReason, string expectedHoldDays, string comment)
        {
            try
            {
                //initial
                com = new Common.func("LotFutureHoldSetupDoc", "LotFutureHoldSetup");

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.containerList("Containers").appendItem("__name", "").setRef(container, "");

                csiContainer csiCon = InputData.containerList("Details").appendItem("", "");
                csiCon.dataField("Comments").setValue(comment);
                csiCon.dataField("ExpectedHoldDays").setValue(expectedHoldDays);
                csiCon.namedObjectField("HoldReason").dataField("__name").setValue(holdReason);
                csiCon.namedObjectField("HoldLocation").dataField("__name").setValue(holdLocation);

                csiNamedObject csiSpec = csiCon.namedObjectField("Spec");
                csiSpec.dataField("__name").setValue(spec);
                csiSpec.dataField("__rev").setValue(specRev);


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
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "LotFutureHoldSetup"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "LotFutureHoldSetup"), true);
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

        public csiDocument Txn_LotHold(string container, string holdLocation, string holdReason, string comment)
        {
            try
            {
                //initial
                com = new Common.func("LotHoldDoc", "LotHold");

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                InputData.dataField("Comments").setValue(comment);

                csiContainer csiCon = InputData.containerList("Details").appendItem("", "");
                csiCon.dataField("ApplyToChildLots").setValue("false");
                csiCon.namedObjectField("Container").dataField("__name").setValue(container);
                csiCon.namedObjectField("HoldLocation").dataField("__name").setValue(holdLocation);
                csiCon.namedObjectField("HoldReason").dataField("__name").setValue(holdReason);

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
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "LotHold"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "LotHold"), true);
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
