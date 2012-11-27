package students.webapi.actions;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import students.repository.JdbcStudentDao;
import students.repository.StudentDao;
import students.webapi.UrlBuilder;

public class DeleteAction implements IAction {
	private static final Logger log = Logger.getLogger(DeleteAction.class);
	private static final String PARAM_ID = "id";

	@Override
	public String perform(HttpServletRequest request,
			HttpServletResponse response) {
		String url = UrlBuilder.buildUrl(UrlBuilder.URI_ACT_GETALL);
		try {
			StudentDao dao = new JdbcStudentDao();
			int id = Integer.parseInt(request.getParameter(PARAM_ID));
			dao.deleteStudent(id);

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
