package students.webapi;

import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import students.webapi.actions.ActionFactory;
import students.webapi.actions.IAction;

/**
 * Servlet implementation class StudentServlet
 */
@WebServlet("/")
public class StudentServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
	private static final ActionFactory factory = new ActionFactory();
	private static final Logger log = Logger.getLogger(StudentServlet.class);

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public StudentServlet() {
		super();
	}
	
	protected String getActionName(HttpServletRequest request) {
		String path = request.getServletPath();
		int pathLength = path.length();
		if (path == null || pathLength == 0)
			return UrlBuilder.URI_ACT_DEFAULT;

		int paramIndex = path.indexOf('?');
		paramIndex = paramIndex == -1 ? path.length() : paramIndex;
		return path.substring(1, paramIndex);
	}

	protected void service(HttpServletRequest request,
			HttpServletResponse response) {
		String actionName = getActionName(request);
		IAction action = factory.create(actionName);
		if (action == null) {
			action = factory.create(UrlBuilder.URI_ACT_DEFAULT);
		}
		String url = action.perform(request, response);
		if (url != null)
			try {
				response.sendRedirect(url);
			} catch (Exception e) {
				log.error(e);
			}
	}
}
