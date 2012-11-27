package students.webapi.actions;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import students.domain.Student;
import students.repository.JdbcStudentDao;
import students.repository.StudentDao;
import students.webapi.UrlBuilder;

public class UpdateAction implements IAction {
	private static final Logger log = Logger.getLogger(UpdateAction.class);
	private static final String PARAM_ID = "id";
	private static final String PARAM_NAME = "name";
	private static final String PARAM_AGE = "age";
	private static final String PARAM_AVG_MARK = "averageMark";

	@Override
	public String perform(HttpServletRequest request,
			HttpServletResponse response) {
		String url = UrlBuilder.buildUrl(UrlBuilder.URI_ACT_GETALL);
		try {
			StudentDao dao = new JdbcStudentDao();
			int id = Integer.parseInt(request.getParameter(PARAM_ID));

			if (request.getMethod().equals("GET")) {
				Student student = dao.getStudent(id);
				request.getSession().setAttribute("student", student);
				url = UrlBuilder.URI_PAGE_EDIT;
			} else {
				String name = request.getParameter(PARAM_NAME);
				String age = request.getParameter(PARAM_AGE);
				String avgMark = request.getParameter(PARAM_AVG_MARK);
				Student student = new Student(id, Double.parseDouble(avgMark),
						name, Integer.parseInt(age));
				dao.updateStudent(student);
				request.getSession().removeAttribute("student");
			}
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
