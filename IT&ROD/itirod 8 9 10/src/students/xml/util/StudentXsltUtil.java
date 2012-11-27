package students.xml.util;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.InputStream;

import javax.xml.transform.Source;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;

import org.apache.log4j.Logger;

public class StudentXsltUtil {
	private static Logger log = Logger.getLogger(StudentXsltUtil.class);
	private String xsltPath;
	
	public StudentXsltUtil(String xsltPath) {
		this.xsltPath = xsltPath;
	}
	
	public ByteArrayOutputStream toHtmlStream(InputStream inXml) {
		
        TransformerFactory factory = TransformerFactory.newInstance();
        Source xslt = new StreamSource(new File(this.xsltPath));
        
        Transformer transformer = null;
		try {
			transformer = factory.newTransformer(xslt);
			Source text = new StreamSource(inXml);
			
			ByteArrayOutputStream htmlData = new ByteArrayOutputStream();
			transformer.transform(text, new StreamResult(htmlData));
			return htmlData;
			
		} catch (TransformerException ex) {
			log.error(ex);
		}
		return null;
	}
}
