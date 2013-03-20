using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PathFindingProject.Agent
{
public class DynamicPercept: ObjectWithDynamicAttributes,
		IPercept {
	public DynamicPercept() {

	}

	public override String describeType() {
        return typeof(IPercept).Name;
	}

	/**
	 * Constructs a DynamicPercept with one attribute
	 * 
	 * @param key1
	 *            the attribute key
	 * @param value1
	 *            the attribute value
	 */
	public DynamicPercept(Object key1, Object value1) {
		this.SetAttribute(key1, value1);
	}

	/**
	 * Constructs a DynamicPercept with two attributes
	 * 
	 * @param key1
	 *            the first attribute key
	 * @param value1
	 *            the first attribute value
	 * @param key2
	 *            the second attribute key
	 * @param value2
	 *            the second attribute value
	 */
	public DynamicPercept(Object key1, Object value1, Object key2, Object value2) {
		this.SetAttribute(key1, value1);
		this.SetAttribute(key2, value2);
	}

	/**
	 * Constructs a DynamicPercept with an array of attributes
	 * 
	 * @param keys
	 *            the array of attribute keys
	 * @param values
	 *            the array of attribute values
	 */
	public DynamicPercept(Object[] keys, Object[] values) {
		assert (keys.Length == values.Length);

		for (int i = 0; i < keys.Length; i++) {
			this.SetAttribute(keys[i], values[i]);
		}
	}
}
}
