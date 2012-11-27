package students.webapi.tags;

import javax.servlet.jsp.tagext.*;
import javax.servlet.jsp.*;
import javax.servlet.http.*;

import org.apache.log4j.Logger;

import java.io.IOException;
import java.text.*;
import java.util.*;

public class DateTag extends TagSupport {

	private static final Logger log = Logger.getLogger(DateTag.class);

	public int doStartTag() throws javax.servlet.jsp.JspException {

		HttpServletRequest request = (HttpServletRequest) pageContext
				.getRequest();
		Locale locale = request.getLocale();
		DateFormat df = SimpleDateFormat.getDateInstance(SimpleDateFormat.FULL,
				locale);
		String date = df.format(new java.util.Date());

		try {
			JspWriter out = pageContext.getOut();
			out.print(date);
		} catch (IOException ex) {
			log.error(ex);
		}
		return Tag.SKIP_BODY;

	}

}
