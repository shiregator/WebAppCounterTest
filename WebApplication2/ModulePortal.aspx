<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModulePortal.aspx.cs" Inherits="WebApplication2.ModulePortal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Module</title>
</head>

<body>
    <form id="form2" runat="server">

        <asp:Button runat="server" OnClick="StartModule" type="button" Text="Start Module"></asp:Button>
        <asp:Button runat="server" OnClick="AddModule" type="button" Text="Add Module"></asp:Button>
        <p>Module Count: <%= countMsg %></p>



    </form>
</body>

</html>
