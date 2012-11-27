package students.webapi.actions;

import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import students.domain.Student;
import students.repository.JdbcStudentDao;
import students.repository.StudentDao;
import students.webapi.UrlBuilder;

public class GetAllAction implements IAction {

	private static final Logger log = Logger.getLogger(GetAllAction.class);

	@Override
	public String perform(HttpServletRequest request,
			HttpServletResponse response) {
		String url = UrlBuilder.URI_PAGE_ALL;
		try {
			StudentDao dao = new JdbcStudentDao();
			List<Student> students = dao.getStudents();
			request.getSession().setAttribute("students", students);
		} catch (Exception ex) {
			log.error(ex);
			url = UrlBuilder.URI_PAGE_ERROR;
		}
		return url;
	}

	@Override
	public void writeToResponseStream(HttpServletResponse response,
			String output) {

	}

}
