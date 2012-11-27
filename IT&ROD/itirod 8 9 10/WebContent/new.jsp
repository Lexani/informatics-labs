<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>New - Student Api</title>
<style>
h1 {
	padding: 10px;
	padding-width: 100%;
	background-color: silver
}

td,th {
	width: 40%;
	border: 1px solid silver;
	padding: 10px
}

td:first-child,th:first-child {
	width: 20%
}

table {
	width: 650px
}
</style>
</head>

<body>
	<form method="post" action="create">
		<h1>New Student</h1>
		<table>
			<tr>
				<th>Name</th>
				<th>Age</th>
				<th>Mark</th>
			<tr />
			<tr>
				<td><input type="text" name="name"></td>
				<td><input type="text" name="age"></td>
				<td><input type="text" name="averageMark"></td>
			</tr>
		</table>
		<br/>
		<div style="text-align: center;">
			<input style="width: 100px; height: 30px" type="submit" id="btnSave"
				value="save">
		</div>
	</form>
</body>
</html>