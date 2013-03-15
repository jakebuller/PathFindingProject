using System.Collections.Generic;
using System.Text;

namespace PathFindingProject.Agent {
	public abstract class ObjectWithDynamicAttributes {
		private Dictionary<object, object> m_attributes = 
			new Dictionary<object, object>();

		public virtual string DescribeType() {
			return GetType().Name;
		}

		/**
		 * Returns a string representation of the object's current attributes
		 * 
		 * @return a string representation of the object's current attributes
		 */
		public string DescribeAttributes() {
			StringBuilder sb = new StringBuilder();

			sb.Append("[");
			var first = true;
			foreach( object key in m_attributes.Keys ) {
				if (first) {
					first = false;
				} else {
					sb.Append(", ");
				}

				sb.Append( key );
				sb.Append( "==" );
				sb.Append( m_attributes[key] );
			}
			sb.Append( "]" );

			return sb.ToString();
		}

		public void SetAttribute( object key, object value ) {
			m_attributes[key] = value;
		}

		public object GetAttribute( object key ) {
			return m_attributes[key];
		}

		public void RemoveAttribute( object key ) {
			m_attributes.Remove( key );
		}

		public override bool Equals( object o ) {
			if (o == null || GetType() != o.GetType()) {
				return base.Equals( o );
			}
			return m_attributes.Equals( ( (ObjectWithDynamicAttributes) o).m_attributes);
		}

		public override int GetHashCode() {
			return m_attributes.GetHashCode();
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();

			sb.Append( DescribeType() );
			sb.Append( DescribeAttributes() );

			return sb.ToString();
		}
	}
}
