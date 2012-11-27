<%@ tag language="java" pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib prefix="tags" tagdir="/WEB-INF/tags"%>

<%@ attribute name="students" required="true" type="java.util.List"
	description="Name attribute"%>

<c:forEach var="student" items="${students}">
	<tr>
		<tags:student avgMark="${student.averageMark}" age="${student.age}"
			name="${student.name}"></tags:student>
		<td><a href="/HelloServlet/delete?id=${student.id}" id="linkDel">delete</a></td>
		<td><a href="/HelloServlet/update?id=${student.id}" id="linkUpd">edit</a></td>
	</tr>
</c:forEach>