package students.xml.util;

import java.io.InputStream;
import java.io.OutputStream;
import java.util.List;

import students.domain.Student;

public interface IStudentXmlUtil {

	static final String TAG_ROOT = "java";
	static final String TAG_OBJECT = "object";
	static final String TAG_PROPERTY = "void";
	static final String TAG_STUDENT = "object";
	static final String TAG_NAME = "name";
	static final String TAG_AGE = "age";
	static final String TAG_AVG_MARK = "averageMark";
	static final String TAG_ID = "id";

	static final String ATTR_STUD_CLASS = "students.domain.Student";
	static final String ATTR_STUD_PROP = "property";
	static final String ATTR_PROP_STUD_AGE = "age";
	static final String ATTR_PROP_STUD_AVG_MARK = "averageMark";
	static final String ATTR_PROP_STUD_ID = "id";
	static final String ATTR_PROP_STUD_NAME = "name";

	static final int PROP_INVALID = -1;
	static final int PROP_AGE = 0;
	static final int PROP_AVG_MARK = 1;
	static final int PROP_ID = 2;
	static final int PROP_NAME = 3;

	List<Student> loadStudents(InputStream in);

	void saveStudents(OutputStream out, List<Student> students);
}
