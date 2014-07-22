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
    /// Summary description for SetupA
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SetupA : System.Web.Services.WebService
    {        
        CamStar_Service.Common.func com;

        [WebMethod(Description = "Create a new equipment; equipmentKind(Item = 1, WafterSort = 2, BackGrind = 3, Assembly = 4, Test = 5).【Setup A】")]
        public XmlNode AddEquipment(string equipmentName, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_AddEquipment(equipmentName, _equipmentKind);

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

        [WebMethod(Description = "Delete a equipment; equipmentKind(Item = 1, WafterSort = 2, BackGrind = 3, Assembly = 4, Test = 5)【Setup A】")]
        public XmlNode DeleteEquipment(string equipmentName, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_DeleteEquipment(equipmentName, _equipmentKind);

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

        [WebMethod(Description = "Delete a equipment; equipmentKind(Item = 1, WafterSort = 2, BackGrind = 3, Assembly = 4, Test = 5)【Setup A】")]
        public XmlNode EquipmentResourceControl(string equipmentName,string toolPlan,Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string resultMsg = "";

            csiDocument ResponseDocument = Txn_EquipmentResourceControl(equipmentName, toolPlan, _equipmentKind);

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

        #region Sebastian Lee
        [WebMethod(Description = "List Work Order Attrs.")]
        public XmlNode WorkOrderAttrs(string _WorkOrder)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;

            try
            {
                //initial
                com = new Common.func("WorkOrderDoc", "WorkOrderMaint");

                //Create a input object
                csiObject InputData = com.gService.inputData();
                // InputData.namedObjectField("ObjectToChange").setRef("TC_WO_UC0002_PA_1");
                InputData.namedObjectField("ObjectToChange").setRef(_WorkOrder);

                //Load the object
                com.gService.perform("Load");

                //Request the completion message from the XML Application Server.
                com.gService.requestData().requestField("CompletionMsg");

                //Create request data fields
                csiRequestData requestData = com.gService.requestData();
                csiRequestField r1 = requestData.requestField("ObjectChanges");
                r1.requestField("Name");
                r1.requestField("Qty");
                r1.requestField("Product");
                r1.requestField("ProductBOM");
                r1.requestField("ProcessSpec");
                r1.requestField("ScheduleQty");
                r1.requestField("BeginProduct");
                r1.requestField("Priority");
                r1.requestField("ScheduleType");
                r1.requestField("ScheduleLimit");
                r1.requestField("ScheduleCanceled");
                r1.requestField("ScheduleCompleted");
                r1.requestField("ScheduleCount");
                r1.requestField("ScheduleQtyCanceled");
                r1.requestField("ScheduleQtyCompleted");
                r1.requestField("OrderStartDate");
                r1.requestField("OrderCompletionDate");
                r1.requestField("Containers");

                //Request field of ChangeHistory SubEntity
                csiRequestField r2 = r1.requestField("ChangeHistory");
                r2.requestField("User");
                r2.requestField("LastChangeDate");

                //Request field of Materials SubEntity
                csiRequestField r3 = r1.requestField("Materials");
                r3.requestField("MaterialLotName");
                r3.requestField("MaterialPart");

                //Submit the input document to the XML Application Server.
                csiDocument ResponseDocument = com.gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "WorkOrderMaint"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "WorkOrderMaint"), true);
                }
                com.PrintDoc(com.gDocument.asXML(), true);
                com.PrintDoc(ResponseDocument.asXML(), false);


                if (com.CheckForErrors(ResponseDocument) == "completion")
                {
                    csiResponseData responseData = ResponseDocument.getService().responseData();
                    csiNamedObject workOrder = responseData.getResponseFieldByName("ObjectChanges").asNamedObject();
                    csiDataField workOrderName = (csiDataField)workOrder.getField("Name");
                    csiDataField qty = (csiDataField)workOrder.getField("Qty");
                    string product = workOrder.getField("Product").asRevisionedObject().getName();
                    string productBOM = workOrder.getField("ProductBOM").asRevisionedObject().getName();
                    string processSpec = workOrder.getField("ProcessSpec").asRevisionedObject().getName();
                    csiDataField scheduleQty = (csiDataField)workOrder.getField("ScheduleQty");
                    string beginProduct = workOrder.getField("BeginProduct").asRevisionedObject().getName();
                    string priority = workOrder.getField("Priority").asNamedObject().getRef();
                    csiDataField scheduleType = (csiDataField)workOrder.getField("ScheduleType");
                    csiDataField scheduleLimit = (csiDataField)workOrder.getField("ScheduleLimit");
                    csiDataField scheduleCancel = (csiDataField)workOrder.getField("ScheduleCanceled");
                    csiDataField scheduleCompleted = (csiDataField)workOrder.getField("ScheduleCompleted");
                    csiDataField scheduleCount = (csiDataField)workOrder.getField("ScheduleCount");
                    csiDataField scheduleQtyCanceled = (csiDataField)workOrder.getField("ScheduleQtyCanceled");
                    csiDataField scheduleQtyCompleted = (csiDataField)workOrder.getField("ScheduleQtyCompleted");
                    csiDataField orderStartDate = (csiDataField)workOrder.getField("OrderStartDate");
                    csiDataField orderCompletionDate = (csiDataField)workOrder.getField("OrderCompletionDate");

                    csiDataList containers = workOrder.getField("Containers").asDataList();
                    string containerList = "";
                    if (containers.hasChildren())
                    {
                        foreach (csiDataField item in containers.getListItems())
                        {
                            string containerName = item.getValue();
                            if (!string.IsNullOrEmpty(containerName))
                                containerList += string.Format(@"<LotID >{0}</LotID>", containerName);
                        }
                    }

                    csiSubentity changeHist = workOrder.getField("ChangeHistory").asSubentity();
                    string user = changeHist.getField("User").asNamedObject().getRef();
                    csiDataField lastChangeDate = (csiDataField)changeHist.getField("LastChangeDate");

                    csiSubentityList materials = workOrder.getField("Materials").asSubentityList();
                    string materialList = "";
                    if (materials.hasChildren())
                    {
                        foreach (csiSubentity item in materials.getListItems())
                        {
                            string materialLotName = item.getField("MaterialLotName").asDataField().getValue();
                            string materialPart = item.getField("MaterialPart").asRevisionedObject().getName();
                            if (!string.IsNullOrEmpty(materialLotName) || !string.IsNullOrEmpty(materialPart))
                                materialList += string.Format(@"<MaterialLotName>{0}</MaterialLotName>
                                                            <MaterialPart>{1}</MaterialPart>", materialLotName,
                                                                                                materialPart);
                        }
                    }

                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                                <responseData>
                                                                <WorkOrder >{0}</WorkOrder>
                                                                <Qty >{1}</Qty>
                                                                <Product >{2}</Product>
                                                                <ProductBOM >{3}</ProductBOM>
                                                                <ProcessSpec >{4}</ProcessSpec>
                                                                <BeginProduct >{5}</BeginProduct>
                                                                <Priority >{6}</Priority>
                                                                <ScheduleType >{7}</ScheduleType>
                                                                <ScheduleLimit >{8}</ScheduleLimit>
                                                                <ScheduleCanceled >{9}</ScheduleCanceled>
                                                                <ScheduleCompleted >{10}</ScheduleCompleted>
                                                                <ScheduleCount >{11}</ScheduleCount>                                                                
                                                                <ScheduleQty >{12}</ScheduleQty>
                                                                <ScheduleQtyCanceled >{13}</ScheduleQtyCanceled>
                                                                <ScheduleQtyCompleted >{14}</ScheduleQtyCompleted>
                                                                <OrderStartDate >{15}</OrderStartDate>
                                                                <OrderCompletionDate >{16}</OrderCompletionDate>
                                                                <LotList >
                                                                {17}
                                                                </LotList>
                                                                <Materials >
                                                                {18}
                                                                </Materials>
                                                                <User >{19}</User>
                                                                <LastChangeDate>{20}</LastChangeDate>
                                                                </responseData>
                                                          </ResultMessage>", workOrderName.getValue(),
                                                                                qty.getValue(),
                                                                                product,
                                                                                productBOM,
                                                                                processSpec,
                                                                                beginProduct,
                                                                                priority,
                                                                                scheduleType.getValue(),
                                                                                scheduleLimit.getValue(),
                                                                                scheduleCancel.getValue(),
                                                                                scheduleCompleted.getValue(),
                                                                                scheduleCount.getValue(),
                                                                                scheduleQty.getValue(),
                                                                                scheduleQtyCanceled.getValue(),
                                                                                scheduleQtyCompleted.getValue(),
                                                                                Convert.ToDateTime(orderStartDate.getValue()),
                                                                                String.IsNullOrEmpty(orderCompletionDate.getValue()) ? (DateTime?)null : Convert.ToDateTime(orderCompletionDate.getValue()),
                                                                                containerList,
                                                                                materialList,
                                                                                user,
                                                                                Convert.ToDateTime(lastChangeDate.getValue())));
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

        [WebMethod(Description = "List Container Hold Reason.")]
        public XmlNode LotHoldReason(string _Lot)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;

            try
            {
                //initial
                com = new Common.func("ViewContainerStatusDoc", "ViewContainerStatus");

                //Create a input object
                csiObject InputData = com.gService.inputData();
                InputData.containerField("Container").setRef(_Lot, "");

                //Create a request object
                csiRequestData requestData = com.gService.requestData();
                requestData.requestField("Container.HoldReason.Name");
                requestData.requestField("Container.HoldReason.Description");

                //Request the completion message from the XML Application Server.
                com.gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                csiDocument ResponseDocument = com.gDocument.submit();

                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "ViewContainerStatus"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "ViewContainerStatus"), true);
                }
                com.PrintDoc(com.gDocument.asXML(), true);
                com.PrintDoc(ResponseDocument.asXML(), false);

                if (com.CheckForErrors(ResponseDocument) == "completion")
                {
                    csiResponseData responseData = ResponseDocument.getService().responseData();
                    string HoldReason = responseData.getResponseFieldByName("Container.HoldReason.Name").asDataField().getValue();
                    string HoldDescription = responseData.getResponseFieldByName("Container.HoldReason.Description").asDataField().getValue();

                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                                <responseData>
                                                                <HoldReason>{0}</HoldReason>
                                                                <Description>{1}</Description>
                                                                </responseData>
                                                              </ResultMessage>",
                                                                                   HoldReason,
                                                                                   HoldDescription));
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


        [WebMethod(Description = "List WorkOrder.")]
        public XmlNode InqueryWorkorder(string _WorkOrder)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;

            try
            {
                //initial
                com = new Common.func("WorkOrderDoc", "WorkOrderMaint");

                //Create a input object
                csiObject InputData = com.gService.inputData();
                // InputData.namedObjectField("ObjectToChange").setRef("TC_WO_UC0002_PA_1");
                InputData.namedObjectField("ObjectToChange").setRef(_WorkOrder);

                //Load the object
                com.gService.perform("Load");

                //Request the completion message from the XML Application Server.
                com.gService.requestData().requestField("CompletionMsg");

                //Create request data fields
                csiRequestData requestData = com.gService.requestData();
                requestData.requestField("ObjectChanges");

                //Submit the input document to the XML Application Server.
                csiDocument ResponseDocument = com.gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "WorkOrderMaint"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "WorkOrderMaint"), true);
                }
                com.PrintDoc(com.gDocument.asXML(), true);
                com.PrintDoc(ResponseDocument.asXML(), false);

                if (com.CheckForErrors(ResponseDocument) == "completion")
                {
                    csiResponseData responseData = ResponseDocument.getService().responseData();
                    if (responseData.getResponseFieldByName("ObjectChanges") == null)
                    {
                        xmlDocumentObject.LoadXml("WorkOrder not found");

                    }
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", com.CheckForErrors(ResponseDocument)));
                    return (xmlDocumentObject);
                }

                return (xmlDocumentObject);
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

        //Txn
        public csiDocument Txn_AddEquipment(string equipmentName, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;

            try
            {
                switch ((int)_equipmentKind)
                {
                    case 1://Item
                        serviceName = "WaferEquipmentMaint";
                        break;
                    case 2://WaferSort
                        serviceName = "WaferSortEquipmentMaint";
                        break;
                    case 3://BackGrind
                        serviceName = "BackGrindEquipmentMaint";
                        break;
                    case 4://Assembly
                        serviceName = "AssemblyEquipmentMaint";
                        break;
                    case 5://Test
                        serviceName = "TestEquipmentMaint";
                        break;
                }

                //initial
                com = new Common.func(serviceName + "Doc", serviceName);
                com.gService.perform("New");

                //Create a input object
                csiObject InputData = com.gService.inputData();

                //Set the Container field.
                csiNamedObject csiObj = InputData.namedObjectField("ObjectChanges");
                csiObj.dataField("Notes").setValue("TEST_NOTE");
                csiObj.dataField("Description").setValue("TEST_DESC");
                csiObj.dataField("Name").setValue(equipmentName);
                csiObj.namedObjectField("SetupAccess").dataField("__name").setValue("Approved");
                csiObj.namedObjectField("BOM").dataField("__useROR").setValue("false");
                csiObj.namedObjectField("DocumentSet").dataField("__name").setValue("Camstar");
                csiObj.namedObjectField("InitialStatus").dataField("__name").setValue("SBY");
                csiObj.namedObjectField("ResourceIcon").dataField("__name").setValue("T1");
                csiObj.namedObjectField("ResourceFamily").dataField("__name").setValue("WST_OVEN");
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
                com.gService.setExecute();

                //Request the completion message from the XML Application Server.
                com.gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = com.gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "NewEquipment" + _equipmentKind.ToString()), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "NewEquipment" + _equipmentKind.ToString()), true);
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

        public csiDocument Txn_DeleteEquipment(string equipmentName, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;

            try
            {
                switch ((int)_equipmentKind)
                {
                    case 1://Item
                        serviceName = "WaferEquipmentMaint";
                        break;
                    case 2://WaferSort
                        serviceName = "WaferSortEquipmentMaint";
                        break;
                    case 3://BackGrind
                        serviceName = "BackGrindEquipmentMaint";
                        break;
                    case 4://Assembly
                        serviceName = "AssemblyEquipmentMaint";
                        break;
                    case 5://Test
                        serviceName = "TestEquipmentMaint";
                        break;
                }

                //initial
                com = new Common.func(serviceName + "Doc", serviceName);
                com.gService.perform("Delete");

                //Create a input object
                csiObject InputData = com.gService.inputData();


                //Set the Container field.
                csiNamedObject csiObj_1 = InputData.namedObjectField("ObjectChanges");

                csiNamedObject csiObj_2 = InputData.namedObjectField("ObjectToChange");
                csiObj_2.dataField("__name").setValue(equipmentName);


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
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "DeleteEquipment" + _equipmentKind.ToString()), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "DeleteEquipment" + _equipmentKind.ToString()), true);
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

        public csiDocument Txn_EquipmentResourceControl(string equipmentName, string toolPlan, Common.func.equipmentKind _equipmentKind)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            string serviceName = string.Empty;

            try
            {
                switch ((int)_equipmentKind)
                {
                    case 1://Item
                        serviceName = "WaferEquipmentMaint";
                        break;
                    case 2://WaferSort
                        serviceName = "WaferSortEquipmentMaint";
                        break;
                    case 3://BackGrind
                        serviceName = "BackGrindEquipmentMaint";
                        break;
                    case 4://Assembly
                        serviceName = "AssemblyEquipmentMaint";
                        break;
                    case 5://Test
                        serviceName = "TestEquipmentMaint";
                        break;
                }

                //initial
                com = new Common.func(serviceName + "Doc", serviceName);
                

                //Create a input object
                csiObject InputData = com.gService.inputData();


                //Set the Container field.
                csiNamedObject csiObj_1 = InputData.namedObjectField("ObjectToChange");
                csiObj_1.dataField("__name").setValue(equipmentName);

                //perform
                com.gService.perform("Load");

                csiObject InputData2 = com.gService.inputData();
                csiNamedObject csiObj_2 = InputData2.namedObjectField("ObjectChanges");
                csiObj_2.namedObjectField("ToolPlan").dataField("__name").setValue(toolPlan);

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
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "EquipmentResourceControl" + _equipmentKind.ToString()), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "EquipmentResourceControl" + _equipmentKind.ToString()), true);
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
