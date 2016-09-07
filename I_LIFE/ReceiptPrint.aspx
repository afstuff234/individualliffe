﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReceiptPrint.aspx.vb" Inherits="I_LIFE_ReceiptPrint" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="/CrystalReportViewers10/css/default.css" type="text/css" rel="Stylesheet" />
    <link href="/CrystalReportWebFormViewer4/css/default.css" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <table>
    <tr><td><asp:Button runat="server" ID="butClose" Text="Close Page" /></td><td></td></tr>
    
    </table>



    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" />
    </div>
    <div>
    <asp:Button ID="butView" runat="server" Text="View Report" />
    </div>
    </form>
</body>
</html>
