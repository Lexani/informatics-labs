package students.webapi.actions;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import students.domain.Student;
import students.repository.*;
import students.webapi.UrlBuilder;
import students.webapi.ValidationUtil;

public class CreateAction implements IAction {

	private static final String PARAM_NAME = "name";
	private static final String PARAM_AGE = "age";
	private static final String PARAM_AVG_MARK = "averageMark";

	private static final Logger log = Logger.getLogger(CreateAction.class);

	@Override
	public String perform(HttpServletRequest request,
			HttpServletResponse response) {
		String url = UrlBuilder.buildUrl(UrlBuilder.URI_ACT_GETALL);
		try {
			Student newbie = new Student();
			String name = request.getParameter(PARAM_NAME);
			if (!ValidationUtil.validateInput(name)) {
				throw new Exception("Name validation error: " + name);
			}
			newbie.setName(name);

			String avgMark = request.getParameter(PARAM_AVG_MARK);
			double avgMarkValue = Double.parseDouble(avgMark);
			newbie.setAverageMark(avgMarkValue);

			String age = request.getParameter(PARAM_AGE);
			int ageValue = Integer.parseInt(age);
			newbie.setAge(ageValue);

			StudentDao dao = new JdbcStudentDao();
			dao.insertStudent(newbie);

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
