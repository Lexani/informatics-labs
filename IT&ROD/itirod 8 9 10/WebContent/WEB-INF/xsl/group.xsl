<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">
	<xsl:output method="xml" indent="yes"
		doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd" />
	<!--XHTML document outline -->
	<xsl:template match="/">
		<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
			<head>
				<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
				<title>test1</title>
				<style type="text/css">
					h1 { padding: 10px; padding-width: 100%; background-color: silver }
					td, th { width: 40%; border: 1px solid silver; padding: 10px }
					td:first-child, th:first-child { width: 20% }
					table { width: 650px }
				</style>
			</head>
			<body>
				<h1>The following students are registered</h1>
				<xsl:apply-templates />
			</body>
		</html>
	</xsl:template>
	<!--Table headers and outline -->
	<xsl:template match="java/object">
		<table>
			<tr>
				<th>Id</th>
				<th>Name</th>
				<th>Age</th>
				<th>Average Mark</th>
			</tr>
			<xsl:for-each select="void/object">
				<tr />
				<td>
					<xsl:value-of select="./void[@property='id']/int" />
				</td>
				<td>
					<xsl:value-of select="./void[@property='name']/string" />
				</td>
				<td>
					<xsl:value-of select="./void[@property='age']/int" />
				</td>
				<td>
					<xsl:value-of select="./void[@property='averageMark']/double" />
				</td>
				<tr />
			</xsl:for-each>

		</table>
	</xsl:template>

</xsl:stylesheet>