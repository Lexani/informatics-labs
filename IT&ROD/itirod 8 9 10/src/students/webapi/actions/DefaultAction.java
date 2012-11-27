package students.webapi.actions;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import students.webapi.UrlBuilder;

public class DefaultAction implements IAction {

	@Override
	public String perform(HttpServletRequest request,
			HttpServletResponse response) {
		return UrlBuilder.URI_PAGE_INDEX;
	}

	@Override
	public void writeToResponseStream(HttpServletResponse response,
			String output) {

	}

}
