<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="THE_LAST_PROECT.App_Code" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<%
			if (!string.IsNullOrEmpty(Request.Form["userBox"]) && (!string.IsNullOrEmpty(Request.Form["passBox"])))
			{
				if (DatabaseAccess.authenticate_user(Request.Form["userBox"], Request.Form["passBox"]))
					Response.Write("Login Successful, welcome " + Request.Form["userBox"]); // In real life we'd actually do something here...
				else
					Response.Write("Login failed."); // In real life we'd reroute to the login form and display ane error

			}
			else
				Response.Write("Either you browsed here, or your missed one of the two fields, should not be seeing this."); // Probably reroute to the login page instead

		%>
    
    </div>
    </form>
</body>
</html>
