<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ASPNETAlugen3_2.aspx.vb" Inherits="ASPNETAlugen3.Form1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>ActiveLock Universal GENerator (ALUGen) for ASP.NET</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:button id="cmdProductCodeGenerator" style="Z-INDEX: 103; LEFT: 64px; POSITION: absolute; TOP: 48px"
				runat="server" Height="23px" Width="189px" Text="Product Code Generator"></asp:button><asp:textbox id="txtLibKey" style="Z-INDEX: 101; LEFT: 147px; POSITION: absolute; TOP: 245px"
				runat="server" Height="158px" Width="427px" BackColor="Control" Font-Names="Courier New" Font-Size="XX-Small" TextMode="MultiLine"></asp:textbox><asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 51px; POSITION: absolute; TOP: 246px" runat="server"
				Width="85px" Font-Names="Microsoft Sans Serif" Font-Size="X-Small">Liberation Key</asp:Label><asp:Button id="Button2" style="Z-INDEX: 104; LEFT: 284px; POSITION: absolute; TOP: 48px" runat="server"
				Height="23px" Width="189px" Text="License Key Generator" ForeColor="ControlDark" BorderStyle="Inset"></asp:Button><asp:Button id="cmdKeyGen" style="Z-INDEX: 105; LEFT: 584px; POSITION: absolute; TOP: 246px"
				runat="server" Height="23px" Width="74px" Text="Generate"></asp:Button><asp:Label id="Label2" style="Z-INDEX: 106; LEFT: 66px; POSITION: absolute; TOP: 220px" runat="server"
				Width="70px" Font-Names="Microsoft Sans Serif" Font-Size="X-Small">User Name</asp:Label><asp:Label id="Label3" style="Z-INDEX: 107; LEFT: 37px; POSITION: absolute; TOP: 192px" runat="server"
				Width="99px" Font-Names="Microsoft Sans Serif" Font-Size="X-Small">Installation Code</asp:Label><asp:TextBox id="txtUser" style="Z-INDEX: 108; LEFT: 147px; POSITION: absolute; TOP: 217px" runat="server"
				Height="21px" Width="427px">Evaluation User</asp:TextBox><asp:TextBox id="txtReqCodeIn" style="Z-INDEX: 109; LEFT: 147px; POSITION: absolute; TOP: 189px"
				runat="server" Height="21px" Width="427px" BackColor="Control">CkV2YWx1YXRpb24gVXNlcg</asp:TextBox><asp:Label id="lblExpiry" style="Z-INDEX: 110; LEFT: 42px; POSITION: absolute; TOP: 165px"
				runat="server" Width="95px" Font-Names="Microsoft Sans Serif" Font-Size="X-Small">Expires on Date</asp:Label><asp:DropDownList id="cmbLicType" style="Z-INDEX: 114; LEFT: 147px; POSITION: absolute; TOP: 135px"
				runat="server" AutoPostBack="true" Height="20px" Width="295px"></asp:DropDownList><asp:TextBox id="txtDays" style="Z-INDEX: 111; LEFT: 147px; POSITION: absolute; TOP: 162px" runat="server"
				Height="21px" Width="138px" BackColor="Control"></asp:TextBox><asp:Label id="lblDays" style="Z-INDEX: 112; LEFT: 292px; POSITION: absolute; TOP: 165px" runat="server"
				Width="75px" Font-Names="Microsoft Sans Serif" Font-Size="X-Small">days</asp:Label><asp:Label id="Label6" style="Z-INDEX: 113; LEFT: 54px; POSITION: absolute; TOP: 138px" runat="server"
				Width="82px" Font-Names="Microsoft Sans Serif" Font-Size="X-Small">License Type</asp:Label><asp:DropDownList id="cmbProds" style="Z-INDEX: 115; LEFT: 147px; POSITION: absolute; TOP: 108px"
				runat="server" autopostback="true" Height="20px" Width="295px"></asp:DropDownList><asp:Label id="Label7" style="Z-INDEX: 116; LEFT: 89px; POSITION: absolute; TOP: 111px" runat="server"
				Width="47px" Font-Names="Microsoft Sans Serif" Font-Size="X-Small">Product</asp:Label><asp:Label id="Label8" style="Z-INDEX: 117; LEFT: 474px; POSITION: absolute; TOP: 111px" runat="server"
				Width="47px" Font-Names="Microsoft Sans Serif" Font-Size="X-Small">Registered Level</asp:Label><asp:DropDownList id="cmbRegisteredLevel" style="Z-INDEX: 118; LEFT: 551px; POSITION: absolute; TOP: 108px"
				runat="server" Height="20px" Width="187px"></asp:DropDownList><asp:TextBox id="txtLibFile" style="Z-INDEX: 119; LEFT: 147px; POSITION: absolute; TOP: 409px"
				runat="server" Height="21px" Width="427"></asp:TextBox><asp:Label id="Label9" style="Z-INDEX: 120; LEFT: 52px; POSITION: absolute; TOP: 412px" runat="server"
				Width="85px" Font-Names="Microsoft Sans Serif" Font-Size="X-Small">Liberation File</asp:Label><asp:Button id="Button3" style="Z-INDEX: 121; LEFT: 584px; POSITION: absolute; TOP: 410px" runat="server"
				Height="21px" Width="22px" Text="..." Font-Bold="True"></asp:Button><asp:Label id="Label10" style="Z-INDEX: 122; LEFT: 64px; POSITION: absolute; TOP: 16px" runat="server"
				Height="21px" Width="450px" BackColor="Blue" Font-Names="Microsoft Sans Serif" Font-Size="Small" Font-Bold="True" ForeColor="White"> ActiveLock Universal GENerator (ALUGen) for ASP.NET</asp:Label><asp:Label id="Label11" style="Z-INDEX: 123; LEFT: 577px; POSITION: absolute; TOP: 66px" runat="server"
				Width="68px" Font-Names="Microsoft Sans Serif" Font-Size="XX-Small" Font-Bold="True" ForeColor="Blue">Activelock 3</asp:Label><IMG style="Z-INDEX: 124; LEFT: 582px; POSITION: absolute; TOP: 13px" alt="" src="small-logo.bmp"></form>
	</body>
</HTML>