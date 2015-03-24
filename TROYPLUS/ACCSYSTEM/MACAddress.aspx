<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MACAddress.aspx.cs" Inherits="MACAddress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript">
        var xml;
        if (window.DOMParser) { // Firefox, Chrome, Opera, etc.
            parser = new DOMParser();
            xmlDoc = parser.parseFromString(xml, "text/xml");
        }
        else // Internet Explorer
        {
            xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = false;
            xmlDoc.loadXML(xml);
        }

        function callme() {           
            var locator = new ActiveXObject("WbemScripting.SWbemLocator");
            var service = locator.ConnectServer(".");
            var properties = service.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True");
            var e = new Enumerator(properties);
            alert(e);
            document.write("<table border=1>");
            dispHeading();
            for (; !e.atEnd() ; e.moveNext()) {
                var p = e.item();
                document.write("<tr>");
                document.write("<td>" + p.Caption + "</td>");
                document.write("<td>" + p.IPFilterSecurityEnabled + "</td>");
                document.write("<td>" + p.IPPortSecurityEnabled + "</td>");
                document.write("<td>" + p.IPXAddress + "</td>");
                document.write("<td>" + p.IPXEnabled + "</td>");
                document.write("<td>" + p.IPXNetworkNumber + "</td>");
                document.write("<td>" + p.MACAddress + "</td>");
                document.write("<td>" + p.WINSPrimaryServer + "</td>");
                document.write("<td>" + p.WINSSecondaryServer + "</td>");
                document.write("</tr>");
            }
            document.write("</table>");
        }
        function dispHeading() {
            document.write("<thead>");
            document.write("<td>Caption</td>");
            document.write("<td>IPFilterSecurityEnabled</td>");
            document.write("<td>IPPortSecurityEnabled</td>");
            document.write("<td>IPXAddress</td>");
            document.write("<td>IPXEnabled</td>");
            document.write("<td>IPXNetworkNumber</td>");
            document.write("<td>MACAddress</td>");
            document.write("<td>WINSPrimaryServer</td>");
            document.write("<td>WINSSecondaryServer</td>");
            document.write("</thead>");
        }
    </script>
</head>
<body onload="callme();">
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
