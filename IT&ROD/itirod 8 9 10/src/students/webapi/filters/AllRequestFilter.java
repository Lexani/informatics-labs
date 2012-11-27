package students.webapi.filters;

import java.io.IOException;

import javax.servlet.*;
import javax.servlet.annotation.WebFilter;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import students.webapi.UrlBuilder;

@WebFilter("/" + UrlBuilder.URI_PAGE_ALL)
public class AllRequestFilter implements Filter {

	private FilterConfig filterConfig;
	private Logger log = Logger.getLogger(AllRequestFilter.class);

	public void init(FilterConfig filterConfig) throws ServletException {
		this.filterConfig = filterConfig;
		log.info("Filter initialized: " + this.filterConfig.toString());
	}

	public void destroy() {
		log.info("Filter destroyed");
		this.filterConfig = null;
	}

	public void doFilter(ServletRequest request, ServletResponse response,
			FilterChain chain) throws IOException, ServletException {
		log.info("doFilter");
		HttpServletRequest req = (HttpServletRequest) request;
		HttpServletResponse resp = (HttpServletResponse) response;
		if (req != null && resp != null) {
			if (req.getSession().getAttribute("students") == null) {
				resp.sendRedirect(UrlBuilder.URI_ACT_GETALL);
				return;
			}
		}
		chain.doFilter(request, response);
	}
}
