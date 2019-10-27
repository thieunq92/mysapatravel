<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuerySql.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QuerySql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
     
            <% 
                Module.CommonDao.OpenSession().CreateSQLQuery("UPDATE [dbo].[bitportal_user] SET [firstname] = N'Nguyễn', [lastname] = 'Thi', [email] = 'thi@orientalsails.com' WHERE userid=13")
                .ExecuteUpdate();%>   
        </div>
    </form>
</body>
</html>
