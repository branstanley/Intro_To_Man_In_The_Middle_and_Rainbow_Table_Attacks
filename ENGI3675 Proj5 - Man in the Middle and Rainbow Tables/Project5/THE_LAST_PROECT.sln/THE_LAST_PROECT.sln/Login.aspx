<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link rel="stylesheet" type="text/css" href="./CSS/table.css" />
	<script type="text/javascript" src="./JavaScript/MD5.js" defer="defer"></script>
	<script type="text/javascript" defer="defer">
		function hashAndSend() {
			var pass = document.getElementById('passBox');

			pass.value = calcMD5(pass.value);

			document.getElementById("loginForm").submit();
		}
	</script>
</head>
<body>
    <form id="loginForm" runat="server" method="post" action="http://brannovations.comze.com/login.php">
    <div class="table">
		<div class="row">
			<div class="headCell">
				User Name
			</div>
			<div class="headCell">
				Password
			</div>
			<div class="headCell">
				
			</div>
		</div>
		<div class="row">
			<div class="cell">
				<input type="text" id="userBox" name="userBox"/>
			</div>
			<div class="cell">
				<input type="password" id="passBox" name="passBox"/>
			</div>
			<div class="cell">
				<input type="button" value="Login" onclick="hashAndSend()" />
			</div>
		</div>
    </div>
    </form>
</body>
</html>
