package students.webapi;

public class UrlBuilder {

	public static final String DISPLAY_NAME = "HelloServlet";

	public static final String URI_PAGE_ALL = "all.jsp";
	public static final String URI_PAGE_EDIT = "edit.jsp";
	public static final String URI_PAGE_ERROR = "error.jsp";
	public static final String URI_PAGE_INDEX = "index.jsp";
	public static final String URI_PAGE_NEW = "new.jsp";
	public static final String URI_PAGE_UPLOAD = "upload.jsp";

	public static final String URI_ACT_CREATE = "create";
	public static final String URI_ACT_DELETE = "delete";
	public static final String URI_ACT_UPDATE = "update";
	public static final String URI_ACT_GETALL = "getall";
	public static final String URI_ACT_DEFAULT = "default";
	public static final String URI_ACT_GETXML = "getxml";
	public static final String URI_ACT_GETXSLT = "getxslt";
	public static final String URI_ACT_UPLOAD = "upload";

	public static String buildUrl(String uri) {
		return "/" + DISPLAY_NAME + "/" + uri;
	}
}
