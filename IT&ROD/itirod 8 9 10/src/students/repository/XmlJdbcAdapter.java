package students.repository;

import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.util.List;

import org.apache.log4j.Logger;

import students.domain.Student;
import students.xml.util.StudentSaxXmlUtil;

public class XmlJdbcAdapter {
	private static Logger log = Logger.getLogger(XmlJdbcAdapter.class);
	private JdbcStudentDao jdbcDao;
	private XmlStudentDao xmlDao;

	public XmlJdbcAdapter() {
		this.jdbcDao = new JdbcStudentDao();
		this.xmlDao = new XmlStudentDao();
		this.xmlDao.setXmlUtil(new StudentSaxXmlUtil());
	}

	public JdbcStudentDao getJdbcDao() {
		return jdbcDao;
	}

	public void setJdbcDao(JdbcStudentDao jdbcDao) {
		this.jdbcDao = jdbcDao;
	}

	public XmlStudentDao getXmlDao() {
		return xmlDao;
	}

	public void setXmlDao(XmlStudentDao xmlDao) {
		this.xmlDao = xmlDao;
	}

	public void saveXmlDataInDatabase(InputStream in) {
		try {
			this.xmlDao.setIn(in);
			List<Student> students = this.xmlDao.getStudents();
			for (Student student : students) {
				this.jdbcDao.insertStudent(student);
			}
		} catch (Exception ex) {
			log.error(ex);
		}
	}

	public ByteArrayOutputStream getXmlDataFromDatabase() {
		ByteArrayOutputStream out = null;
		try {
			List<Student> students = this.jdbcDao.getStudents();
			out = new ByteArrayOutputStream();
			this.xmlDao.getXmlUtil().saveStudents(out, students);
		} catch (Exception ex) {
			log.error(ex);
		}
		return out;
	}

}
