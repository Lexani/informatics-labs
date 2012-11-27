package students.webapi.actions;

import java.io.ByteArrayOutputStream;
import java.io.OutputStream;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import students.repository.XmlJdbcAdapter;
import students.webapi.UrlBuilder;

public class GetXmlAction implements IAction {
	private static Logger log = Logger.getLogger(GetXmlAction.class);

	@Override
	public String perform(HttpServletRequest request,
			HttpServletResponse response) {
		String url = UrlBuilder.buildUrl(UrlBuilder.URI_ACT_GETALL);
		try {
			response.setContentType("text/xml");
			// uncomment the code below to save .xml
			// response.addHeader("Content-Disposition",
			// "form-data; filename=students.xml");
			XmlJdbcAdapter adapter = new XmlJdbcAdapter();
			ByteArrayOutputStream out = adapter.getXmlDataFromDatabase();
			byte[] data = out.toByteArray();
			response.setContentLength(data.length);
			OutputStream os = response.getOutputStream();
			os.write(data, 0, data.length);
			os.flush();
		} catch (Exception e) {
			log.error(e);
			url = UrlBuilder.URI_PAGE_ERROR;
		}
		return url;
	}

	@Override
	public void writeToResponseStream(HttpServletResponse response,
			String output) {
	}

}
