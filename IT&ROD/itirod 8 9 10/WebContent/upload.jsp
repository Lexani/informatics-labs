<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>Upload - Students Api</title>
</head>
<body style="text-align: center;">
	<form enctype="multipart/form-data" method="POST" action="upload">
		<div style="background-color: yellow;">
			File:&nbsp;&nbsp;&nbsp;<input type="file" name="xmluploader"
				size="20" />
			<p>
				<input type="submit" value="Upload!">
			</p>
		</div>

	</form>
</body>
</html>