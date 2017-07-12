<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModulePortal.aspx.cs" Inherits="WebApplication2.ModulePortal" %>

<!DOCTYPE html>
  
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset=utf-8>
  <title>Test Module</title>
</head>

<body>
<form id="form2" runat="server">

<asp:button runat="server" onclick="StartModule" type="button" Text="Start Module"></asp:button>
<asp:button runat="server" onclick="AddModule" type="button" Text="Add Module"></asp:button>

<p>Module Count: <%= countMsg %></p>

</script> 

</form>
</body>
  
</html>