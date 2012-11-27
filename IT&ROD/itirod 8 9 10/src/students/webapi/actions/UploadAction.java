package students.webapi.actions;

import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.IOException;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import students.repository.XmlJdbcAdapter;
import students.webapi.UrlBuilder;

public class UploadAction implements IAction {
	private static Logger log = Logger.getLogger(UploadAction.class);
	private static final String XML_START_MARKER = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
	private static final String XML_END_MARKER = "</java>";

	@Override
	public String perform(HttpServletRequest request,
			HttpServletResponse response) {
		String url = UrlBuilder.buildUrl(UrlBuilder.URI_ACT_GETALL);
		try {
			String contentType = request.getContentType();

			if ((contentType != null)
					&& (contentType.indexOf("multipart/form-data") >= 0)) {
				DataInputStream in = new DataInputStream(
						request.getInputStream());
				// we are taking the length of Content type data
				int formDataLength = request.getContentLength();
				byte dataBytes[] = new byte[formDataLength];
				int byteRead = 0;
				int totalBytesRead = 0;
				// this loop converting the uploaded file into byte code
				while (totalBytesRead < formDataLength) {
					byteRead = in.read(dataBytes, totalBytesRead,
							formDataLength);
					totalBytesRead += byteRead;
				}
				String file = new String(dataBytes);
				int start = file.indexOf(XML_START_MARKER);
				file = file.substring(start);
				int end = file.indexOf(XML_END_MARKER);
				file = file.substring(0, end + XML_END_MARKER.length());
				ByteArrayInputStream inXml = new ByteArrayInputStream(
						dataBytes, start, file.length());
				XmlJdbcAdapter adapter = new XmlJdbcAdapter();
				adapter.saveXmlDataInDatabase(inXml);
			}
		} catch (IOException e) {
			log.error(e);
			url = UrlBuilder.URI_PAGE_ERROR;
		}

		return url;
	}

	@Override
	public void writeToResponseStream(HttpServletResponse response,
			String output) {
		// TODO Auto-generated method stub

	}

}
