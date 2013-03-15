using System.Collections.Generic;
using System.Linq;

namespace PathFindingProject.Search.Framework {
	public class Metrics {
		private readonly Dictionary<string, string> m_metrics;

		public Metrics() {
			m_metrics = new Dictionary<string, string>();
		}

		public void Set( string name, int i ) {
			m_metrics[name] = i.ToString();
		}

		public void Set( string name, double d ) {
			m_metrics[name] = d.ToString();
		}

		public void Set( string name, long l ) {
			m_metrics[name] = l.ToString();
		}

		public bool TryGetInt( string name, out int i ) {
			i = 0;
			if( m_metrics.Keys.Contains( name ) ) {
				return int.TryParse( m_metrics[name], out i );
			}

			return false;
		}

		public bool TryGetDouble( string name, out double d ) {
			d = 0;
			if( m_metrics.Keys.Contains( name ) ) {
				return double.TryParse( m_metrics[name], out d );
			}

			return false;
		}

		public bool TryGetLong( string name, out long l ) {
			l = 0;

			if ( m_metrics.Keys.Contains( name ) ) {
				return long.TryParse( m_metrics[name], out l );
			}

			return false;
		}
	}
}
