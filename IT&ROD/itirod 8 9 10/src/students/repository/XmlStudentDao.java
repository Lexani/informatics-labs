package students.repository;

import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;

import students.domain.Student;
import students.xml.util.IStudentXmlUtil;

public class XmlStudentDao implements StudentDao {

	private InputStream in;
	private OutputStream out;
	private IStudentXmlUtil xmlUtil;
	private static Logger log = Logger.getLogger(XmlStudentDao.class);

	public IStudentXmlUtil getXmlUtil() {
		return xmlUtil;
	}

	public void setXmlUtil(IStudentXmlUtil xmlUtil) {
		this.xmlUtil = xmlUtil;
	}

	public InputStream getIn() {
		return in;
	}

	public void setIn(InputStream in) {
		this.in = in;
	}

	public OutputStream getOut() {
		return out;
	}

	public void setOut(OutputStream out) {
		this.out = out;
	}

	public XmlStudentDao() {
	}

	public XmlStudentDao(InputStream in, OutputStream out) {
		this.in = in;
		this.out = out;
	}

	@Override
	public void insertStudent(Student student) {
		try {
			List<Student> students = xmlUtil.loadStudents(in);
			if (students == null)
				students = new ArrayList<Student>();
			students.add(student);
			xmlUtil.saveStudents(out, students);
		} catch (Exception ex) {
			log.error(ex);
		}
	}

	@Override
	public Student getStudent(int id) {
		Student result = null;

		try {
			if (!this.validateDao())
				throw new Exception("XmlStudentDao fields not set.");

			List<Student> students = xmlUtil.loadStudents(in);
			if (students != null && students.size() > 0) {
				for (Student student : students) {
					if (student.getId() == id) {
						result = student;
					}
				}
			}
		} catch (Exception ex) {
			log.error(ex);
		}
		return result;
	}

	@Override
	public void updateStudent(Student student) {
		try {
			if (!this.validateDao())
				throw new Exception("XmlStudentDao fields not set.");
			List<Student> students = xmlUtil.loadStudents(in);
			if (students != null && students.size() > 0) {
				for (Student st : students) {
					if (st.getId() == student.getId()) {
						st.setAge(student.getAge());
						st.setName(student.getName());
						st.setAverageMark(student.getAverageMark());
					}
				}
				xmlUtil.saveStudents(out, students);
			}
		} catch (Exception ex) {
			log.error(ex);
		}
	}

	@Override
	public void deleteStudent(int id) {
		try {
			if (!this.validateDao())
				throw new Exception("XmlStudentDao fields not set.");
			List<Student> students = xmlUtil.loadStudents(in);
			if (students != null && students.size() > 0) {
				int studentIndex = -1;
				for (int i = 0; i < students.size(); i++) {
					if (students.get(i).getId() == id) {
						studentIndex = i;
						break;
					}
				}
				students.remove(studentIndex);
				xmlUtil.saveStudents(out, students);
			}
		} catch (Exception ex) {
			log.error(ex);
		}

	}

	@Override
	public List<Student> getStudents() {
		List<Student> result = null;
		try {
			if (in == null || xmlUtil == null)
				throw new Exception("XmlStudentDao fields not set.");
			result = xmlUtil.loadStudents(in);
		} catch (Exception ex) {
			log.error(ex);
		}
		// IStudentXmlUtil xmlTool = new StudentSaxXmlUtil();
		// OutputStream out = new FileOutputStream("/home/alex/students.xml");
		// xmlTool.saveStudents(out, students);
		// out.close();

		// InputStream in = new FileInputStream("/home/alex/students.xml");
		// List<Student> newstudents = xmlTool.loadStudents(in);
		// in.close();
		return result;
	}

	private boolean validateDao() {
		return in != null && out != null && xmlUtil != null;
	}

}
