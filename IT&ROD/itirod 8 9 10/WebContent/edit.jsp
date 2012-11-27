<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<%@ page import="students.domain.Student"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>Edit - Student Api</title>
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
<jsp:useBean id="student" type="students.domain.Student" scope="session" />
<body>
	<form method="post" action="update?id=${student.id}">
		<h1>Edit Student</h1>
		<table>
			<tr>
				<th>Name</th>
				<th>Age</th>
				<th>Mark</th>
			<tr />
			<tr>
				<td><input type="text" name="name"
					value=<c:out value="${student.name}"></c:out>></td>
				<td><input type="text" name="age"
					value=<c:out value="${student.age}"></c:out>></td>
				<td><input type="text" name="averageMark"
					value=<c:out value="${student.averageMark}"></c:out>></td>
			</tr>
		</table>
		<br />
		<div style="text-align: center;">

			<input style="width: 100px; height: 30px" type="submit" id="btnSave"
				value="save">
		</div>

	</form>

</body>
</html>