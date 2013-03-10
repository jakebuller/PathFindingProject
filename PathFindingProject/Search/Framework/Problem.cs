﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Search.Framework {
	public class Problem {
		private readonly object m_initialState;
		private readonly IActionsFunction m_actionsFunction;
		private readonly IResultFunction m_resultFunction;
		private readonly IGoalTest m_goalTest;
		private readonly IStepCostFunction m_stepCostFunction;

		public Problem(
			object initialState,
			IActionsFunction actionsFunction,
			IResultFunction resultFunction,
			IGoalTest goalTest,
			IStepCostFunction stepCostFunction
		){
			m_initialState = initialState;
			m_actionsFunction = actionsFunction;
			m_resultFunction = resultFunction;
			m_goalTest = goalTest;
			m_stepCostFunction = stepCostFunction;
		}

		public object InitialState { 
			get {
				return m_initialState;
			}
		}

		public IActionsFunction ActionsFunction {
			get {
				return m_actionsFunction;
			}
		}

		public IResultFunction ResultFunction {
			get {
				return m_resultFunction;
			}
		}

		public IStepCostFunction StepCostFunction {
			get {
				return m_stepCostFunction;
			}
		}

		public IGoalTest GoalTest {
			get {
				return m_goalTest;
			}
		}

		public bool IsGoalState( object state ) {
			return m_goalTest.IsGoalState( state );
		}
	}
}
