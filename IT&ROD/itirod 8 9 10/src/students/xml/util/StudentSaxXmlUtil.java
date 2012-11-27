package students.xml.util;

import java.beans.XMLEncoder;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.xml.parsers.ParserConfigurationException;
import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;

import org.apache.log4j.Logger;
import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

import students.domain.Student;

public class StudentSaxXmlUtil implements IStudentXmlUtil {

	private static final Logger log = Logger.getLogger(StudentDomXmlUtil.class);
	private static Map<String, Integer> propNameValue = nameValueMap();

	// private static Map<Integer, String> propValueName = valueNameMap();

	protected static Map<String, Integer> nameValueMap() {
		Map<String, Integer> map = null;
		try {
			map = new HashMap<String, Integer>();
			map.put(ATTR_PROP_STUD_AGE, PROP_AGE);
			map.put(ATTR_PROP_STUD_AVG_MARK, PROP_AVG_MARK);
			map.put(ATTR_PROP_STUD_ID, PROP_ID);
			map.put(ATTR_PROP_STUD_NAME, PROP_NAME);
		} catch (Exception ex) {
			log.error(ex);
		}
		return map;
	}

	protected static Map<Integer, String> valueNameMap() {
		Map<Integer, String> map = null;
		try {
			map = new HashMap<Integer, String>();
			map.put(PROP_AGE, ATTR_PROP_STUD_AGE);
			map.put(PROP_AVG_MARK, ATTR_PROP_STUD_AVG_MARK);
			map.put(PROP_ID, ATTR_PROP_STUD_ID);
			map.put(PROP_NAME, ATTR_PROP_STUD_NAME);
		} catch (Exception ex) {
			log.error(ex);
		}
		return map;
	}

	@Override
	public List<Student> loadStudents(InputStream in) {
		List<Student> group = null;
		try {
			group = this.parseDocument(in);
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

	private List<Student> parseDocument(InputStream in) {

		SAXParserFactory spf = SAXParserFactory.newInstance();
		List<Student> group = null;
		try {
			SAXParser sp = spf.newSAXParser();
			ParserHelper helper = new ParserHelper();
			sp.parse(in, helper);
			group = helper.getGroup();
		} catch (SAXException se) {
			log.error(se);
		} catch (ParserConfigurationException pce) {
			log.error(pce);
		} catch (IOException ie) {
			log.error(ie);
		}
		return group;
	}

	private class ParserHelper extends DefaultHandler {

		private Student tempStudent;
		private String tempValue;
		private int parsingPropertyId = PROP_INVALID;
		private List<Student> group;

		public ParserHelper() {
			this.group = new ArrayList<Student>();
			this.tempValue = "";
		}

		public List<Student> getGroup() {
			return group;
		}

		public void startElement(String uri, String localName, String qName,
				Attributes attributes) throws SAXException {
			tempValue = "";
			String attrClassValue = attributes.getValue("class");
			if (qName.equalsIgnoreCase(TAG_OBJECT)
					&& attrClassValue.equalsIgnoreCase(ATTR_STUD_CLASS)) {
				tempStudent = new Student();
				return;
			}

			if (qName.equalsIgnoreCase(TAG_PROPERTY)) {
				String attrPropValue = attributes.getValue(ATTR_STUD_PROP);
				if (attrPropValue != null) {
					this.parsingPropertyId = propNameValue.get(attrPropValue);
				}
			}
		}

		public void characters(char[] ch, int start, int length)
				throws SAXException {
			tempValue = new String(ch, start, length);
		}

		public void endElement(String uri, String localName, String qName)
				throws SAXException {

			if (qName.equalsIgnoreCase(TAG_OBJECT) && tempStudent != null) {
				group.add(tempStudent);
				tempStudent = null;
				return;
			}
			this.tryParseProperty(qName, this.tempStudent);
		}

		private boolean tryParseProperty(String qName, Student student) {
			if (this.parsingPropertyId == PROP_INVALID) {
				return false;
			}
			switch (this.parsingPropertyId) {

			case PROP_AGE: {
				student.setAge(Integer.parseInt(tempValue));
				this.parsingPropertyId = PROP_INVALID;
				return true;
			}

			case PROP_AVG_MARK: {
				student.setAverageMark(Double.parseDouble(tempValue));
				this.parsingPropertyId = PROP_INVALID;
				return true;
			}

			case PROP_ID: {
				student.setId(Integer.parseInt(tempValue));
				this.parsingPropertyId = PROP_INVALID;
				return true;
			}

			case PROP_NAME: {
				student.setName(tempValue);
				this.parsingPropertyId = PROP_INVALID;
				return true;
			}

			default: {
				return false;
			}

			}
		}
	}
}
