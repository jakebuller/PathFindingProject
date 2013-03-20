using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Environment.Map
{
    public class MapFunctionFactory {
	private static IResultFunction _resultFunction = null;
	private static IPerceptToStateFunction _perceptToStateFunction = null;

	public static IActionsFunction getActionsFunction(Map map) {
		return new MapActionsFunction(map);
	}

	public static IResultFunction getResultFunction() {
		if (null == _resultFunction) {
			_resultFunction = new MapResultFunction();
		}
		return _resultFunction;
	}

	private static class MapActionsFunction : IActionsFunction {
		private Map map = null;

		public MapActionsFunction(Map map) {
			this.map = map;
		}

		public Set<IAction> actions(Object state) {
			Set<IAction> actions = new LinkedHashSet<IAction>();
			string location = state.ToString();

			List<string> linkedLocations = map.getLocationsLinkedTo(location);
			foreach (string linkLoc in linkedLocations) {
				actions.add(new MoveToAction(linkLoc));
			}

			return actions;
		}
	}

	public static IPerceptToStateFunction getPerceptToStateFunction() {
		if (null == _perceptToStateFunction) {
			_perceptToStateFunction = new MapPerceptToStateFunction();
		}
		return _perceptToStateFunction;
	}

	private static class MapResultFunction:IResultFunction {
		public MapResultFunction() {
		}

		public Object result(Object s, Action a) {

			if (a.GetType() == typeof(MoveToAction)) {
				MoveToAction mta = (MoveToAction) a;

				return mta.getToLocation();
			}

			// The Action is not understood or is a NoOp
			// the result will be the current state.
			return s;
		}
	}

	private static class MapPerceptToStateFunction:
			IPerceptToStateFunction {
		public Object getState(IPercept p) {
			return ((DynamicPercept) p)
					.getAttribute(DynAttributeNames.PERCEPT_IN);
		}
	}
}
}
