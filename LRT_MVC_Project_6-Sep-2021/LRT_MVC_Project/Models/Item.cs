using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace LRT_MVC_Project.Models
{
    public class QRCodeModel
    {
        [Display(Name = "QRCode Text")]
        public string QRCodeText { get; set; }
        [Display(Name = "QRCode Image")]
        public string QRCodeImagePath { get; set; }
    }
    public class Drives
    {
        public string DriveName { get; set; }
    }
    public class Item
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int UOM_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int UOM_Group_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Valuation_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Sub_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Manage_Code_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Valuation_Method_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Material_Category_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Description { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Manufacture { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Brand { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Supplier_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public decimal Item_Price { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public decimal Item_Min_Order_Qty { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Threshold { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_HS_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_URL_Link { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_CoCountry_of_Origin { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Manufacturing_Part_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public decimal Item_Shipping_Cost { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Contact_Details { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_EMail { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_We_Chat { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_WhatsApp { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string item_Other_Contact_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Data_Sheet_Url { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Compliance_Url { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string item_CAD_File_Url { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Certification_Url { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Scrapping_from_WebUrl { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Images_Url { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string item_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Internal_Item_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Type_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UOM_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UOM_Group_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Valuation_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Sub_Type_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Manage_Item_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Material_Category_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Item_Type_Field_Options { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Item_Type_Field_values { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Item_Type_Field_UOM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Search_Value { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Part_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public int User_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Label1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Count { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Bar_Code_No { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Price_Str { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Search_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Approved_Count { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Reject_Count { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Pending_Count { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Scrap_Url { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Scrap_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Image_Path { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_URLS { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_QRCode_path { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Currency_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Currency_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Request_Description { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Request_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DriveName { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FolderName { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Alternate_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_Manufacturing_Part_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_Item_Brand { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_Item_Url { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_UOM_Group_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_UOM_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string HS_Category_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Alt_UOM_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Alt_UOM_Group_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int HS_Category_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Rack_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Rack_Number_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Store_Location_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public decimal Item_Quantity { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Store_Location_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Store_Location_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Rack_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Rack_Number_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sort_Item_Desctiption { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Scrap_Size { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Designer_URL { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Cell_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Cell_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Quntity_str { get; set; }
        #region//------------------webscrapping----------------------------------------
        decimal start, end, rowcounter;
        public System.Data.DataTable GetScrapData(Item objscrap)
        {
            string[,] outputarray = null;
            System.Data.DataTable table = null;
            if (objscrap.Item_URLS == "https://my.mouser.com/")
            {
                start = 1;
                end = 100;
                string url = objscrap.Item_URL_Link.Trim().ToString();
                decimal d = (end - start) / 29;
                int pages = (int)Math.Ceiling(d);
                var wc = new WebClient();
                int rowcounter = 1;
                wc.Headers.Add("user-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
                outputarray = new string[(int)end + 1, 7];
                outputarray[0, 0] = "PartNo";
                outputarray[0, 1] = "Description";
                outputarray[0, 2] = "Manufacturer";
                outputarray[0, 3] = "Price";
                outputarray[0, 4] = "Brand";
                outputarray[0, 5] = "Image";
                outputarray[0, 6] = "Size";
                string html = "";
                table = new System.Data.DataTable();
                var targeturl = new Uri(url);
                try
                {
                    html = wc.DownloadString(targeturl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (html != "")
                {
                    var document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(html);
                    HtmlNode node = document.DocumentNode;
                    string partno="", description="", manufacturer="", price="", brand="", imagesrc="";
                    if (objscrap.Scrap_Type == "multiple")
                    {
                        List<HtmlNode> list = new List<HtmlNode>();
                        List<HtmlNode> list1 = new List<HtmlNode>();
                        try { 
                        list = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("search-table-wrapper"))
                            .FirstOrDefault()
                            .Descendants("table")
                            .FirstOrDefault()
                            .Descendants("tbody")
                            .FirstOrDefault()
                            .Descendants("tr")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals(" even-row"))
                            .ToList();
                        }
                        catch (Exception ex) { }
                        try { 
                        list1 = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("search-table-wrapper"))
                            .FirstOrDefault()
                            .Descendants("table")
                            .FirstOrDefault()
                            .Descendants("tbody")
                            .FirstOrDefault()
                            .Descendants("tr")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals(" "))
                            .ToList();
                        }
                        catch (Exception ex) { }
                        if (list.Concat(list1).ToList().Count > 0)
                        {
                            DataRow dr = null;
                            table.Columns.Add(new DataColumn("partno", typeof(string)));
                            table.Columns.Add(new DataColumn("description", typeof(string)));
                            table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                            table.Columns.Add(new DataColumn("price", typeof(string)));
                            table.Columns.Add(new DataColumn("brand", typeof(string)));
                            table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                            table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                            foreach (HtmlNode n in list.Concat(list1).ToList())
                            {
                                if (rowcounter <= end)
                                {
                                    try { 
                                    partno = n.Descendants("td")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("column part-column hide-xsmall"))
                                        .FirstOrDefault()
                                        .Descendants("label")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("mpart-number-lbl"))
                                        .FirstOrDefault()
                                        .InnerText.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    description = n.Descendants("td")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("column desc-column hide-xsmall"))
                                        .FirstOrDefault()
                                        .Descendants("span")
                                        .FirstOrDefault()
                                        .InnerHtml.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    manufacturer = n.Descendants("td")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("column mfr-column hide-xsmall"))
                                        .FirstOrDefault()
                                        .Descendants("a")
                                        .FirstOrDefault()
                                        .InnerHtml.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    price = n.Descendants("tr")
                                        .FirstOrDefault()
                                        .Descendants("td")
                                        .FirstOrDefault()
                                        .Descendants("span")
                                        .FirstOrDefault()
                                        .InnerText.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    brand = n.Descendants("td")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("column mfr-column hide-xsmall"))
                                        .FirstOrDefault()
                                        .Descendants("a")
                                        .FirstOrDefault()
                                        .InnerHtml.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    imagesrc = n.Descendants("td")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("column text-center image-column collapse-for-xs"))
                                        .FirstOrDefault()
                                        .Descendants("div")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("image-detail text-center"))
                                        .FirstOrDefault()
                                        .Descendants("img")
                                        .FirstOrDefault()
                                        .OuterHtml.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    //< img class="refine-prod-img" alt="Eagle Plastic Devices 561-27PIF0040" id="lKzi+zRwlYs=" src="/images/essentra/images/27pif0032_SPL.jpg">
                                    //< img class="refine-pr"od-img alt="Eagle Plastic Devices 561-17W11203" id="5wsNXxbYqis=" src="https://www.mouser.in/images/essentra/images/17w11203_SPL.jpg">
                                    outputarray[rowcounter, 0] = partno;
                                    outputarray[rowcounter, 1] = description;
                                    outputarray[rowcounter, 2] = manufacturer;
                                    outputarray[rowcounter, 3] = price.Replace("â‚¹", "");
                                    outputarray[rowcounter, 4] = brand;
                                    outputarray[rowcounter, 5] = imagesrc.Replace("src=\"", "style=\"width: 61%;\" src=\"https://www.mouser.in");
                                    outputarray[rowcounter, 6] = "";
                                    //string [] imagspilt = imagesrc.ToString().Split('=');
                                    //var img = imagspilt[0] + "=" + imagspilt[1] + "=" + imagspilt[2] + "=" + imagspilt[3] + "=" + imagspilt[4] + "=https://www.mouser.in" + imagspilt[5];
                                    //imagspilt[4] += "https://www.mouser.in/"

                                    dr = table.NewRow();
                                    dr["partno"] = outputarray[rowcounter, 0];
                                    dr["description"] = outputarray[rowcounter, 1];
                                    dr["manufacturer"] = outputarray[rowcounter, 2];
                                    dr["price"] = outputarray[rowcounter, 3];
                                    dr["brand"] = outputarray[rowcounter, 4];
                                    dr["imagesrc"] = outputarray[rowcounter, 5];
                                    dr["itemsize"] = outputarray[rowcounter, 6];
                                    table.Rows.Add(dr);
                                    rowcounter += 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = null;
                        table.Columns.Add(new DataColumn("partno", typeof(string)));
                        table.Columns.Add(new DataColumn("description", typeof(string)));
                        table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                        table.Columns.Add(new DataColumn("price", typeof(string)));
                        table.Columns.Add(new DataColumn("brand", typeof(string)));
                        table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                        table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                        try { 
                        partno = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("id", "")
                            .Equals("divMouserPartNum"))
                            .FirstOrDefault()
                            .Descendants("span")
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        description = node.Descendants("span")
                            .Where(x => x.GetAttributeValue("id", "")
                            .Equals("spnDescription"))
                            .FirstOrDefault()
                            .InnerHtml.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        manufacturer = node.Descendants("a")
                            .Where(x => x.GetAttributeValue("id", "")
                            .Equals("lnkManufacturerName"))
                            .FirstOrDefault()
                            .InnerHtml.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        price = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("pdp-pricing-table"))
                            .FirstOrDefault()
                            .Descendants("td")
                            .Where(x => x.GetAttributeValue("headers", "")
                            .Equals("pricebreakqty_5000 unitpricecolhdr"))
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try 
                        { 
                        brand = node.Descendants("a")
                            .Where(x => x.GetAttributeValue("id", "")
                            .Equals("lnkManufacturerName"))
                            .FirstOrDefault()
                            .InnerHtml.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try 
                        { 
                        imagesrc = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("image-container text-center"))
                            .FirstOrDefault()
                            .Descendants("a")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("img-link"))
                            .FirstOrDefault()
                            .Descendants("img")
                            .FirstOrDefault()
                            .OuterHtml.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        List<HtmlNode> siz1 = new List<HtmlNode>();
                        try
                        {
                            siz1 = node.Descendants("table")
                              .Where(x => x.GetAttributeValue("class", "")
                              .Equals("specs-table"))
                                //.FirstOrDefault()?
                                //.Descendants("tbody")
                               .FirstOrDefault()
                              .Descendants("tr")
                                //.Where(x => x.GetAttributeValue("rel", "")
                                //.Equals("nofollow"))

                              .ToList();
                        }
                        catch (Exception ex) { }

                        string sizval = "";
                        try
                        {
                            if (string.IsNullOrEmpty(siz1.Count.ToString()))
                            {

                            }
                            else
                            {
                                for (int j = 1; j < siz1.Count; j++)
                                {
                                    List<HtmlNode> siz2 = new List<HtmlNode>();
                                    try
                                    {
                                        string sizehd = siz1[j].Descendants("td").Where(x => x.GetAttributeValue("class", "")
                                                      .Equals("attr-col")).FirstOrDefault().Descendants("label").FirstOrDefault().InnerText;
                                        string sizev = siz1[j].Descendants("td").Where(x => x.GetAttributeValue("class", "")
                                                      .Equals("attr-value-col")).FirstOrDefault().InnerText;
                                        // siz2 = siz1[j].Descendants("td").ToList();

                                        if (j == 1)
                                        {
                                            sizval = sizehd.Replace("|", " ") +  sizev.Replace("|", " ");
                                        }
                                        else
                                        {
                                            sizval = sizval + "|" + sizehd.Replace("|", " ") + sizev.Replace("|", " ");
                                        }

                                    }
                                    catch (Exception ex)
                                    { }
                                }
                            }
                        }
                        catch (Exception ex)
                        { }

                        //< img class="refine-prod-img" alt="Eagle Plastic Devices 561-27PIF0040" id="lKzi+zRwlYs=" src="/images/essentra/images/27pif0032_SPL.jpg">
                        //< img class="refine-pr"od-img alt="Eagle Plastic Devices 561-17W11203" id="5wsNXxbYqis=" src="https://www.mouser.in/images/essentra/images/17w11203_SPL.jpg">
                        outputarray[rowcounter, 0] = partno;
                        outputarray[rowcounter, 1] = description;
                        outputarray[rowcounter, 2] = manufacturer;
                        outputarray[rowcounter, 3] = price.Replace("â‚¹", "");
                        outputarray[rowcounter, 4] = brand;
                        outputarray[rowcounter, 5] = imagesrc.Replace("src=\"", "style=\"width: 100%;\" src=\"https://www.mouser.in");
                        outputarray[rowcounter, 6] = sizval;
                        //string [] imagspilt = imagesrc.ToString().Split('=');
                        //var img = imagspilt[0] + "=" + imagspilt[1] + "=" + imagspilt[2] + "=" + imagspilt[3] + "=" + imagspilt[4] + "=https://www.mouser.in" + imagspilt[5];
                        //imagspilt[4] += "https://www.mouser.in/"

                        dr = table.NewRow();
                        dr["partno"] = outputarray[rowcounter, 0];
                        dr["description"] = outputarray[rowcounter, 1];
                        dr["manufacturer"] = outputarray[rowcounter, 2];
                        dr["price"] = outputarray[rowcounter, 3];
                        dr["brand"] = outputarray[rowcounter, 4];
                        dr["imagesrc"] = outputarray[rowcounter, 5];
                        dr["itemsize"] = outputarray[rowcounter, 6];
                        table.Rows.Add(dr);
                    }
                }
            }
            if (objscrap.Item_URLS == "https://my.rs-online.com/web/")
            {
                start = 1;
                end = 100;
                string url = objscrap.Item_URL_Link.Trim().ToString();
                decimal d = (end - start) / 29;
                int pages = (int)Math.Ceiling(d);
                var wc = new WebClient();
                int rowcounter = 1;
                wc.Headers.Add("user-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
                outputarray = new string[(int)end + 1, 7];
                outputarray[0, 0] = "PartNo";
                outputarray[0, 1] = "Description";
                outputarray[0, 2] = "Manufacturer";
                outputarray[0, 3] = "Price";
                outputarray[0, 4] = "Brand";
                outputarray[0, 5] = "Image";
                outputarray[0, 6] = "Size";

                string html = "";
                table = new System.Data.DataTable();
                var targeturl = new Uri(url);
                try
                {
                    html = wc.DownloadString(targeturl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (html != "")
                {
                    if (objscrap.Scrap_Type == "multiple")
                    {
                        var document = new HtmlAgilityPack.HtmlDocument();
                        document.LoadHtml(html);
                        HtmlNode node = document.DocumentNode;
                        string partno = "", description = "", manufacturer = "", price = "", brand = "", imagesrc = "";
                        List<HtmlNode> list = new List<HtmlNode>();
                        try
                        {
                            list = node.Descendants("div")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("resultsTable results-table-container"))
                                .FirstOrDefault()
                                .Descendants("table")
                                .FirstOrDefault()
                                .Descendants("tbody")
                                .FirstOrDefault()
                                .Descendants("tr")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("resultRow"))
                                .ToList();
                        }
                        catch (Exception ex) { }
                        try
                        {
                            if (list.Count > 0)
                            {
                                DataRow dr = null;
                                table.Columns.Add(new DataColumn("partno", typeof(string)));
                                table.Columns.Add(new DataColumn("description", typeof(string)));
                                table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                                table.Columns.Add(new DataColumn("price", typeof(string)));
                                table.Columns.Add(new DataColumn("brand", typeof(string)));
                                table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                                table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                                foreach (HtmlNode n in list.ToList())
                                {
                                    if (rowcounter <= end)
                                    {
                                        try
                                        {
                                            partno = n.Descendants("td")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("descriptionCol compareContainer"))
                                                .FirstOrDefault()
                                                .Descendants("a")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("small-link"))
                                                .FirstOrDefault()
                                                .InnerText.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        try
                                        {
                                            description = n.Descendants("td")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("descriptionCol compareContainer"))
                                                .FirstOrDefault()
                                                .Descendants("a")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("product-name"))
                                                .FirstOrDefault()
                                                .InnerText.Trim() ?? "";
                                        }
                                        catch (Exception) { }
                                        try
                                        {
                                            manufacturer = n.Descendants("td")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("column mfr-column hide-xsmall"))
                                                .FirstOrDefault()
                                                .Descendants("a")
                                                .FirstOrDefault()
                                                .InnerHtml.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        try
                                        {
                                            price = n.Descendants("td")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("priceCol"))
                                                .FirstOrDefault()
                                                .Descendants("span")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("col-xs-12 price text-left"))
                                                .FirstOrDefault()
                                                .InnerText.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        try
                                        {
                                            brand = n.Descendants("a")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("small-link"))
                                                .LastOrDefault()
                                                .InnerText.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        try
                                        {
                                            imagesrc = n.Descendants("td")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("imageCol"))
                                                .FirstOrDefault()
                                                .Descendants("div")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("fixedImageColumn"))
                                                .FirstOrDefault()
                                                .Descendants("img")
                                                .FirstOrDefault()
                                                .OuterHtml.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        outputarray[rowcounter, 0] = partno;
                                        outputarray[rowcounter, 1] = description;
                                        outputarray[rowcounter, 2] = manufacturer;
                                        outputarray[rowcounter, 3] = price;
                                        outputarray[rowcounter, 4] = brand;
                                        outputarray[rowcounter, 5] = imagesrc;
                                        outputarray[rowcounter, 6] = "";

                                        dr = table.NewRow();
                                        dr["partno"] = outputarray[rowcounter, 0];
                                        dr["description"] = outputarray[rowcounter, 1];
                                        dr["manufacturer"] = outputarray[rowcounter, 2];
                                        dr["price"] = outputarray[rowcounter, 3];
                                        dr["brand"] = outputarray[rowcounter, 4];
                                        dr["imagesrc"] = outputarray[rowcounter, 5];
                                        dr["itemsize"] = outputarray[rowcounter, 6];
                                        table.Rows.Add(dr);
                                        rowcounter += 1;
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        var document = new HtmlAgilityPack.HtmlDocument();
                        document.LoadHtml(html);
                        HtmlNode node = document.DocumentNode;
                        string partno="", description="", manufacturer="", price="", brand="", imagesrc="";
                        DataRow dr = null;
                        table.Columns.Add(new DataColumn("partno", typeof(string)));
                        table.Columns.Add(new DataColumn("description", typeof(string)));
                        table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                        table.Columns.Add(new DataColumn("price", typeof(string)));
                        table.Columns.Add(new DataColumn("brand", typeof(string)));
                        table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                        table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                        try
                        {
                            partno = node.Descendants("dl")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("sc-hqyNC bCOeCk"))
                                .LastOrDefault()
                                .Descendants("dd")
                                .ElementAtOrDefault(1)
                                //.Descendants("span")
                                //.LastOrDefault()
                                .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try
                        {
                            description = node.Descendants("div")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("sc-jbKcbu iRiDaB"))
                                .FirstOrDefault()
                                .Descendants("h1")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("sc-dnqmqq emQDXz"))
                                .FirstOrDefault()
                                .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try
                        {
                            manufacturer = node.Descendants("dl")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("sc-hqyNC bCOeCk"))
                                .LastOrDefault()
                                .Descendants("dd")
                                .LastOrDefault()
                                //.Descendants("span")
                                //.LastOrDefault()
                                .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try
                        {
                            price = node.Descendants("div")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("sc-ckVGcZ hWseTY"))
                                .LastOrDefault()
                                .Descendants("p")
                                .FirstOrDefault()
                                .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try
                        {
                            brand = node.Descendants("ul")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("keyDetailsLL"))
                                .LastOrDefault()
                                .Descendants("li")
                                .LastOrDefault()
                                .Descendants("span")
                                .LastOrDefault()
                                .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try
                        {
                            imagesrc = node.Descendants("div")
                                .Where(x => x.GetAttributeValue("data-test", "")
                                .Equals("image-carousel"))
                                .FirstOrDefault()
                                .Descendants("script")
                                .FirstOrDefault()
                                .OuterHtml.Trim() ?? "";
                        }
                        catch (Exception ex) {
                            try
                            {
                                imagesrc = node.Descendants("div")
                                                                .Where(x => x.GetAttributeValue("class", "")
                                                                .Equals("slide-item-wrap"))
                                                                .FirstOrDefault()
                                                                .Descendants("div")
                                                                  .Where(x => x.GetAttributeValue("class", "")
                                                                 .Equals("thumbnails-wrap css-b4pym7"))
                                                                .FirstOrDefault()
                                                                  .Descendants("div")
                                                                  .Where(x => x.GetAttributeValue("class", "")
                                                                 .Equals("css-wxdkry"))
                                                                .FirstOrDefault()
                                                                 .Descendants("div")
                                                                  .Where(x => x.GetAttributeValue("class", "")
                                                                 .Equals("css-b7jmoi"))
                                                                .FirstOrDefault()
                                                                .InnerHtml.Trim() ?? "";
                            }
                            catch (Exception ex1) { }
                        }
                        List<HtmlNode> siz1 = new List<HtmlNode>();
                        try
                        {
                            siz1 = node.Descendants("table")
                              .Where(x => x.GetAttributeValue("data-testid", "")
                              .Equals("specification-attributes"))
                                //.FirstOrDefault()
                                //.Descendants("tbody")
                                // .FirstOrDefault()
                                //.Descendants("tr")
                                //.Where(x => x.GetAttributeValue("rel", "")
                                //.Equals("nofollow"))

                              .ToList();
                        }
                        catch (Exception ex) { }
                        string finalsize = "";
                        try
                        {
                            List<HtmlNode> siz2 = new List<HtmlNode>();
                           
                            siz2 = siz1[0].Descendants("tr").ToList();
                            for (int i = 1; i < siz2.Count; i++)
                            {
                                List<HtmlNode> siz3 = new List<HtmlNode>();
                                siz3 = siz2[i].Descendants("td").ToList();
                                if (i == 1)
                                {
                                    finalsize = siz3[0].InnerText + ":" + siz3[1].InnerText;
                                }
                                else
                                {
                                    finalsize = finalsize + "," + siz3[0].InnerText + ":" + siz3[1].InnerText;
                                }
                            }
                        }
                        catch(Exception ex)
                        { }
                        string[] imgspilt = imagesrc.ToString().Split(':');
                        if (imgspilt.Length > 1)
                        {
                            string imgsrc = "<img src=" + imgspilt[1].Replace("largeImageURL", "").Replace(",", "") + "/>";
                            outputarray[rowcounter, 5] = imgsrc;
                        }
                        else
                        {
                            outputarray[rowcounter, 5] = "";
                        }
                        outputarray[rowcounter, 0] = partno;
                        outputarray[rowcounter, 1] = description;
                        outputarray[rowcounter, 2] = manufacturer;
                        outputarray[rowcounter, 3] = price;
                        outputarray[rowcounter, 4] = brand;
                        //outputarray[rowcounter, 4] = brand;
                        outputarray[rowcounter, 6] = finalsize;

                        dr = table.NewRow();
                        dr["partno"] = outputarray[rowcounter, 0];
                        dr["description"] = outputarray[rowcounter, 1];
                        dr["manufacturer"] = outputarray[rowcounter, 2];
                        dr["price"] = outputarray[rowcounter, 3];
                        dr["brand"] = outputarray[rowcounter, 4];
                        dr["imagesrc"] = outputarray[rowcounter, 5];
                        dr["itemsize"] = outputarray[rowcounter, 6];
                        table.Rows.Add(dr);
                        rowcounter += 1;
                    }
                }
            }
            if (objscrap.Item_URLS == "https://www.pemnet.com/")
            {
                start = 1;
                end = 100;
                string url = objscrap.Item_URL_Link.Trim().ToString();
                decimal d = (end - start) / 29;
                int pages = (int)Math.Ceiling(d);
                var wc = new WebClient();
                int rowcounter = 1;
                wc.Headers.Add("user-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
                outputarray = new string[(int)end + 1, 7];
                outputarray[0, 0] = "PartNo";
                outputarray[0, 1] = "Description";
                outputarray[0, 2] = "Manufacturer";
                outputarray[0, 3] = "Price";
                outputarray[0, 4] = "Brand";
                outputarray[0, 5] = "Image";
                outputarray[0, 6] = "Size";

                string html = "";
                table = new System.Data.DataTable();
                var targeturl = new Uri(url);
                try
                {
                    html = wc.DownloadString(targeturl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (html != "")
                {
                    //Console.WriteLine(html);
                    var document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(html);
                    HtmlNode node = document.DocumentNode;
                     string partno="", description="", manufacturer="", price="", brand="", imagesrc="";
                    if (objscrap.Scrap_Type == "multiple")
                    {
                        List<HtmlNode> list = new List<HtmlNode>();
                        List<HtmlNode> list1 = new List<HtmlNode>();
                        try
                        {
                            list = node.Descendants("div")
                                //.Where(x => x.GetAttributeValue("class", "")
                                //.Equals("ui-widget ng-scope"))
                                //.FirstOrDefault()?
                                //.Descendants("div")
                                .Where(x => x.GetAttributeValue("id", "")
                                .Equals("plp-thumbs"))
                                .FirstOrDefault()
                                .Descendants("div")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("ui-widget-content ui-corner-all plp-thumb"))
                                .ToList();
                        }
                        catch (Exception ex) { }
                        if (list.Count > 0)
                        {
                            DataRow dr = null;
                            table.Columns.Add(new DataColumn("partno", typeof(string)));
                            table.Columns.Add(new DataColumn("description", typeof(string)));
                            table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                            table.Columns.Add(new DataColumn("price", typeof(string)));
                            table.Columns.Add(new DataColumn("brand", typeof(string)));
                            table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                            table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                            foreach (HtmlNode n in list.ToList())
                            {
                                if (rowcounter <= end)
                                {
                                    partno = n.Descendants("td")
                                        .FirstOrDefault()
                                        .Descendants("span")
                                        .FirstOrDefault()
                                        .Descendants("a")
                                        .FirstOrDefault()
                                        .InnerText.Trim() ?? "";
                                    description = n
                                        //.Descendants("div")
                                        //.Where(x => x.GetAttributeValue("data-measure", "")
                                        //.Equals("general"))
                                        //.LastOrDefault()?
                                        .InnerText.Trim() ?? "";
                                    manufacturer = n.Descendants("td")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("column mfr-column hide-xsmall"))
                                        .FirstOrDefault()
                                        .Descendants("a")
                                        .FirstOrDefault()
                                        .InnerHtml.Trim() ?? "";
                                    price = n.Descendants("td")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("priceCol"))
                                        .FirstOrDefault()
                                        .Descendants("span")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("col-xs-12 price text-left"))
                                        .FirstOrDefault()
                                        .InnerText.Trim() ?? "";
                                    brand = n.Descendants("a")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("small-link"))
                                        .LastOrDefault()
                                        .InnerText.Trim() ?? "";
                                    imagesrc = n.Descendants("a")
                                        //.Where(x => x.GetAttributeValue("class", "")
                                        //.Equals("small-lin"))
                                        .FirstOrDefault()
                                        .InnerHtml.Trim() ?? "";
                                    outputarray[rowcounter, 0] = partno;
                                    outputarray[rowcounter, 1] = description;
                                    outputarray[rowcounter, 2] = manufacturer;
                                    outputarray[rowcounter, 3] = price;
                                    outputarray[rowcounter, 4] = brand;
                                    outputarray[rowcounter, 5] = imagesrc.Replace("src=\"", "style=\"width: 100%;\" src=\"https://catalog.pemnet.com");
                                    outputarray[rowcounter, 6] = "";

                                    dr = table.NewRow();
                                    dr["partno"] = outputarray[rowcounter, 0];
                                    dr["description"] = outputarray[rowcounter, 1];
                                    dr["manufacturer"] = outputarray[rowcounter, 2];
                                    dr["price"] = outputarray[rowcounter, 3];
                                    dr["brand"] = outputarray[rowcounter, 4];
                                    dr["imagesrc"] = outputarray[rowcounter, 5];
                                    dr["itemsize"] = outputarray[rowcounter, 6];
                                    table.Rows.Add(dr);
                                    rowcounter += 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = null;
                        table.Columns.Add(new DataColumn("partno", typeof(string)));
                        table.Columns.Add(new DataColumn("description", typeof(string)));
                        table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                        table.Columns.Add(new DataColumn("price", typeof(string)));
                        table.Columns.Add(new DataColumn("brand", typeof(string)));
                        table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                        table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                        try { 
                        partno = node.Descendants("td")
                            .FirstOrDefault()
                            .Descendants("span")
                            .FirstOrDefault()
                            .Descendants("a")
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        description = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("plp-item-description"))
                            .FirstOrDefault()
                            .Descendants("b")
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        //description = node.Descendants("nav")
                        //    .Where(x => x.GetAttributeValue("id", "")
                        //    .Equals("plp-product-title"))
                        //    .FirstOrDefault()?
                        //    .Descendants("h1")
                        //    .FirstOrDefault()?
                        //    .InnerText.Trim() ?? "";
                        try { 
                        manufacturer = node.Descendants("td")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("column mfr-column hide-xsmall"))
                            .FirstOrDefault()
                            .Descendants("a")
                            .FirstOrDefault()
                            .InnerHtml.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        price = node.Descendants("td")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("priceCol"))
                            .FirstOrDefault()
                            .Descendants("span")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("col-xs-12 price text-left"))
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        brand = node.Descendants("a")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("small-link"))
                            .LastOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        List<HtmlNode> img1 = new List<HtmlNode>();
                        try { 
                        img1 = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("plp-image-carousel"))
                            .FirstOrDefault()
                            .Descendants("ul")
                            .FirstOrDefault()
                            .Descendants("li").ToList();
                        }
                        catch (Exception ex) { }
                        List<HtmlNode> siz1 = new List<HtmlNode>();
                        try
                        {
                            siz1 = node.Descendants("table")
                              .Where(x => x.GetAttributeValue("class", "")
                              .Equals("plp-table"))
                              .FirstOrDefault()
                              .Descendants("tbody")
                                //.Where(x => x.GetAttributeValue("role", "")
                                //.Equals("tablist"))
                               .FirstOrDefault()
                             .Descendants("tr")
                                //.Where(x => x.GetAttributeValue("rel", "")
                                //.Equals("nofollow"))

                              .ToList();
                        }
                        catch (Exception ex) { }
                        string img2 = "";
                        try
                        {
                            for (int i = 0; i < img1.Count; i++)
                            {

                                string img3 = img1[i].InnerHtml.ToString().Replace(","," ").Replace("src=\"", "style=\"width: 100%;\" src=\"https://catalog.pemnet.com");
                                if (i == 0)
                                {
                                    img2 = img3;
                                }
                                else
                                {
                                    img2 = img2 + "," + img3;
                                }

                            }
                        }
                        catch (Exception ex)
                        { }
                        string sizhd1 = "";
                        string sizval = "";
                        try
                        {
                            if (string.IsNullOrEmpty(siz1.Count.ToString()))
                            {

                            }
                            else
                            {
                                for (int j = 0; j < siz1.Count; j++)
                                {
                                    List<HtmlNode> siz2 = new List<HtmlNode>();
                                    try
                                    {
                                        string sizehd = siz1[j].Descendants("td").FirstOrDefault().Descendants("h2").FirstOrDefault().InnerText;
                                        string sizev = siz1[j].Descendants("td").LastOrDefault().Descendants("span").LastOrDefault().InnerText;
                                        // siz2 = siz1[j].Descendants("td").ToList();

                                        if (j == 0)
                                        {
                                            sizval = sizehd.Replace("|", " ") + ":" + sizev.Replace("|" , " ");
                                        }
                                        else
                                        {
                                            sizval = sizval + "|" + sizehd.Replace("|", " ") + ":" + sizev.Replace("|", " ");
                                        }

                                    }
                                    catch(Exception ex)
                                    { }
                                }
                            }
                        }
                        catch (Exception ex)
                        { }


                        outputarray[rowcounter, 0] = partno;
                        outputarray[rowcounter, 1] = description;
                        outputarray[rowcounter, 2] = manufacturer;
                        outputarray[rowcounter, 3] = price;
                        outputarray[rowcounter, 4] = brand;
                        outputarray[rowcounter, 5] = img2;// imagesrc.Replace("src=\"", "style=\"width: 100%;\" src=\"https://catalog.pemnet.com");
                        outputarray[rowcounter, 6] = sizval;

                        dr = table.NewRow();
                        dr["partno"] = outputarray[rowcounter, 0];
                        dr["description"] = outputarray[rowcounter, 1];
                        dr["manufacturer"] = outputarray[rowcounter, 2];
                        dr["price"] = outputarray[rowcounter, 3];
                        dr["brand"] = outputarray[rowcounter, 4];
                        dr["imagesrc"] = outputarray[rowcounter, 5];
                        dr["itemsize"] = outputarray[rowcounter, 6];
                        table.Rows.Add(dr);

                    }
                }
            }
            if (objscrap.Item_URLS == "https://my.misumi-ec.com/")
            {
                start = 1;
                end = 100;
                string url = objscrap.Item_URL_Link.Trim().ToString();
                decimal d = (end - start) / 29;
                int pages = (int)Math.Ceiling(d);
                var wc = new WebClient();
                int rowcounter = 1;
                wc.Headers.Add("user-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
                outputarray = new string[(int)end + 1, 7];
                outputarray[0, 0] = "PartNo";
                outputarray[0, 1] = "Description";
                outputarray[0, 2] = "Manufacturer";
                outputarray[0, 3] = "Price";
                outputarray[0, 4] = "Brand";
                outputarray[0, 5] = "Image";
                outputarray[0, 6] = "Size";
                string html = "";
                table = new System.Data.DataTable();
                var targeturl = new Uri(url);
                try
                {
                    html = wc.DownloadString(targeturl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (html != "")
                {
                    //Console.WriteLine(html);
                    var document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(html);
                    HtmlNode node = document.DocumentNode;
                    string partno = "", description = "", manufacturer = "", price = "", brand = "", imagesrc = "";
                    if (objscrap.Scrap_Type == "multiple")
                    {
                        List<HtmlNode> list = new List<HtmlNode>();
                        List<HtmlNode> list1 = new List<HtmlNode>();
                        try { 
                        list = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("l-content--filter"))
                            //.Equals("l-content--category"))
                            .FirstOrDefault()
                            .Descendants("ul")
                             .Where(x => x.GetAttributeValue("data-spec", "")
                            .Equals("productlist"))
                            //.Descendants("li")
                            .FirstOrDefault()
                            //.Descendants("li")
                            //.FirstOrDefault()?
                            .Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("m-listAreaUnit__body"))
                            .ToList();
                        }
                        catch (Exception ex) { }
                        if (list.Count > 0)
                        {
                            DataRow dr = null;
                            table.Columns.Add(new DataColumn("partno", typeof(string)));
                            table.Columns.Add(new DataColumn("description", typeof(string)));
                            table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                            table.Columns.Add(new DataColumn("price", typeof(string)));
                            table.Columns.Add(new DataColumn("brand", typeof(string)));
                            table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                            table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                            foreach (HtmlNode n in list.ToList())
                            {
                                if (rowcounter <= end)
                                {
                                    try { 
                                    partno = "";
                                    //n.Descendants("span")
                                    //    .FirstOrDefault()?
                                    //    .InnerText.Trim() ?? "";
                                    description = n
                                         //.Descendants("div")
                                         // .Where(x => x.GetAttributeValue("class", "")
                                         // .Equals("m-panel__flow"))
                                         // .FirstOrDefault()?
                                         .Descendants("p")
                                            .Where(x => x.GetAttributeValue("class", "")
                                           .Equals("mc-name"))
                                           .FirstOrDefault()
                                         .InnerText.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    manufacturer = n
                                       .Descendants("p")
                                          .Where(x => x.GetAttributeValue("class", "")
                                         .Equals("mc-maker"))
                                         .FirstOrDefault()
                                         .InnerText.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    price = n.Descendants("p")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("priceCol"))
                                        .FirstOrDefault()
                                        .Descendants("span")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("col-xs-12 price text-left"))
                                        .FirstOrDefault()
                                        .InnerText.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    brand = n.Descendants("a")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("small-link"))
                                        .LastOrDefault()
                                        .InnerText.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    imagesrc = n.Descendants("div")
                                         .Where(x => x.GetAttributeValue("class", "")
                                         .Equals("m-panel__fixed"))
                                         .FirstOrDefault()
                                         .Descendants("p")
                                          .Where(x => x.GetAttributeValue("class", "")
                                         .Equals("mc-img"))
                                         .FirstOrDefault()
                                         .OuterHtml.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    outputarray[rowcounter, 0] = partno;
                                    outputarray[rowcounter, 1] = description;
                                    outputarray[rowcounter, 2] = manufacturer;
                                    outputarray[rowcounter, 3] = price;
                                    outputarray[rowcounter, 4] = brand;
                                    outputarray[rowcounter, 5] = imagesrc.Replace("src=\"", "style=\"width:100%;\" src=\"");
                                    outputarray[rowcounter, 6] = brand;

                                    dr = table.NewRow();
                                    dr["partno"] = outputarray[rowcounter, 0];
                                    dr["description"] = outputarray[rowcounter, 1];
                                    dr["manufacturer"] = outputarray[rowcounter, 2];
                                    dr["price"] = outputarray[rowcounter, 3];
                                    dr["brand"] = outputarray[rowcounter, 4];
                                    dr["imagesrc"] = outputarray[rowcounter, 5];
                                    dr["itemsize"] = outputarray[rowcounter, 6];
                                    table.Rows.Add(dr);
                                    rowcounter += 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = null;
                        table.Columns.Add(new DataColumn("partno", typeof(string)));
                        table.Columns.Add(new DataColumn("description", typeof(string)));
                        table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                        table.Columns.Add(new DataColumn("price", typeof(string)));
                        table.Columns.Add(new DataColumn("brand", typeof(string)));
                        table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                        table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                        try { 
                        partno = node.Descendants("table")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("m-codeTable"))
                            .FirstOrDefault()
                            .Descendants("td")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("mc-code"))
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        description = node.Descendants("h1")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("m-h1"))
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        manufacturer = node.Descendants("td")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("column mfr-column hide-xsmall"))
                            .FirstOrDefault()
                            .Descendants("a")
                            .FirstOrDefault()
                            .InnerHtml.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        price = node.Descendants("td")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("priceCol"))
                            .FirstOrDefault()
                            .Descendants("span")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("col-xs-12 price text-left"))
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        brand = node.Descendants("a")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("small-link"))
                            .LastOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        List<HtmlNode> img1 = new List<HtmlNode>();
                        try { 
                        img1 = node.Descendants("div")
                           .Where(x => x.GetAttributeValue("class", "")
                           .Equals("m-media--product__img"))
                           .FirstOrDefault()
                           .Descendants("ul")
                           .Where(x => x.GetAttributeValue("class", "")
                           .Equals("photoBox__slider rotateArea"))
                           .FirstOrDefault()
                             .Descendants("a")
                           .ToList();
                        }
                        catch (Exception ex) { }
                        try { 
                        imagesrc = node.Descendants("div")
                          .Where(x => x.GetAttributeValue("class", "")
                          .Equals("m-media--product__img"))
                          .FirstOrDefault()
                          .Descendants("img")
                          .FirstOrDefault()
                          .OuterHtml.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        List<HtmlNode> siz1 = new List<HtmlNode>();
                        try { 
                        siz1 = node.Descendants("table")
                           .Where(x => x.GetAttributeValue("class", "")
                           .Equals("m-listTable"))
                           .FirstOrDefault()
                           .Descendants("tbody")
                           //.Where(x => x.GetAttributeValue("class", "")
                          // .Equals("photoBox__slider rotateArea"))
                           .FirstOrDefault()
                             .Descendants("tr")
                           .ToList();
                        }
                        catch (Exception ex) { }

                        //string img2 = "";
                        //try
                        //{
                        //    for (int i = 0; i < img1.Count; i++)
                        //    {

                        //        string img3 = img1[i].InnerHtml.ToString().Replace(",", " ").Replace("src=\"", "style=\"width:100%;\" src=\"").Replace("content","my");
                        //        if (i == 0)
                        //        {
                        //            img2 = img3;
                        //        }
                        //        else
                        //        {
                        //            img2 = img2 + "," + img3;
                        //        }

                        //    }
                        //}
                        //catch (Exception ex)
                        //{ }
                        string sizhd1 = "";
                        string sizval = "";
                        try
                        {
                            if (string.IsNullOrEmpty(siz1.Count.ToString()))
                            {

                            }
                            else
                            {
                                for (int j = 0; j < siz1.Count; j++)
                                {
                                    List<HtmlNode> sizhd2 = new List<HtmlNode>();
                                    List<HtmlNode> sizval2 = new List<HtmlNode>();
                                    try
                                    {
                                        sizhd2 = siz1[j].Descendants("th").ToList();
                                        sizval2 = siz1[j].Descendants("td").ToList();
                                        // siz2 = siz1[j].Descendants("td").ToList();
                                        for (int z = 0; z < sizhd2.Count; z++)
                                        {
                                            if (j == 0 && z==0 )
                                            {
                                                sizval = sizhd2[z].InnerText.Replace("|", " ") + ":" + sizval2[z].InnerText.Replace("|", " ");
                                            }
                                            else
                                            {
                                                sizval = sizval + "|" + sizhd2[z].InnerText.Replace("|", " ") + ":" + sizval2[z].InnerText.Replace("|", " ");
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    { }
                                }
                            }
                        }
                        catch (Exception ex)
                        { }

                        outputarray[rowcounter, 0] = partno;
                        outputarray[rowcounter, 1] = description;
                        outputarray[rowcounter, 2] = manufacturer;
                        outputarray[rowcounter, 3] = price;
                        outputarray[rowcounter, 4] = brand;
                        outputarray[rowcounter, 5] =  imagesrc.Replace("src=\"", "style=\"width:100%;\" src=\"");
                        outputarray[rowcounter, 6] = sizval;

                        dr = table.NewRow();
                        dr["partno"] = outputarray[rowcounter, 0];
                        dr["description"] = outputarray[rowcounter, 1];
                        dr["manufacturer"] = outputarray[rowcounter, 2];
                        dr["price"] = outputarray[rowcounter, 3];
                        dr["brand"] = outputarray[rowcounter, 4];
                        dr["imagesrc"] = outputarray[rowcounter, 5];
                        dr["itemsize"] = outputarray[rowcounter, 6];
                        table.Rows.Add(dr);
                    }
                }
            }
            if (objscrap.Item_URLS == "https://www.alibaba.com/")
            {
                start = 1;
                end = 100;
                string url = objscrap.Item_URL_Link.Trim().ToString();
                decimal d = (end - start) / 29;
                int pages = (int)Math.Ceiling(d);
                var wc = new WebClient();
                int rowcounter = 1;
                wc.Headers.Add("user-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
                outputarray = new string[(int)end + 1, 7];
                outputarray[0, 0] = "PartNo";
                outputarray[0, 1] = "Description";
                outputarray[0, 2] = "Manufacturer";
                outputarray[0, 3] = "Price";
                outputarray[0, 4] = "Brand";
                outputarray[0, 5] = "Image";
                outputarray[0, 6] = "Size";
                string html = "";
                table = new System.Data.DataTable();
                var targeturl = new Uri(url);
                try
                {
                    html = wc.DownloadString(targeturl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (html != "")
                {
                    //Console.WriteLine(html);
                    var document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(html);
                    HtmlNode node = document.DocumentNode;
                    string partno="", description="", manufacturer="", price="", brand="", imagesrc="";
                    if (objscrap.Scrap_Type == "multiple")
                    {
                        List<HtmlNode> list = new List<HtmlNode>();
                        List<HtmlNode> list1 = new List<HtmlNode>();
                        try
                        {
                            list = node.Descendants("div")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("l-theme-card-box"))
                                .FirstOrDefault()
                                .Descendants("div")
                                .Where(x => x.GetAttributeValue("class", "")
                               .Equals("m-gallery-product-item-wrap"))
                                // .Equals("grid-col-item-wrapper grid-col-4"))
                                .ToList();
                        }
                        catch (Exception ex) { }
                        try
                        {
                            if (list.Count > 0)
                            {
                                DataRow dr = null;
                                table.Columns.Add(new DataColumn("partno", typeof(string)));
                                table.Columns.Add(new DataColumn("description", typeof(string)));
                                table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                                table.Columns.Add(new DataColumn("price", typeof(string)));
                                table.Columns.Add(new DataColumn("brand", typeof(string)));
                                table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                                table.Columns.Add(new DataColumn("itemsize", typeof(string)));

                                foreach (HtmlNode n in list.ToList())
                                {
                                    if (rowcounter <= end)
                                    {
                                        try
                                        {
                                            partno = n.Descendants("div")
                                                .Where(x => x.GetAttributeValue("data-measure", "")
                                                .Equals("general"))
                                                .FirstOrDefault()
                                                .InnerText.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        try
                                        {
                                            description = n.Descendants("div")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("item-info"))
                                                .FirstOrDefault()
                                                .InnerText.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        try
                                        {
                                            manufacturer = n.Descendants("td")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("column mfr-column hide-xsmall"))
                                                .FirstOrDefault()
                                                .Descendants("a")
                                                .FirstOrDefault()
                                                .InnerHtml.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        try
                                        {
                                            price = n.Descendants("div")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("price"))
                                                .FirstOrDefault()
                                                .InnerText.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        try
                                        {
                                            brand = n.Descendants("a")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("small-link"))
                                                .LastOrDefault()
                                                .InnerText.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        try
                                        {
                                            imagesrc = n.Descendants("div")
                                                .Where(x => x.GetAttributeValue("class", "")
                                                .Equals("offer-image-box"))
                                                .FirstOrDefault()
                                                .Descendants("img")
                                                .FirstOrDefault()
                                                .OuterHtml.Trim() ?? "";
                                        }
                                        catch (Exception ex) { }
                                        outputarray[rowcounter, 0] = partno;
                                        outputarray[rowcounter, 1] = description;
                                        outputarray[rowcounter, 2] = manufacturer;
                                        outputarray[rowcounter, 3] = price;
                                        outputarray[rowcounter, 4] = brand;
                                        outputarray[rowcounter, 5] = imagesrc.Replace("src=\"", "style=\"width:100px;\" src=\"");
                                        outputarray[rowcounter, 6] = "";

                                        dr = table.NewRow();
                                        dr["partno"] = outputarray[rowcounter, 0];
                                        dr["description"] = outputarray[rowcounter, 1];
                                        dr["manufacturer"] = outputarray[rowcounter, 2];
                                        dr["price"] = outputarray[rowcounter, 3];
                                        dr["brand"] = outputarray[rowcounter, 4];
                                        dr["imagesrc"] = outputarray[rowcounter, 5];
                                        dr["itemsize"] = outputarray[rowcounter, 6];
                                        table.Rows.Add(dr);
                                        rowcounter += 1;
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        List<HtmlNode> img1 = new List<HtmlNode>();
                        List<HtmlNode> siz1 = new List<HtmlNode>();
                        DataRow dr = null;
                        table.Columns.Add(new DataColumn("partno", typeof(string)));
                        table.Columns.Add(new DataColumn("description", typeof(string)));
                        table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                        table.Columns.Add(new DataColumn("price", typeof(string)));
                        table.Columns.Add(new DataColumn("brand", typeof(string)));
                        table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                        table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                        try
                        {
                            partno = node.Descendants("div")
                                .Where(x => x.GetAttributeValue("data-measure", "")
                                .Equals("general"))
                                .FirstOrDefault()
                                .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try
                        {
                            description = node.Descendants("div")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("ma-title-wrap"))
                                .FirstOrDefault()
                                .Descendants("h1")
                                .FirstOrDefault()
                                .InnerText.Trim() ?? "";
                           
                           
                        }
                        catch(Exception ex)
                        { 
                        try
                             {
                                description = node.Descendants("div")
                               .Where(x => x.GetAttributeValue("class", "")
                               .Equals("J-ma-title-wrap"))
                               .FirstOrDefault()
                               .Descendants("h1")
                               .FirstOrDefault()
                               .InnerText.Trim() ?? "";
                            }
                        catch (Exception ex1) { }
                        }
                        try
                        {
                            manufacturer = node.Descendants("div")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("column mfr-column hide-xsmall"))
                                .FirstOrDefault()
                                .Descendants("a")
                                .FirstOrDefault()
                                .InnerHtml.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try
                        {
                            price = node.Descendants("div")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("ma-reference-price"))
                                .FirstOrDefault()
                                .Descendants("span")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("ma-ref-price"))
                                .FirstOrDefault()
                                .Descendants("span")
                                .FirstOrDefault()
                                .InnerHtml.Trim() ?? "";
                           
                        }
                        catch (Exception ex) {
                            try
                            {
                                price = node.Descendants("div")
                                                                  .Where(x => x.GetAttributeValue("class", "")
                                                                  .Equals("ma-price-wrap"))
                                                                  .FirstOrDefault()
                                                                  .Descendants("span")
                                                                  .Where(x => x.GetAttributeValue("class", "")
                                                                  .Equals("ma-ref-price"))
                                                                  .FirstOrDefault()
                                    // .Descendants("span")
                                    //.FirstOrDefault()?
                                                                  .InnerHtml.Trim() ?? "";
                            }
                            catch(Exception ex1){}
                        }
                        try
                        {
                            brand = node.Descendants("a")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("small-link"))
                                .LastOrDefault()
                                .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try
                        {
                            img1 = node.Descendants("ul")
                               .Where(x => x.GetAttributeValue("class", "")
                                   //.Equals("main-image-thumb-ul"))
                               .Equals("inav util-clearfix"))
                               .FirstOrDefault()
                              .Descendants("a")
                               .Where(x => x.GetAttributeValue("rel", "")
                               .Equals("nofollow"))

                               .ToList();
                           
                        }
                        catch (Exception ex) { 
                        try
                        {
                            img1 = node.Descendants("ul")
                                                                .Where(x => x.GetAttributeValue("class", "")
                                                                    //.Equals("main-image-thumb-ul"))
                                                                .Equals("main-image-thumb-ul"))
                                                                .FirstOrDefault()
                                                               .Descendants("li")
                                                                .Where(x => x.GetAttributeValue("class", "")
                                                                .Equals("main-image-thumb-item"))

                                                                .ToList();
                        }
                        catch (Exception ex1) { }
                        }
                        try
                        {
                            siz1 = node.Descendants("div")
                               .Where(x => x.GetAttributeValue("id", "")
                               .Equals("skuWrap"))
                               .FirstOrDefault()
                              .Descendants("dl")
                                //.Where(x => x.GetAttributeValue("rel", "")
                                //.Equals("nofollow"))

                               .ToList();
                        }
                        catch (Exception ex) { }
                        string img2 = "";
                        try
                        {
                            for (int i = 0; i < img1.Count; i++)
                            {

                                string img3 = img1[i].InnerHtml.ToString().Replace("src=\"", "style=\"width:100px;\" src=\"");
                                if (i == 0)
                                {
                                    img2 = img3;
                                }
                                else
                                {
                                    img2 = img2 + "," + img3;
                                }

                            }
                        }
                        catch(Exception ex)
                        { }
                        string sizhd1 = "";
                        string sizval = "";
                        try
                        {
                            if(string.IsNullOrEmpty(siz1.Count.ToString()))
                            {
                            
                            }
                            else
                            {
                                for (int j = 0; j < siz1.Count; j++)
                                {
                                    List<HtmlNode> siz2 = new List<HtmlNode>();
                                    string finalsize = "";
                                    string siz2hed = siz1[j].Descendants("dt").FirstOrDefault().InnerText;

                                    siz2 = siz1[j].Descendants("span").Where(x => x.GetAttributeValue("data-role", "").Equals("sku-attr-val")).ToList();
                                    if (siz2.Count == 0)
                                    {
                                        siz2 = siz1[j].Descendants("span").Where(x => x.GetAttributeValue("class", "").Equals("sku-attr-val-frame text-frame")).ToList();
                                    }
                                    for (int z = 0; z < siz2.Count; z++)
                                    {
                                        if (z == 0)
                                        {
                                            finalsize = siz2[z].InnerText;
                                        }
                                        else
                                        {
                                            finalsize = finalsize + "+" + siz2[z].InnerText;
                                        }
                                    }
                                    if (j == 0)
                                    {
                                        //sizhd1 = siz2hed;
                                        sizval = siz2hed + finalsize;
                                    }
                                    else
                                    {
                                        // sizhd1 = sizhd1 + ","+ siz2hed;
                                        sizval = sizval + "," + siz2hed + finalsize;
                                    }
                                }
                            }
                        }
                        catch(Exception ex)
                        { }
                        outputarray[rowcounter, 0] = partno;
                        outputarray[rowcounter, 1] = description;
                        outputarray[rowcounter, 2] = manufacturer;
                        outputarray[rowcounter, 3] = price;
                        outputarray[rowcounter, 4] = brand;
                        outputarray[rowcounter, 5] = img2;
                        outputarray[rowcounter, 6] = sizval;

                        dr = table.NewRow();
                        dr["partno"] = outputarray[rowcounter, 0];
                        dr["description"] = outputarray[rowcounter, 1];
                        dr["manufacturer"] = outputarray[rowcounter, 2];
                        dr["price"] = outputarray[rowcounter, 3];
                        dr["brand"] = outputarray[rowcounter, 4];
                        dr["imagesrc"] = outputarray[rowcounter, 5];
                        dr["itemsize"] = outputarray[rowcounter, 6];
                        table.Rows.Add(dr);
                        rowcounter += 1;
                    }
                }
            }

            //< ul class="inav util-clearfix">
            //                     <li class="item first active play" data-video="true" data-role="item">
            //         <div class="thumb">
            //             <a rel = "nofollow" href="javascript:void(0);">
            //                 <img src = "https://img.alicdn.com/imgextra/i3/6000000005604/O1CN01syD0CI1rGgBziJ9QO_!!6000000005604-0-tbvideo.jpg" alt="Partially threaded carbon steel 12.9 galvanized zinc DIN 933 hexagon head bolt">
            //             </a>
            //         </div>
            //         <span class="arrow"></span>
            //     </li>



            if (objscrap.Item_URLS == "http://www.instarelectrical.com/")
            {
                start = 1;
                end = 100;
                string url = objscrap.Item_URL_Link.Trim().ToString();
                decimal d = (end - start) / 29;
                int pages = (int)Math.Ceiling(d);
                var wc = new WebClient();
                int rowcounter = 1;
                wc.Headers.Add("user-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
                outputarray = new string[(int)end + 1, 7];
                outputarray[0, 0] = "PartNo";
                outputarray[0, 1] = "Description";
                outputarray[0, 2] = "Manufacturer";
                outputarray[0, 3] = "Price";
                outputarray[0, 4] = "Brand";
                outputarray[0, 5] = "Image";
                outputarray[0, 6] = "Size";

                string html = "";
                table = new System.Data.DataTable();
                var targeturl = new Uri(url);
                try
                {
                    html = wc.DownloadString(targeturl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (html != "")
                {
                    var document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(html);
                    HtmlNode node = document.DocumentNode;
                    string partno = "", description = "", manufacturer = "", price = "", brand = "", imagesrc = "";
                    if (objscrap.Scrap_Type == "multiple")
                    {
                        List<HtmlNode> list = new List<HtmlNode>();
                        List<HtmlNode> list1 = new List<HtmlNode>();
                        try
                        {
                            list = node.Descendants("div")
                                .Where(x => x.GetAttributeValue("class", "")
                                .Equals("olty"))
                                .FirstOrDefault()
                                .Descendants("ul")
                                .FirstOrDefault()
                                .Descendants("li")
                                .Where(x => x.GetAttributeValue("id", "")
                                .Equals("linum"))
                                .ToList();
                        }
                        catch (Exception ex) { }
                        if (list.Count > 0)
                        {
                            DataRow dr = null;
                            table.Columns.Add(new DataColumn("partno", typeof(string)));
                            table.Columns.Add(new DataColumn("description", typeof(string)));
                            table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                            table.Columns.Add(new DataColumn("price", typeof(string)));
                            table.Columns.Add(new DataColumn("brand", typeof(string)));
                            table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                            table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                            foreach (HtmlNode n in list.ToList())
                            {
                                if (rowcounter <= end)
                                {
                                    try { 
                                    partno = n.Descendants("div")
                                        .Where(x => x.GetAttributeValue("data-measure", "")
                                        .Equals("general"))
                                        .FirstOrDefault()
                                        .InnerText.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    description = n.Descendants("div")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("pager"))
                                        .FirstOrDefault()
                                        .Descendants("p")
                                        .FirstOrDefault()
                                        .InnerText.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    manufacturer = n.Descendants("div")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("fs-14"))
                                        .FirstOrDefault()
                                        .InnerHtml.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    price = n.Descendants("span")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("price"))
                                        .FirstOrDefault()
                                        .InnerText.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    brand = n.Descendants("a")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("small-link"))
                                        .LastOrDefault()
                                        .InnerText.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    try { 
                                    imagesrc = n.Descendants("div")
                                        .Where(x => x.GetAttributeValue("class", "")
                                        .Equals("img-200"))
                                        .LastOrDefault()
                                        .Descendants("a")
                                        .LastOrDefault()
                                        .Descendants("img")
                                        .LastOrDefault()
                                        .OuterHtml.Trim() ?? "";
                                    }
                                    catch (Exception ex) { }
                                    outputarray[rowcounter, 0] = partno;
                                    outputarray[rowcounter, 1] = description;
                                    outputarray[rowcounter, 2] = manufacturer;
                                    outputarray[rowcounter, 3] = price;
                                    outputarray[rowcounter, 4] = brand;
                                    outputarray[rowcounter, 5] = imagesrc.Replace("src=\"", "style=\"width:100px;\" src=\"http://www.instarelectrical.com/");
                                    outputarray[rowcounter, 6] = "";
                                    //< img src = "static/template/instar-en/images/03_03.png" >
                                    //< img src = "gzyunck/instar-cn/image/20190315_215550_448568.jpg" alt = "Servo Motor (Encoder 2500P/r)" >
                                    //< img src = "http://www.instarelectrical.com/gzyunck/instar-cn/image/20190315_215550_448568.jpg" alt = "Servo Motor (Encoder 2500P/r)" >
                                    //imagesrc.Replace("src=\"", "src=\"http://www.instarelectrical.com") 
                                    //    "<img src=\"http://www.instarelectrical.comstatic/template/instar-en/images/03_03.png\">" 
                                    //< img src = "http://www.instarelectrical.com/static/template/instar-en/images/03_03.png" >
                                    dr = table.NewRow();
                                    dr["partno"] = outputarray[rowcounter, 0];
                                    dr["description"] = outputarray[rowcounter, 1];
                                    dr["manufacturer"] = outputarray[rowcounter, 2];
                                    dr["price"] = outputarray[rowcounter, 3];
                                    dr["brand"] = outputarray[rowcounter, 4];
                                    dr["imagesrc"] = outputarray[rowcounter, 5];
                                    dr["itemsize"] = outputarray[rowcounter, 6];
                                    table.Rows.Add(dr);
                                    rowcounter += 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        List<HtmlNode> img1 = new List<HtmlNode>();
                        List<HtmlNode> siz1 = new List<HtmlNode>();
                        DataRow dr = null;
                        table.Columns.Add(new DataColumn("partno", typeof(string)));
                        table.Columns.Add(new DataColumn("description", typeof(string)));
                        table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                        table.Columns.Add(new DataColumn("price", typeof(string)));
                        table.Columns.Add(new DataColumn("brand", typeof(string)));
                        table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                        table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                        try { 
                        partno = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("data-measure", "")
                            .Equals("general"))
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        description = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("eomt-text"))
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        manufacturer = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("fs-1"))
                            .FirstOrDefault()
                            .InnerHtml.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        price = node.Descendants("span")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("price"))
                            .FirstOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        try { 
                        brand = node.Descendants("a")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("small-link"))
                            .LastOrDefault()
                            .InnerText.Trim() ?? "";
                        }
                        catch (Exception ex) { }
                        //imagesrc = node.Descendants("div")
                        //    .Where(x => x.GetAttributeValue("class", "")
                        //    .Equals("con-fangDaIMg"))
                        //    .FirstOrDefault()?
                        //    .Descendants("img")
                        //    .FirstOrDefault()?
                        //    .OuterHtml.Trim() ?? "";

                        try { 
                        img1 = node.Descendants("ul")
                          .Where(x => x.GetAttributeValue("class", "")
                          //.Equals("main-image-thumb-ul"))
                          .Equals("con-FangDa-ImgList"))
                          .FirstOrDefault()
                         .Descendants("li")
                          //.Where(x => x.GetAttributeValue("rel", "")
                          //.Equals("nofollow"))

                          .ToList();
                        }
                        catch (Exception ex) { }
                        try { 
                        siz1 = node.Descendants("div")
                           .Where(x => x.GetAttributeValue("class", "")
                           .Equals("emyud"))
                           .FirstOrDefault()
                          .Descendants("p")
                           //.Where(x => x.GetAttributeValue("rel", "")
                           //.Equals("nofollow"))

                           .ToList();
                        }
                        catch (Exception ex) { }
                        string img2 = "";
                        try
                        {
                            for (int i = 0; i < img1.Count; i++)
                            {

                                string img3 = img1[i].InnerHtml.ToString().Replace("src=\"", "style=\"width:100px;\" src=\"http://www.instarelectrical.com/");
                                if (i == 0)
                                {
                                    img2 = img3;
                                }
                                else
                                {
                                    img2 = img2 + "," + img3;
                                }

                            }
                        }
                        catch (Exception ex)
                        { }
                        string sizhd1 = "";
                        string sizval = "";
                        try
                        {
                            if (string.IsNullOrEmpty(siz1.Count.ToString()))
                            {

                            }
                            else
                            {
                                for (int j = 0; j < siz1.Count; j++)
                                {
                                   
                                    string sizv = siz1[j].InnerText.Trim().Replace("â— ","").Replace("ï¼š", ":");


                                    if (j == 0)
                                    {

                                        sizval = sizv.Replace("ï½ž", " ~ ");
                                    }
                                    else
                                    {
                                        // sizhd1 = sizhd1 + ","+ siz2hed;
                                        sizval = sizval + "|" + sizv.Replace("ï½ž", " ~ ");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        { }


                        outputarray[rowcounter, 0] = partno;
                        outputarray[rowcounter, 1] = description.Replace("â–¡", "");
                        outputarray[rowcounter, 2] = manufacturer;
                        outputarray[rowcounter, 3] = price;
                        outputarray[rowcounter, 4] = brand;
                        outputarray[rowcounter, 5] = img2;// imagesrc.Replace("src=\"", "style=\"width:100px;\" src=\"http://www.instarelectrical.com/");
                        outputarray[rowcounter, 6] = sizval;
                        //< img src = "static/template/instar-en/images/03_03.png" >
                        //< img src = "gzyunck/instar-cn/image/20190315_215550_448568.jpg" alt = "Servo Motor (Encoder 2500P/r)" >
                        //< img src = "http://www.instarelectrical.com/gzyunck/instar-cn/image/20190315_215550_448568.jpg" alt = "Servo Motor (Encoder 2500P/r)" >
                        //imagesrc.Replace("src=\"", "src=\"http://www.instarelectrical.com") 
                        //    "<img src=\"http://www.instarelectrical.comstatic/template/instar-en/images/03_03.png\">" 
                        //< img src = "http://www.instarelectrical.com/static/template/instar-en/images/03_03.png" >
                        dr = table.NewRow();
                        dr["partno"] = outputarray[rowcounter, 0];
                        dr["description"] = outputarray[rowcounter, 1];
                        dr["manufacturer"] = outputarray[rowcounter, 2];
                        dr["price"] = outputarray[rowcounter, 3];
                        dr["brand"] = outputarray[rowcounter, 4];
                        dr["imagesrc"] = outputarray[rowcounter, 5];
                        dr["itemsize"] = outputarray[rowcounter, 6];
                        table.Rows.Add(dr);
                        rowcounter += 1;
                    }
                }
            }
            if (objscrap.Item_URLS == "https://www.ccmrails.com/")
            {
                start = 1;
                end = 100;
                string url = objscrap.Item_URL_Link.Trim().ToString();
                decimal d = (end - start) / 29;
                int pages = (int)Math.Ceiling(d);
                var wc = new WebClient();
                int rowcounter = 1;
                wc.Headers.Add("user-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
                outputarray = new string[(int)end + 1, 7];
                outputarray[0, 0] = "PartNo";
                outputarray[0, 1] = "Description";
                outputarray[0, 2] = "Manufacturer";
                outputarray[0, 3] = "Price";
                outputarray[0, 4] = "Brand";
                outputarray[0, 5] = "Image";
                outputarray[0, 6] = "Size";

                string html = "";
                table = new System.Data.DataTable();
                var targeturl = new Uri(url);
                try
                {
                    html = wc.DownloadString(targeturl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (html != "")
                {
                    var document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(html);
                    HtmlNode node = document.DocumentNode;
                    string partno = "", description = "", manufacturer = "", price = "", brand = "", imagesrc = "";
                    DataRow dr = null;
                    table.Columns.Add(new DataColumn("partno", typeof(string)));
                    table.Columns.Add(new DataColumn("description", typeof(string)));
                    table.Columns.Add(new DataColumn("manufacturer", typeof(string)));
                    table.Columns.Add(new DataColumn("price", typeof(string)));
                    table.Columns.Add(new DataColumn("brand", typeof(string)));
                    table.Columns.Add(new DataColumn("imagesrc", typeof(string)));
                    table.Columns.Add(new DataColumn("itemsize", typeof(string)));
                    try { 
                    partno = node.Descendants("div")
                        .Where(x => x.GetAttributeValue("data-measure", "")
                        .Equals("general"))
                        .FirstOrDefault()
                        .InnerText.Trim() ?? "";
                    }
                    catch (Exception ex) { }
                    try { 
                    description = node.Descendants("div")
                        .Where(x => x.GetAttributeValue("class", "")
                        .Equals("j-module n j-header "))
                        .FirstOrDefault()
                        .Descendants("h1")
                        .FirstOrDefault()
                        .InnerText.Trim() ?? "";
                        }
                    catch (Exception ex) { }
                    try
                    {
                        manufacturer = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("fs-1"))
                            .FirstOrDefault()
                            .InnerHtml.Trim() ?? "";
                    }
                    catch (Exception ex) { }
                    try { 
                    price = node.Descendants("span")
                        .Where(x => x.GetAttributeValue("class", "")
                        .Equals("price"))
                        .FirstOrDefault()
                        .InnerText.Trim() ?? "";
                    }
                    catch (Exception ex) { }
                    try { 
                    brand = node.Descendants("a")
                        .Where(x => x.GetAttributeValue("class", "")
                        .Equals("small-link"))
                        .LastOrDefault()
                        .InnerText.Trim() ?? "";
                    }
                    catch (Exception ex) { }
                    try { 
                    imagesrc = node.Descendants("div")
                        .Where(x => x.GetAttributeValue("class", "")
                        .Equals("cc-m-hgrid-column"))
                        .FirstOrDefault()
                        .Descendants("img")
                        .FirstOrDefault()
                        .OuterHtml.Trim() ?? "";
                    }
                    catch (Exception ex) { }
                    List<HtmlNode> img1 = new List<HtmlNode>();
                    try { 
                    img1 = node.Descendants("div")
                        .Where(x => x.GetAttributeValue("class", "")
                        .Equals("cc-m-gallery-slider-thumbnails"))
                        .FirstOrDefault()
                        .Descendants("a")
                        .ToList();
                    }
                    catch (Exception ex) { }
                    List<HtmlNode> siz1 = new List<HtmlNode>();
                    try
                    {
                        siz1 = node.Descendants("div")
                            .Where(x => x.GetAttributeValue("class", "")
                            .Equals("j-module n j-table "))
                            .FirstOrDefault()
                            .Descendants("table")
                            .FirstOrDefault()
                            .Descendants("tr")
                            .ToList();
                    }
                    catch (Exception ex) { }
                    string img2 = "";
                    try
                    {
                        for (int i = 0; i < img1.Count; i++)
                        {

                            string img3 = img1[i].InnerHtml.ToString();//.Replace("src=\"", "style=\"width:150px;\" src=\"");
                            if (i == 0)
                            {
                                img2 = img3;
                            }
                            else
                            {
                                img2 = img2 + "|" + img3;
                            }

                        }
                    }
                    catch (Exception ex)
                    { }

                    string sizval = "";
                    try
                    {
                        if (string.IsNullOrEmpty(siz1.Count.ToString()))
                        {

                        }
                        else
                        {
                            for (int j = 0; j < siz1.Count; j++)
                            {
                                List<HtmlNode> siz2 = new List<HtmlNode>();
                                try
                                {
                                    string sizehd = siz1[j].Descendants("td").FirstOrDefault().Descendants("strong").FirstOrDefault().InnerText;
                                    string sizev = siz1[j].Descendants("td").LastOrDefault().Descendants("span").LastOrDefault().InnerText.Replace("â‰¤", "≤");
                                    // siz2 = siz1[j].Descendants("td").ToList();

                                    if (j == 0)
                                    {
                                        sizval = sizehd.Replace("|", " ") +  sizev.Replace("|", " ");
                                    }
                                    else
                                    {
                                        sizval = sizval + "|" + sizehd.Replace("|", " ") +  sizev.Replace("|", " ");
                                    }

                                }
                                catch (Exception ex)
                                { }
                            }
                        }
                    }
                    catch (Exception ex)
                    { }

                    outputarray[rowcounter, 0] = partno;
                    outputarray[rowcounter, 1] = description;
                    outputarray[rowcounter, 2] = manufacturer;
                    outputarray[rowcounter, 3] = price;
                    outputarray[rowcounter, 4] = brand;
                    outputarray[rowcounter, 5] = img2;// imagesrc.Replace("src=\"", "style=\"width:150px;\" src=\"");
                    outputarray[rowcounter, 6] = sizval;
                    //< img src = "static/template/instar-en/images/03_03.png" >
                    //< img src = "gzyunck/instar-cn/image/20190315_215550_448568.jpg" alt = "Servo Motor (Encoder 2500P/r)" >
                    //< img src = "http://www.instarelectrical.com/gzyunck/instar-cn/image/20190315_215550_448568.jpg" alt = "Servo Motor (Encoder 2500P/r)" >
                    //imagesrc.Replace("src=\"", "src=\"http://www.instarelectrical.com") 
                    //    "<img src=\"http://www.instarelectrical.comstatic/template/instar-en/images/03_03.png\">" 
                    //< img src = "http://www.instarelectrical.com/static/template/instar-en/images/03_03.png" >
                    dr = table.NewRow();
                    dr["partno"] = outputarray[rowcounter, 0];
                    dr["description"] = outputarray[rowcounter, 1];
                    dr["manufacturer"] = outputarray[rowcounter, 2];
                    dr["price"] = outputarray[rowcounter, 3];
                    dr["brand"] = outputarray[rowcounter, 4];
                    dr["imagesrc"] = outputarray[rowcounter, 5];
                    dr["itemsize"] = outputarray[rowcounter, 6];
                    table.Rows.Add(dr);
                    rowcounter += 1;
                }
            }
            return table;
        }
        #endregion

        public string InsertItem(Item objItem)
        {
            string i = "";
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_item", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UOM_Id", objItem.UOM_Id);
            cmd.Parameters.AddWithValue("@UOM_Group_Id", objItem.UOM_Group_Id);
            cmd.Parameters.AddWithValue("@Valuation_Id", objItem.Valuation_Id);
            cmd.Parameters.AddWithValue("@Item_Type_Id", objItem.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Item_Sub_Type_Id", objItem.Item_Sub_Type_Id);
            cmd.Parameters.AddWithValue("@Item_Manage_Code_Id", objItem.Item_Manage_Code_Id);
            cmd.Parameters.AddWithValue("@Valuation_Method_Id", objItem.Valuation_Method_Id);
            cmd.Parameters.AddWithValue("@Material_Category_Id", objItem.Material_Category_Id);
            cmd.Parameters.AddWithValue("@Item_Description", Regex.Replace(objItem.Item_Description.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Manufacture", Regex.Replace(objItem.Item_Manufacture.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Brand", Regex.Replace(objItem.Item_Brand.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Supplier_Name", Regex.Replace(objItem.Supplier_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Price", objItem.Item_Price);
            cmd.Parameters.AddWithValue("@Item_Min_Order_Qty", objItem.Item_Min_Order_Qty);
            cmd.Parameters.AddWithValue("@Item_Thres_hold", Regex.Replace(objItem.Item_Threshold.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@item_HS_Code", Regex.Replace(objItem.Item_HS_Code.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@item_URL_Link", Regex.Replace(objItem.Item_URL_Link.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_CoCountryof_Origin", Regex.Replace(objItem.Item_CoCountry_of_Origin.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Manufacturing_Part_Number", Regex.Replace(objItem.Item_Manufacturing_Part_Number.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Shipping_Cost", objItem.Item_Shipping_Cost);
            cmd.Parameters.AddWithValue("@Item_Contact_Details", Regex.Replace(objItem.Item_Contact_Details.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_EMail", Regex.Replace(objItem.Item_EMail.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_WeChat", Regex.Replace(objItem.Item_We_Chat.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@item_Whats_App", Regex.Replace(objItem.Item_WhatsApp.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Other_Contact_Type", Regex.Replace(objItem.item_Other_Contact_Type.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Data_Sheet_Url", Regex.Replace(objItem.Item_Data_Sheet_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Compliance_Url", Regex.Replace(objItem.Item_Compliance_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@item_CADFile_Url", Regex.Replace(objItem.item_CAD_File_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Certification_Url", Regex.Replace(objItem.Item_Certification_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Scrapping_fromWeb_Url", Regex.Replace(objItem.Item_Scrapping_from_WebUrl.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Images_Url", Regex.Replace(objItem.Item_Images_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Status", Regex.Replace(objItem.item_Status.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Field_Option_Type", Regex.Replace(objItem.Sub_Item_Type_Field_Options.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Field_Option_Values", Regex.Replace(objItem.Sub_Item_Type_Field_values.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Bar_Code_No", objItem.Bar_Code_No);
            cmd.Parameters.AddWithValue("@Item_Name", Regex.Replace(objItem.Item_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Scrap_Url", Regex.Replace(objItem.Item_URL_Link.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Scrap_Type", objItem.Scrap_Type);
            cmd.Parameters.AddWithValue("@Item_Scrap_Image", Regex.Replace(objItem.Image_Path.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_QRCode_Path", Regex.Replace(objItem.Item_QRCode_path.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Currency_Id", objItem.Currency_Id);
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Field_UOM", objItem.Sub_Item_Type_Field_UOM);
            cmd.Parameters.AddWithValue("@Designer_URL", objItem.Designer_URL);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.VarChar, 50));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToString(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = "1";
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }
        public int InsertItemAlternate(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_item_alternate1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Item_Id", objItem.Item_Id);
            cmd.Parameters.AddWithValue("@Item_Internal_Code", objItem.Internal_Item_Code);
            cmd.Parameters.AddWithValue("@Alt_Manufacturing_Part_Number", objItem.Alt_Manufacturing_Part_Number);
            cmd.Parameters.AddWithValue("@Alt_Item_Brand", objItem.Alt_Item_Brand);
            cmd.Parameters.AddWithValue("@Alt_Item_URL", objItem.Alt_Item_Url);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }
        public int UpdateItem(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_item", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Id", objItem.Item_Id);
            cmd.Parameters.AddWithValue("@UOM_Id", objItem.UOM_Id);
            cmd.Parameters.AddWithValue("@UOM_Group_Id", objItem.UOM_Group_Id);
            cmd.Parameters.AddWithValue("@Valuation_Id", objItem.Valuation_Id);
            cmd.Parameters.AddWithValue("@Item_Type_Id", objItem.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Item_Sub_Type_Id", objItem.Item_Sub_Type_Id);
            cmd.Parameters.AddWithValue("@Item_Manage_Code_Id", objItem.Item_Manage_Code_Id);
            cmd.Parameters.AddWithValue("@Valuation_Method_Id", objItem.Valuation_Method_Id);
            cmd.Parameters.AddWithValue("@Material_Category_Id", objItem.Material_Category_Id);
            cmd.Parameters.AddWithValue("@Item_Description", Regex.Replace(objItem.Item_Description.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Manufacture", Regex.Replace(objItem.Item_Manufacture.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Brand", Regex.Replace(objItem.Item_Brand.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Supplier_Name", Regex.Replace(objItem.Supplier_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Price", objItem.Item_Price);
            cmd.Parameters.AddWithValue("@Item_Min_Order_Qty", objItem.Item_Min_Order_Qty);
            cmd.Parameters.AddWithValue("@Item_Thres_hold", Regex.Replace(objItem.Item_Threshold.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@item_HS_Code", Regex.Replace(objItem.Item_HS_Code.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@item_URL_Link", Regex.Replace(objItem.Item_URL_Link.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_CoCountryof_Origin", Regex.Replace(objItem.Item_CoCountry_of_Origin.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Manufacturing_Part_Number", Regex.Replace(objItem.Item_Manufacturing_Part_Number.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Shipping_Cost", objItem.Item_Shipping_Cost);
            cmd.Parameters.AddWithValue("@Item_Contact_Details", Regex.Replace(objItem.Item_Contact_Details.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_EMail", Regex.Replace(objItem.Item_EMail.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_WeChat", Regex.Replace(objItem.Item_We_Chat.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@item_Whats_App", Regex.Replace(objItem.Item_WhatsApp.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Other_Contact_Type", Regex.Replace(objItem.item_Other_Contact_Type.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Data_Sheet_Url", Regex.Replace(objItem.Item_Data_Sheet_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Compliance_Url", Regex.Replace(objItem.Item_Compliance_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@item_CADFile_Url", Regex.Replace(objItem.item_CAD_File_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Certification_Url", Regex.Replace(objItem.Item_Certification_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Scrapping_fromWeb_Url", Regex.Replace(objItem.Item_Scrapping_from_WebUrl.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Images_Url", Regex.Replace(objItem.Item_Images_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Item_Status", Regex.Replace(objItem.item_Status.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Field_Option_Type", Regex.Replace(objItem.Sub_Item_Type_Field_Options.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Field_Option_Values", Regex.Replace(objItem.Sub_Item_Type_Field_values.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Bar_Code_No", objItem.Bar_Code_No);
            cmd.Parameters.AddWithValue("@Item_Name", Regex.Replace(objItem.Item_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Currency_Id", objItem.Currency_Id);
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Field_UOM", objItem.Sub_Item_Type_Field_UOM);
            cmd.Parameters.AddWithValue("@Alt_UOM_Id", objItem.Alt_UOM_Id);
            cmd.Parameters.AddWithValue("@Alt_UOM_Group_Id", objItem.Alt_UOM_Group_Id);
            cmd.Parameters.AddWithValue("@HS_Category_Id", objItem.HS_Category_Id);
            cmd.Parameters.AddWithValue("@Item_Quantity", objItem.Item_Quantity);

            cmd.Parameters.AddWithValue("@Rack_Id", objItem.Rack_Id);
            cmd.Parameters.AddWithValue("@Rack_Number_Id", objItem.Rack_Number_Id);
            cmd.Parameters.AddWithValue("@Store_Location_Id", objItem.Store_Location_Id);
            cmd.Parameters.AddWithValue("@Store_Location_Code", objItem.Store_Location_Code);
            cmd.Parameters.AddWithValue("@Designer_URL", objItem.Designer_URL);
            cmd.Parameters.AddWithValue("@Cell_Id", objItem.Cell_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }
        public DataSet GetItem(int Itemid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_for_edit_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int DeleteItem(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_item", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Id", objItem.Item_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }
        public int DeleteAltItem(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_itemalternate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Id", objItem.Item_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }
        public int ItemStatusChange(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_m_item_Inactive_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Id", objItem.Item_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }
        public DataSet GetItemForSearchList(Item ObjItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_for_search_list_Acc_Type_Id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Type_Id", ObjItem.Item_Type_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemForSearchListAccSearchValue(Item ObjItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_for_search_acc_searchValue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchValue", Regex.Replace(ObjItem.Item_Search_Value.Trim(), @"\s+", " "));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetItemForList(int Itemid)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetItemForListForPopup(Item objitemlistview)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_list_for_popup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Id", objitemlistview.Item_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemForListForExport(Item objitemlistview)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_list_for_export", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Id", objitemlistview.Item_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemForListForExportAccInternalcode(Item objItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_list_for_exportAccItercode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_internal_code", objItem.Internal_Item_Code);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemImageForListForPopup(Item objItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_image_list_for_popup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Id", objItem.Item_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemDetailForUpdate(Item objItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_detail_for_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Id", objItem.Item_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemAltDetailForUpdate(Item objItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_itemalternate_for_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Id", objItem.Item_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemForcolumnBarChart()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_count_For_Columan_bar_chart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemStatusForPieChart()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_count_For_pending_approvel_pie_chart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);
              
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public int GetItemApproveStatus(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_item_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Id", objItem.Item_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }

        public DataSet GetSubItemFieldValueForCheckDuplcate(Item objItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_sub_item_field_value_for_ckeck_duplicate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sub_item_Type_Id", objItem.Item_Sub_Type_Id);
                cmd.Parameters.AddWithValue("@sub_item_Type_Field", Regex.Replace(objItem.Sub_Item_Type_Field_values.Trim(), @"\s+", " "));
                cmd.Parameters.AddWithValue("@sub_item_Type_Field_UOM", Regex.Replace(objItem.Sub_Item_Type_Field_UOM.Trim(), @"\s+", " "));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetItemDoc(Item objsearchitemid)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_doc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Id", objsearchitemid.Item_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetItemWithID(Item objsearchitemid)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_Name_with_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@manufacturing_number", Regex.Replace(objsearchitemid.Item_Manufacturing_Part_Number.Trim(), @"\s+", " "));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetSubItemFieldValueForCheckDuplcateUpdate(Item objItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_sub_item_field_value_for_ckeck_duplicate_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Id", objItem.Item_Id);
                cmd.Parameters.AddWithValue("@sub_item_Type_Id", objItem.Item_Sub_Type_Id);
                cmd.Parameters.AddWithValue("@sub_item_Type_Field", Regex.Replace(objItem.Sub_Item_Type_Field_values.Trim(), @"\s+", " "));
                cmd.Parameters.AddWithValue("@sub_item_Type_Field_UOM", Regex.Replace(objItem.Sub_Item_Type_Field_UOM.Trim(), @"\s+", " "));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetSearchManufactureAccKeywords(string keyword)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_Manufacturer_name", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Search_Keywords", keyword);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }
        public DataSet GetItemForCheckDuplcate(Item objItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_ckeck_duplicate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_Type_Id", objItem.Item_Type_Id);
                cmd.Parameters.AddWithValue("@Item_Sub_Type_Id", objItem.Item_Sub_Type_Id);
                cmd.Parameters.AddWithValue("@Item_Manufacturing_Part_Number", Regex.Replace(objItem.Item_Manufacturing_Part_Number.Trim(), @"\s+", " "));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemForCheckDuplcateForUpdate(Item objItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_ckeck_duplicate_for_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_Type_Id", objItem.Item_Type_Id);
                cmd.Parameters.AddWithValue("@Item_Sub_Type_Id", objItem.Item_Sub_Type_Id);
                cmd.Parameters.AddWithValue("@Item_Manufacturing_Part_Number", Regex.Replace(objItem.Item_Manufacturing_Part_Number.Trim(), @"\s+", " "));
                cmd.Parameters.AddWithValue("@Iteme_Id", objItem.Item_Id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemForListPending()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_pending_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetItemForListApproved()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_approved_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //  cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemForListRejected()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_rejected_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //  cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int InsertItemRequest(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_itemrequestforapproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Id", objItem.Item_Id);
            cmd.Parameters.AddWithValue("@Item_Code", objItem.Internal_Item_Code);
            cmd.Parameters.AddWithValue("@Item_MPN", objItem.Item_Manufacturing_Part_Number);
            cmd.Parameters.AddWithValue("@Request_Description", objItem.Item_Request_Description);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }
        public DataSet GetItemRequestForList()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_request_for_edit_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int GetRequestApproveStatus(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_item_request", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Request_Id", objItem.Request_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }
        public DataSet GetItemForListForCompare(Item objItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_list_for_compare", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Id", objItem.Item_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemDetailForRefrance(Item ObjItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_detail_for_search_reference", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchValue", Regex.Replace(ObjItem.Item_Search_Value.Trim(), @"\s+", " "));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetSubItemTypeIdForRefrance(Item ObjItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_sub_item_type_id_for_reference", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchValue", Regex.Replace(ObjItem.Item_Search_Value.Trim(), @"\s+", " "));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemDetailForRefranceAccItemId(Item ObjItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_detail_for_reference_acc_itemid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_Id", ObjItem.Item_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemDetailAccSearchValue(Item ObjItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_Detail_acc_searchValue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchValue", Regex.Replace(ObjItem.Item_Search_Value.Trim(), @"\s+", " "));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetSubItemTypeFieldForScrap(Item ObjItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_subitemfield_for_scrap", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_Sub_Type_Id", ObjItem.Item_Sub_Type_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetItemLocationDetailAccQRCode(Item ObjItem)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_item_location_detail_acc_qrcode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Bar_Code_No", ObjItem.Bar_Code_No);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public int RejectUpdatedItem(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_reject_m_item_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Request_Id", objItem.Request_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }

        public int InsertItemByExcel(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_item_excel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UOM_Name", objItem.UOM_Name);
            cmd.Parameters.AddWithValue("@Item_Type_Name", objItem.Item_Type_Name);
            cmd.Parameters.AddWithValue("@Item_Sub_Type_Name", objItem.Item_Sub_Type_Name);
            cmd.Parameters.AddWithValue("@Item_Description", objItem.Item_Description);
            cmd.Parameters.AddWithValue("@Item_Manufacture_No", objItem.Item_Manufacturing_Part_Number);
            cmd.Parameters.AddWithValue("@Item_Quantity", objItem.Item_Quntity_str);
            cmd.Parameters.AddWithValue("@Item_Rack_No", objItem.Rack_Name);
            cmd.Parameters.AddWithValue("@Item_Bin_no", objItem.Rack_Number_Name);
            cmd.Parameters.AddWithValue("@Item_QRCode_Path", Regex.Replace(objItem.Item_QRCode_path.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Bar_Code_No", objItem.Bar_Code_No);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }
    }
}