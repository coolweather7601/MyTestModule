using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Camstar.XMLClient.API;
using System.IO;
using System.Xml;

namespace CamStar_Service
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service1 : System.Web.Services.WebService
    {
        public Guid gSessionID = Guid.NewGuid();
        public csiClient gClient = new csiClient();
        public csiConnection gConnection;
        public csiSession gSession;
        public csiService gService;
        public csiDocument gDocument;
        public string gHost = "twgkhhpsk1ms049";
        public int gPort= 2881;
        public string gUserName = "APKTEST14";
        public string gPassword= "Training!";

        #region 【Resource Txn】
        [WebMethod(Description = "Equipment set status in WaferSort part(ex. WST001, PRD, DOWN)【Resource Txn】")]
        public XmlNode waferSortEquipmentSetStatus(string _Resource, string _ResourceStatusCode, string _ResourceStatusReason)
        {            
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {                
                //KAL05, PRD, DOWN
                init();
                CreateDocumentandService("WaferSortEquipmentSetStatusDoc", "WaferSortEquipmentSetStatus");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                InputData.namedObjectField("Resource").dataField("__name").setValue(_Resource);
                InputData.namedObjectField("ResourceStatusCode").dataField("__name").setValue(_ResourceStatusCode);
                InputData.namedObjectField("ResourceStatusReason").dataField("__name").setValue(_ResourceStatusReason);

                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "WaferSortEquipmentSetStatus"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "WaferSortEquipmentSetStatus"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);

                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
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

        [WebMethod(Description = "Equipment setup in WaferSort part(ex. WST001, 190007001|190007002|190007003)【Resource Txn】")]
        public XmlNode waferSortEquipmentSetup(string _Resource, string _Tool)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                //KAL05, PRD, DOWN
                init();
                CreateDocumentandService("WaferSortEquipmentSetupDoc", "WaferSortEquipmentSetup");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                InputData.namedObjectField("Resource").dataField("__name").setValue(_Resource);

                string[] tools = _Tool.Split('|');
                foreach (string t in tools)
                {
                    InputData.containerList("Tools").appendItem(t, "");    
                }

                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();
                
                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "WaferSortEquipmentSetup"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "WaferSortEquipmentSetup"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);

                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    string check = CheckForErrors(ResponseDocument);
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", check));
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

        [WebMethod(Description = "Equipment set status in Test part(ex. AGIL-01, PRD, DOWN)【Resource Txn】")]
        public XmlNode TestEquipmentSetStatus(string _Resource, string _ResourceStatusCode, string _ResourceStatusReason)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("TestEquipmentSetStatusDoc", "TestEquipmentSetStatus");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                InputData.namedObjectField("Resource").dataField("__name").setValue(_Resource);
                InputData.namedObjectField("ResourceStatusCode").dataField("__name").setValue(_ResourceStatusCode);
                InputData.namedObjectField("ResourceStatusReason").dataField("__name").setValue(_ResourceStatusReason);


                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "WaferSortEquipmentSetStatus"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "WaferSortEquipmentSetStatus"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);

                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
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

        [WebMethod(Description = "Equipment set status in Assy part(ex. APK-DB001, SBY, DOWN)【Resource Txn】")]
        public XmlNode AssemblyEquipmentSetStatus(string _Resource, string _ResourceStatusCode, string _ResourceStatusReason)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("AssemblyEquipmentSetStatusDoc", "AssemblyEquipmentSetStatus");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                InputData.namedObjectField("Resource").dataField("__name").setValue(_Resource);
                InputData.namedObjectField("ResourceStatusCode").dataField("__name").setValue(_ResourceStatusCode);        
                InputData.namedObjectField("ResourceStatusReason").dataField("__name").setValue(_ResourceStatusReason);


                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "WaferSortEquipmentSetStatus"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "WaferSortEquipmentSetStatus"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);

                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
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
        #endregion

        #region 【Setup A】
        [WebMethod(Description = "Create a new equipment in Wafer Sort part.【Setup A】")]
        public XmlNode NewWaferSortEquipment(string _EquipmentName)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("WaferSortEquipmentMaintDoc", "WaferSortEquipmentMaint");
                gService.perform("New");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                csiNamedObject csiObj = InputData.namedObjectField("ObjectChanges");
                csiObj.dataField("Notes").setValue("TEST_NOTE");
                csiObj.dataField("Description").setValue("TEST_DESC");
                csiObj.dataField("Name").setValue(_EquipmentName);

                csiObj.namedObjectField("SetupAccess").dataField("__name").setValue("Approved");
                csiObj.namedObjectField("BOM").dataField("__useROR").setValue("false");
                csiObj.namedObjectField("DocumentSet").dataField("__name").setValue("Camstar");
                csiObj.namedObjectField("InitialStatus").dataField("__name").setValue("PRD");
                csiObj.namedObjectField("ResourceFamily").dataField("__name").setValue("WST_OVEN");
                csiObj.namedObjectField("ResourceIcon").dataField("__name").setValue("T1");
                csiObj.namedObjectField("Vendor").dataField("__name").setValue("V01");
                csiObj.namedObjectField("Factory").dataField("__name").setValue("APK");
                                
                //<Location>
                //  <__name />
                //  <__parent __CDOTypeName="Factory">
                //    <__name><![CDATA[APK]]></__name>
                //  </__parent>
                //</Location>
                
                //csiObj.namedObjectField("Location").dataField("__name").setValue("T1");
                

                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "NewWaferSortEquipment"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "NewWaferSortEquipment"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);


                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
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
        #endregion

        #region 【Inquiry Txn】
        [WebMethod(Description = "View Container details.【Inquiry Txn】")]
        public XmlNode ViewContainerStatus(string _Lot)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("ViewContainerStatusDoc", "ViewContainerStatus");
                
                //Create a input object
                csiObject InputData = gService.inputData();
                InputData.containerField("Container").setRef(_Lot, "");

                //Create a request object
                csiRequestData requestData = gService.requestData();
                requestData.requestField("ComputerName");
                requestData.requestField("Factory");
                requestData.requestField("LotStatus");
                requestData.requestField("MainQty");
                requestData.requestField("NextStep");
                requestData.requestField("ProcessSpec");
                requestData.requestField("ProcessSpecObjectCategory");
                requestData.requestField("ProcessSpecObjectType");
                requestData.requestField("ProcessSpecRevision");
                requestData.requestField("ProductLine");
                requestData.requestField("ProductRevision");
                requestData.requestField("Qty2");
                requestData.requestField("SS");
                requestData.requestField("SSObjectCategory");
                requestData.requestField("SSObjectType");
                requestData.requestField("SSRevision");
                requestData.requestField("StartReason");
                requestData.requestField("WIPStatus");                
                requestData.requestField("WIPType");
                requestData.requestField("WIPYieldResult");
                requestData.requestField("Location");
                requestData.requestField("Product");
                requestData.requestField("Qty");
                requestData.requestField("Step");
                requestData.requestField("Workflow");

                csiRequestField r1 =  requestData.requestField("ContainerStatusDetails");
                r1.requestField("HoldReason");
                r1.requestField("MfgOrder");
                r1.requestField("Owner");
                r1.requestField("PriorityCode");
                r1.requestField("ReworkReason");
                r1.requestField("WorkflowRev");
                
                csiRequestField r2 =  requestData.requestField("LotAttributes");
                r2.requestField("ShelfLocation");
                r2.requestField("ShipFromFactory");
                

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "ViewContainerStatus"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "ViewContainerStatus"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);
                              


                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    csiResponseData responseData = ResponseDocument.getService().responseData();
                    csiDataField ComputerName = (csiDataField)responseData.getResponseFieldByName("ComputerName");                   
                    csiDataField LotStatus = (csiDataField)responseData.getResponseFieldByName("LotStatus");
                    csiDataField MainQty = (csiDataField)responseData.getResponseFieldByName("MainQty");
                    csiDataField NextStep = (csiDataField)responseData.getResponseFieldByName("NextStep");
                    
                    csiDataField ProcessSpec = (csiDataField)responseData.getResponseFieldByName("ProcessSpec");
                    csiDataField ProcessSpecObjectCategory = (csiDataField)responseData.getResponseFieldByName("ProcessSpecObjectCategory");
                    csiDataField ProcessSpecObjectType = (csiDataField)responseData.getResponseFieldByName("ProcessSpecObjectType");
                    csiDataField ProcessSpecRevision = (csiDataField)responseData.getResponseFieldByName("ProcessSpecRevision");
                    csiDataField ProductLine = (csiDataField)responseData.getResponseFieldByName("ProductLine");

                    csiDataField ProductRevision = (csiDataField)responseData.getResponseFieldByName("ProductRevision");
                    csiDataField Qty2 = (csiDataField)responseData.getResponseFieldByName("Qty2");
                    csiDataField SS = (csiDataField)responseData.getResponseFieldByName("SS");
                    csiDataField SSObjectCategory = (csiDataField)responseData.getResponseFieldByName("SSObjectCategory");
                    csiDataField SSObjectType = (csiDataField)responseData.getResponseFieldByName("SSObjectType");

                    csiDataField SSRevision = (csiDataField)responseData.getResponseFieldByName("SSRevision");
                    csiDataField StartReason = (csiDataField)responseData.getResponseFieldByName("StartReason");
                    csiDataField WIPStatus = (csiDataField)responseData.getResponseFieldByName("WIPStatus");
                    csiDataField WIPType = (csiDataField)responseData.getResponseFieldByName("WIPType");
                    csiDataField WIPYieldResult = (csiDataField)responseData.getResponseFieldByName("WIPYieldResult");

                    csiDataField Location = (csiDataField)responseData.getResponseFieldByName("Location");
                    csiDataField Product = (csiDataField)responseData.getResponseFieldByName("Product");
                    csiDataField Qty = (csiDataField)responseData.getResponseFieldByName("Qty");
                    csiDataField Step = (csiDataField)responseData.getResponseFieldByName("Step");
                    csiDataField Workflow = (csiDataField)responseData.getResponseFieldByName("Workflow");

                    string factory = responseData.getResponseFieldByName("Factory").asNamedObject().getRef(); // NamedObjRef

                    csiSubentity ContainerStatusDetails = responseData.getResponseFieldByName("ContainerStatusDetails").asSubentity();//SubentityObjRef
                    string HoldReason = ContainerStatusDetails.getField("HoldReason").asNamedObject().getRef();
                    string MfgOrder = ContainerStatusDetails.getField("MfgOrder").asNamedObject().getRef();
                    string Owner = ContainerStatusDetails.getField("Owner").asNamedObject().getRef();
                    string PriorityCode = ContainerStatusDetails.getField("PriorityCode").asNamedObject().getRef();
                    string ReworkReason = ContainerStatusDetails.getField("ReworkReason").asNamedObject().getRef();
                    string WorkflowRev = ContainerStatusDetails.dataField("WorkflowRev").getValue();

                    csiSubentity LotAttributes = responseData.getResponseFieldByName("LotAttributes").asSubentity();//SubentityObjRef
                    string ShelfLocation = LotAttributes.getField("ShelfLocation").asDataField().getValue();
                    string ShipFromFactory = LotAttributes.getField("ShipFromFactory").asNamedObject().getRef();


                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                                <responseData>
                                                                <ComputerName >{0}</ComputerName>
                                                                <Factory >{1}</Factory>
                                                                <LotStatus >{2}</LotStatus>
                                                                <MainQty >{3}</MainQty>
                                                                <NextStep >{4}</NextStep>
                                                                <ProcessSpec >{5}</ProcessSpec>
                                                                <ProcessSpecObjectCategory >{6}</ProcessSpecObjectCategory>
                                                                <ProcessSpecObjectType >{7}</ProcessSpecObjectType>
                                                                <ProcessSpecRevision >{8}</ProcessSpecRevision>
                                                                <ProductLine >{9}</ProductLine>
                                                                <ProductRevision >{10}</ProductRevision>
                                                                <Qty2 >{11}</Qty2>
                                                                <SS >{12}</SS>
                                                                <SSObjectCategory >{13}</SSObjectCategory>
                                                                <SSObjectType >{14}</SSObjectType>
                                                                <SSRevision >{15}</SSRevision>
                                                                <StartReason >{16}</StartReason>
                                                                <WIPStatus >{17}</WIPStatus>
                                                                <WIPType >{18}</WIPType>
                                                                <WIPYieldResult >{19}</WIPYieldResult>
                                                                <Location >{20}</Location>
                                                                <Product >{21}</Product>
                                                                <Qty >{22}</Qty>
                                                                <Step >{23}</Step>
                                                                <Workflow >{24}</Workflow>
                                                                <ContainerStatusDetails>
                                                                    <HoldReason>{25}</HoldReason>
                                                                    <MfgOrder>{26}</MfgOrder>
                                                                    <Owner>{27}</Owner>
                                                                    <PriorityCode>{28}</PriorityCode>
                                                                    <ReworkReason>{29}</ReworkReason>
                                                                    <WorkflowRev>{30}</WorkflowRev>
                                                                </ContainerStatusDetails>
                                                                <LotAttributes>
                                                                    <ShelfLocation>{31}</ShelfLocation>
                                                                    <ShipFromFactory>{32}</ShipFromFactory>
                                                                </LotAttributes>
                                                                </responseData>
                                                              </ResultMessage>", ComputerName.getValue(),
                                                                                 factory,
                                                                                 LotStatus.getValue(),
                                                                                 MainQty.getValue(),
                                                                                 NextStep.getValue(),
                                                                                 ProcessSpec.getValue(),
                                                                                 ProcessSpecObjectCategory.getValue(),
                                                                                 ProcessSpecObjectType.getValue(),
                                                                                 ProcessSpecRevision.getValue(),
                                                                                 ProductLine.getValue(),
                                                                                 ProductRevision.getValue(),
                                                                                 Qty2.getValue(),
                                                                                 SS.getValue(),
                                                                                 SSObjectCategory.getValue(),
                                                                                 SSObjectType.getValue(),
                                                                                 SSRevision.getValue(),
                                                                                 StartReason.getValue(),
                                                                                 WIPStatus.getValue(),
                                                                                 WIPType.getValue(),
                                                                                 WIPYieldResult.getValue(),
                                                                                 Location.getValue(),
                                                                                 Product.getValue(),
                                                                                 Qty.getValue(),
                                                                                 Step.getValue(),
                                                                                 Workflow.getValue(),
                                                                                 HoldReason,
                                                                                 MfgOrder,
                                                                                 Owner,
                                                                                 PriorityCode,
                                                                                 ReworkReason,
                                                                                 WorkflowRev,
                                                                                 ShelfLocation,
                                                                                 ShipFromFactory
                                                                                 ));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
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
        #endregion

        #region 【Ad-Hoc Txn】
        [WebMethod(Description = "View Container LotModifyAttrs.【Ad-Hoc Txn】")]
        public XmlNode LotModifyAttrs(string _Lot)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("LotModifyAttrsDoc", "LotModifyAttrs");

                //Create a input object
                csiObject InputData = gService.inputData();
                InputData.dataField("SelectionId").setValue(_Lot);
                
                //perform
                gService.perform("ResolveSelectionId");

                //Create a request object
                csiRequestData requestData = gService.requestData();
                requestData.requestField("SelectionContainer");

                csiRequestField r1 = requestData.requestField("ServiceAttrsDetailsSelection");
                r1.requestField("AccessLevel");
                r1.requestField("AlternateName1");
                r1.requestField("AlternateName2");
                r1.requestField("Attribute");
                r1.requestField("AttributeName");
                r1.requestField("AttributeRevision");
                r1.requestField("AttributeValue");
                r1.requestField("FieldType");
                r1.requestField("IsRequired");
                r1.requestField("ObjectTypeName");
                r1.requestField("ServiceAttrsSetupName");

                csiRequestField r2 = r1.requestField("ValidValues");
                r2.requestField("AttributeRevision");
                r2.requestField("AttributeValue");


                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "LotModifyAttrs"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "LotModifyAttrs"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);



                if (CheckForErrors(ResponseDocument) == "completion")
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
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
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
        public XmlNode LotHold(string _Container, string _HoldLocation, string _HoldReason, string _Comment)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("LotHoldDoc", "LotHold");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                InputData.dataField("Comments").setValue(_Comment);
                csiContainerList csiList = InputData.containerList("Details");
                csiContainer csiCon = csiList.appendItem("", "");
                csiCon.dataField("ApplyToChildLots").setValue("false");
                csiCon.namedObjectField("Container").dataField("__name").setValue(_Container);
                csiCon.namedObjectField("HoldLocation").dataField("__name").setValue(_HoldLocation);
                csiCon.namedObjectField("HoldReason").dataField("__name").setValue(_HoldReason);

                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "LotHold"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "LotHold"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);


                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
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

        [WebMethod(Description = "Puts given Container on FutureLotHold(ex. N25_AP01, SQA, 1, HOLDLOC003, ADD, 5, TEST_CCOMMENT)【Ad-Hoc Txn】")]
        public XmlNode LotFutureHoldSetup(string _Container,string _Spec, string _SpecRev, string _HoldLocation, string _HoldReason, string _ExpectedHoldDays, string _Comment)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("LotFutureHoldSetupDoc", "LotFutureHoldSetup");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                csiContainerList csiContainers = InputData.containerList("Containers");
                csiContainers.appendItem("__name", "").setRef(_Container,"");

                csiContainerList csiDetails = InputData.containerList("Details");
                csiContainer csiCon = csiDetails.appendItem("", "");
                csiCon.dataField("Comments").setValue(_Comment);
                csiCon.dataField("ExpectedHoldDays").setValue(_ExpectedHoldDays);
                csiCon.namedObjectField("HoldReason").dataField("__name").setValue(_HoldReason);
                csiCon.namedObjectField("HoldLocation").dataField("__name").setValue(_HoldLocation);
                csiNamedObject csiSpec = csiCon.namedObjectField("Spec");
                csiSpec.dataField("__name").setValue(_Spec);
                csiSpec.dataField("__rev").setValue(_SpecRev);


                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "LotFutureHoldSetup"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "LotFutureHoldSetup"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);


                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
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

        [WebMethod(Description = "View Container LotModify Scribe Items.【Ad-Hoc Txn】")]
        public XmlNode LotWafersScribeItems(string _Lot)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("LotModifyWafersDoc", "LotModifyWafers");

                //Create a input object
                csiObject InputData = gService.inputData();
                InputData.dataField("SelectionId").setValue(_Lot);

                //perform
                gService.perform("ResolveSelectionId");

                //Create a request object
                csiRequestData requestData = gService.requestData();
                requestData.requestField("SelectionContainer");
                requestData.requestField("Qty");
                requestData.requestField("Qty2");

                csiRequestField r1 = requestData.requestField("LotWafers");
                r1.requestField("InstanceID");
                r1.requestField("Brightness");
                r1.requestField("BrokenPieces");
                r1.requestField("ChipGrade");
                r1.requestField("Color");
                r1.requestField("DieSize");
                r1.requestField("FilmId");
                r1.requestField("Flux");
                r1.requestField("GoodQty");
                r1.requestField("Grade");
                r1.requestField("NDPW");
                r1.requestField("OxideThickness");
                r1.requestField("ProductionGrade");
                r1.requestField("RequireDataCollection");
                r1.requestField("RequireTracking");
                r1.requestField("ResortNoteCode");
                r1.requestField("SortComments");
                r1.requestField("SortEquipment");
                r1.requestField("SortIQCComments");
                r1.requestField("SortIQCResult");                
                r1.requestField("SortNoteCode");
                r1.requestField("SortSQAComments");
                r1.requestField("SortSQAResult");
                r1.requestField("TxnTimestamp");
                r1.requestField("Username");
                r1.requestField("VendorLotNumber");
                r1.requestField("VendorName");
                r1.requestField("VF");
                r1.requestField("WaferBatchId");
                r1.requestField("WaferNumber");
                r1.requestField("WaferProduct");
                r1.requestField("WaferScribeNumber");
                r1.requestField("WaferSequence");
                r1.requestField("WaferSize");
                r1.requestField("WaferThickness");

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "LotModifyWafers"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "LotModifyWafers"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);



                if (CheckForErrors(ResponseDocument) == "completion")
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
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
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
        #endregion

        #region 【WIP Txn】

        #endregion

        //initial parameters
        private void init()
        {
            
            //Configure connection information for InSite XML Server
            gConnection = gClient.createConnection(gHost,gPort);
            //Create session information for InSite XML Server
            gSession = gConnection.createSession(gUserName, gPassword, gSessionID.ToString());
        }

        //create service
        private void CreateDocumentandService(string DocumentName, string ServiceName)
        {
            //If a document already exist remove the document
            if ((DocumentName.Length) > 0) 
            {
                gSession.removeDocument(DocumentName);
            }

            //clear service if it is not nothing
            if (gService!=null)
            {
               gService = null;
            }

            //Create a new document
            gDocument = gSession.createDocument(DocumentName);

            //you cannot create a service if you have no name to assign it
            if (ServiceName.Length > 0) 
            {
                gService = gDocument.createService(ServiceName); //Create a service
            }
        }

        //check error or exception
        private string CheckForErrors(csiDocument _ResponseDocument)
        {
            csiExceptionData csiexceptiondata;
            csiField CompletionMsg;
            csiService csiService;
            string ErrorMsg;

            if (_ResponseDocument.checkErrors() == true)
            {

                //Get the error from the response document and throw an exception.
                csiexceptiondata = _ResponseDocument.exceptionData();

                //Throw a new exception for the calling object
                ErrorMsg = "Severity: " + csiexceptiondata.getSeverity();
                ErrorMsg = ErrorMsg + "Error Code: ";
                ErrorMsg = ErrorMsg + csiexceptiondata.getErrorCode() + "\n";
                ErrorMsg = ErrorMsg + "Description: " + csiexceptiondata.getDescription();
                return ErrorMsg;
                //Throw New System.Exception(ErrorMsg)
            }
            else
            {
                // Get completion message and display
                csiService = _ResponseDocument.getService(); 
                if (csiService != null)
                {
                    CompletionMsg = csiService.responseData().getResponseFieldByName("CompletionMsg");
                    return "completion";
                    //MsgBox(CompletionMsg.getValue, MsgBoxStyle.OkOnly, "CompletionMessage")
                }
            }
            return "";
        }

        //save xml
        public void PrintDoc(string documentContent, bool isInputDoc)
        {
            try
            {
                string strAppPath = Server.MapPath(".") + "\\xml\\";
                string strDocPath = (isInputDoc == true) ? "inputDoc.xml" : "responseDoc.xml";
                
                strDocPath = strAppPath + strDocPath;

                StreamWriter objSW = File.CreateText(strDocPath);
                objSW.Write(documentContent);
                objSW.Flush();
                objSW.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}