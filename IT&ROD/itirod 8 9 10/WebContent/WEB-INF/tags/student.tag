<%@ tag language="java" pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ attribute name="name" required="true" type="java.lang.String"
	description="Name attribute"%>
<%@ attribute name="age" required="true" type="java.lang.Integer"
	description="Average mark attribute"%>
<%@ attribute name="avgMark" required="true" type="java.lang.Double"
	description="Age attribute"%>

<td class="student-name">
	<c:out value="${name}"></c:out>
</td>
<td class="student-age">
	<c:out value="${age}"></c:out>
</td>
<td class="student-avgmark">
	<c:out value="${avgMark}"></c:out>
</td>