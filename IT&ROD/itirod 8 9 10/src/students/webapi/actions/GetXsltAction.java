package students.webapi.actions;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.OutputStream;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import students.repository.XmlJdbcAdapter;
import students.xml.util.StudentXsltUtil;

public class GetXsltAction implements IAction {

	private static Logger log = Logger.getLogger(GetXsltAction.class);
	private final static String XSL_URI = "/WEB-INF/xsl/group.xsl";

	@Override
	public String perform(HttpServletRequest request,
			HttpServletResponse response) {
		try {
			XmlJdbcAdapter adapter = new XmlJdbcAdapter();
			ByteArrayOutputStream outXmlData = adapter.getXmlDataFromDatabase();
			ByteArrayInputStream inXmlData = new ByteArrayInputStream(
					outXmlData.toByteArray());
			String xslPath = request.getServletContext().getRealPath("/")
					+ XSL_URI;
			StudentXsltUtil util = new StudentXsltUtil(xslPath);
			OutputStream responseOut = response.getOutputStream();
			ByteArrayOutputStream s = util.toHtmlStream(inXmlData);
			byte[] htmlData = s.toByteArray();
			responseOut.write(htmlData, 0, htmlData.length);
			responseOut.flush();
			s.close();
			outXmlData.close();
			inXmlData.close();
		} catch (Exception ex) {
			log.error(ex);
		}
		return null;
	}

	@Override
	public void writeToResponseStream(HttpServletResponse response,
			String output) {

	}

}
