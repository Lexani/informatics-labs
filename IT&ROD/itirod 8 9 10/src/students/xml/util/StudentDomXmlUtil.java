package students.xml.util;

import java.beans.XMLEncoder;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.List;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.apache.log4j.Logger;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import students.domain.Student;
import students.webapi.ValidationUtil;

public class StudentDomXmlUtil implements IStudentXmlUtil {

	private static final Logger log = Logger.getLogger(StudentDomXmlUtil.class);

	@Override
	public List<Student> loadStudents(InputStream in) {
		List<Student> group = null;
		try {
			Document doc = this.getDocument(in);
			group = this.parseDocument(doc);
		} catch (Exception ex) {
			log.error(ex);
		}
		return group;
	}

	@Override
	public void saveStudents(OutputStream out, List<Student> students) {
		try {
			XMLEncoder xmlEncoder = new XMLEncoder(out);
			xmlEncoder.writeObject(students);
			xmlEncoder.flush();
			xmlEncoder.close();
		} catch (Exception ex) {
			log.error(ex);
		}
	}

	private Document getDocument(InputStream in) {
		DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
		Document doc = null;
		try {
			DocumentBuilder db = dbf.newDocumentBuilder();
			doc = db.parse(in);
		} catch (ParserConfigurationException pce) {
			log.error(pce);
		} catch (SAXException se) {
			log.error(se);
		} catch (IOException ioe) {
			log.error(ioe);
		}
		return doc;
	}

	private List<Student> parseDocument(Document doc) {
		List<Student> group = new ArrayList<Student>();

		Element root = doc.getDocumentElement();
		Element listObjectNode = (Element) root
				.getElementsByTagName(TAG_OBJECT).item(0);
		NodeList studentNodes = listObjectNode
				.getElementsByTagName(TAG_STUDENT);

		if (studentNodes != null && studentNodes.getLength() > 0) {
			for (int i = 0; i < studentNodes.getLength(); i++) {
				Element studentObjectNode = (Element) studentNodes.item(i);
				Student student = this.getStudent(studentObjectNode);
				group.add(student);
			}
		}
		return group;
	}

	private Student getStudent(Element element) {
		Student student = new Student();
		try {
			NodeList properties = element.getElementsByTagName(TAG_PROPERTY);
			if (properties != null && properties.getLength() > 0) {
				for (int i = 0; i < properties.getLength(); i++) {
					Element property = (Element) properties.item(i);
					String propValue = property.getTextContent();
					propValue = propValue.replaceAll(
							ValidationUtil.TRIM_REGEXP, "");
					this.setStudentPropertyValue(i, propValue, student);
				}
			}
		} catch (Exception ex) {
			log.error(ex);
		}
		return student;
	}

	private void setStudentPropertyValue(int index, String value,
			Student student) {
		switch (index) {
		case PROP_AGE:
			student.setAge(Integer.parseInt(value));
			break;

		case PROP_AVG_MARK:
			student.setAverageMark(Double.parseDouble(value));
			break;

		case PROP_ID:
			student.setId(Integer.parseInt(value));
			break;

		case PROP_NAME:
			student.setName(value);
			break;

		default:
			break;
		}
	}
}
