package students.webapi;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.apache.log4j.Logger;

import students.domain.Student;

public class ValidationUtil {
	// add trim spaces functionality
	private static final String INPUT_REGEXP = "^[0-9\\sA-Za-z]+$";
	private static final Double MAX_MARK = 10.0;
	private static final Double MIN_MARK = 0.0;
	private static final Integer MIN_AGE = 16;
	private static final Integer MAX_AGE = 100;
	private static Logger log = Logger.getLogger(ValidationUtil.class);

	public static final String TRIM_REGEXP = "[\t\\s\n]";

	public static boolean validateInput(String query) {
		Pattern p = Pattern.compile(INPUT_REGEXP);
		Matcher m = p.matcher(query);
		return m.matches();
	}

	public static boolean validateStudent(Student student) {
		log.info("Student validation: " + student.toString());
		return validateName(student.getName()) && validateAge(student.getAge())
				&& validateAvgMark(student.getAverageMark());
	}

	private static boolean validateName(String name) {
		return !name.isEmpty() && validateInput(name);
	}

	private static boolean validateAge(int age) {
		return age > MIN_AGE && age < MAX_AGE;
	}

	private static boolean validateAvgMark(Double avgMark) {
		return MIN_MARK.compareTo(avgMark) < 0
				&& avgMark.compareTo(MAX_MARK) < 0;
	}
}
