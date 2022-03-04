<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhoneExif.aspx.cs" Inherits="PhoneExifbyNET.PhoneExif" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="ShowOrigialSzieAndXY" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="Resize" OnClick="Button2_Click" />
            <asp:Button ID="Button3" runat="server" Text="ShowResizeXY" OnClick="Button3_Click" />
            <asp:Button ID="Button4" runat="server" Text="WriteXY" OnClick="Button4_Click" />
        </div>
        <div>
            Before：<asp:Label ID="Label1" runat="server"></asp:Label><br />
            After：<asp:Label ID="Label2" runat="server"></asp:Label><br />
            Write：<asp:Label ID="Label3" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
