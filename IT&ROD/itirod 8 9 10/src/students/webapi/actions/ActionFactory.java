package students.webapi.actions;

import java.util.HashMap;
import java.util.Map;
import org.apache.log4j.Logger;

import students.webapi.UrlBuilder;

public class ActionFactory {
	private static final Logger log = Logger.getLogger(ActionFactory.class);
	protected Map<String, IAction> map = defaultMap();

	public ActionFactory() {
		super();
	}

	public IAction create(String actionName) {
		IAction action = map.get(UrlBuilder.URI_ACT_DEFAULT);

		try {
			action = map.get(actionName);
		} catch (Exception ex) {
			log.error(ex);
		}
		return action;
	}

	protected static Map<String, IAction> defaultMap() {
		Map<String, IAction> map = null;
		try {
			map = new HashMap<String, IAction>();
			map.put(UrlBuilder.URI_ACT_CREATE, new CreateAction());
			map.put(UrlBuilder.URI_ACT_DELETE, new DeleteAction());
			map.put(UrlBuilder.URI_ACT_UPDATE, new UpdateAction());
			map.put(UrlBuilder.URI_ACT_GETALL, new GetAllAction());
			map.put(UrlBuilder.URI_ACT_DEFAULT, new DefaultAction());
			map.put(UrlBuilder.URI_ACT_GETXML, new GetXmlAction());
			map.put(UrlBuilder.URI_ACT_GETXSLT, new GetXsltAction());
			map.put(UrlBuilder.URI_ACT_UPLOAD, new UploadAction());
		} catch (Exception ex) {
			log.error(ex);
		}
		return map;
	}
}