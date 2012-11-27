<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<%@page import="java.util.ArrayList"%>
<%@page import="students.domain.Student"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="/WEB-INF/lib/tags/DateTag.tld" prefix="dt"%>
<%@ taglib uri="/WEB-INF/lib/tags/DecoratorTag.tld" prefix="ddt"%>
<%@ taglib prefix="tags" tagdir="/WEB-INF/tags"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>All - Student Api</title>
<style type="text/css">
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

<jsp:useBean id="students"
	type="java.util.ArrayList<students.domain.Student>" scope="session" />
<body>
	<form method="GET">
		<h1>Student List</h1>
		<h2>
			<ddt:decorate>
				<dt:displayDate />
			</ddt:decorate>
		</h2>
		<table>
			<thead>
				<tr>
					<td>Name:</td>
					<td>Age:</td>
					<td>Average mark:</td>
					<td colspan="2">Action</td>
				</tr>
			</thead>
			<tags:group students="${students}"></tags:group>
			<tfoot>
				<tr>
					<td colspan="5" style="text-align: center"><a href="new.jsp" id="linkNew">new</a></td>
				</tr>
			</tfoot>

		</table>
	</form>
</body>
</html>