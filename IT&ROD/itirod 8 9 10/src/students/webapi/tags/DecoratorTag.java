package students.webapi.tags;

import javax.servlet.jsp.tagext.*;
import javax.servlet.jsp.*;

import org.apache.log4j.Logger;

import java.io.IOException;

public class DecoratorTag extends TagSupport {

	/**
	 * 
	 */
	private static final long serialVersionUID = 7451138536150917716L;
	private static final Logger log = Logger.getLogger(DecoratorTag.class);

	public int doStartTag() throws javax.servlet.jsp.JspException {
		this.doMarkup();
		return Tag.EVAL_BODY_INCLUDE;
	}

	public int doEndTag() throws javax.servlet.jsp.JspException {
		this.doMarkup();
		return Tag.EVAL_PAGE;
	}

	private void doMarkup() {
		String decoration = "|||||||||||||||||";
		try {
			JspWriter out = pageContext.getOut();
			out.print(decoration);
		} catch (IOException ex) {
			log.error(ex);
		}
	}

}
