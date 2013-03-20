using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Util
{
public class Util {
	public static const string NO = "No";
	public static const string YES = "Yes";
	//
	private static Random _r = new Random();

	/**
	 * Get the first element from a list.
	 * 
	 * @param l
	 *            the list the first element is to be extracted from.
	 * @return the first element of the passed in list.
	 */
	public static T first(List<T> l) {
		return l.get(0);
	}

	/**
	 * Get a sublist of all of the elements in the list except for first.
	 * 
	 * @param l
	 *            the list the rest of the elements are to be extracted from.
	 * @return a list of all of the elements in the passed in list except for
	 *         the first element.
	 */
	public static List<T> rest(List<T> l) {
		return l.Skip(1);
	}

	/**
	 * Create a Map<K, V> with the passed in keys having their values
	 * initialized to the passed in value.
	 * 
	 * @param keys
	 *            the keys for the newly constructed map.
	 * @param value
	 *            the value to be associated with each of the maps keys.
	 * @return a map with the passed in keys initialized to value.
	 */
    public static <K, V> Map<K, V> create(Collection<K> keys, V value) {
        Map<K, V> map = new LinkedHashMap<K, V>();

        for (K k : keys) {
            map.put(k, value);
        }

        return map;
    }

	/**
	 * Randomly select an element from a list.
	 * 
	 * @param <T>
	 *            the type of element to be returned from the list l.
	 * @param l
	 *            a list of type T from which an element is to be selected
	 *            randomly.
	 * @return a randomly selected element from l.
	 */
	public static T selectRandomlyFromList(List<T> l) {
		return l.[_r.Next(l.Count)];
	}

	public static bool randomBoolean() {
		int trueOrFalse = _r.Next(2);
		return (!(trueOrFalse == 0));
	}

	public static double[] normalize(double[] probDist) {
		int len = probDist.Length;
		double total = 0.0;
		foreach (double d in probDist) {
			total = total + d;
		}

		double[] normalized = new double[len];
		if (total != 0) {
			for (int i = 0; i < len; i++) {
				normalized[i] = probDist[i] / total;
			}
		}

		return normalized;
	}

	public static List<double> normalize(List<double> values) {
		double[] valuesAsArray = new double[values.Count];
		for (int i = 0; i < valuesAsArray.Length; i++) {
			valuesAsArray[i] = values[i];
		}
		double[] normalized = normalize(valuesAsArray);
		List<double> results = new List<double>();
		for (int i = 0; i < normalized.Length; i++) {
			results.Add(normalized[i]);
		}
		return results;
	}

	public static int min(int i, int j) {
		return (i > j ? j : i);
	}

	public static int max(int i, int j) {
		return (i < j ? j : i);
	}

	public static int max(int i, int j, int k) {
		return max(max(i, j), k);
	}

	public static int min(int i, int j, int k) {
		return min(min(i, j), k);
	}

	public static T mode(List<T> l) {
		Dictionary<T, int> hash = new Dictionary<T, int>();
		foreach (T obj in l) {
			if (hash.ContainsKey(obj)) {
				hash[obj] = hash[obj].intValue() + 1;
			} else {
				hash[obj] = 1;
			}
		}

		T maxkey = hash.Keys.FirstOrDefault();
		foreach (T key in hash.Keys) {
			if (hash.get(key) > hash.get(maxkey)) {
				maxkey = key;
			}
		}
		return maxkey;
	}

	public static string[] yesno() {
		return new string[] { YES, NO };
	}

	public static double log2(double d) {
		return Math.Log(d) / Math.Log(2);
	}

	public static double information(double[] probabilities) {
		double total = 0.0;
		foreach (double d in probabilities) {
			total += (-1.0 * log2(d) * d);
		}
		return total;
	}

	public static <T> List<T> removeFrom(List<T> list, T member) {
		List<T> newList = new List<T>(list);
		newList.Remove(member);
		return newList;
	}

    //public static <T extends Number> double sumOfSquares(List<T> list) {
    //    double accum = 0;
    //    foreach (T item in list) {
    //        accum = accum + (item.doubleValue() * item.doubleValue());
    //    }
    //    return accum;
    //}

	public static string ntimes(string s, int n) {
		StringBuilder buf = new StringBuilder();
		for (int i = 0; i < n; i++) {
			buf.Append(s);
		}
		return buf.ToString();
	}

	public static void checkForNanOrInfinity(double d) {
		if (Double.IsNaN(d)) {
			throw new ArgumentException("Not a Number");
		}
		if (double.IsInfinity(d)) {
			throw new ArgumentException("Infinite Number");
		}
	}

	public static int randomNumberBetween(int i, int j) {
		/* i,j bothinclusive */
		return _r.Next(j - i + 1) + i;
	}

	public static double calculateMean(List<double> lst) {
		double sum = 0.0;
		foreach (double d in lst) {
			sum = sum + d;
		}
		return sum / lst.Count;
	}

	public static double calculateStDev(List<double> values, double mean) {

		int listSize = values.Count;

		double sumOfDiffSquared = 0.0;
		foreach (double value in values) {
			double diffFromMean = value - mean;
			sumOfDiffSquared += ((diffFromMean * diffFromMean) / (listSize - 1));
			// division moved here to avoid sum becoming too big if this
			// doesn't work use incremental formulation

		}
		double variance = sumOfDiffSquared;
		// (listSize - 1);
		// assumes at least 2 members in list.
		return Math.Sqrt(variance);
	}

	public static List<double> normalizeFromMeanAndStdev(List<double> values,
			double mean, double stdev) {
		List<double> normalized = new List<double>();
		foreach (double d in values) {
			normalized.Add((d - mean) / stdev);
		}
		return normalized;
	}

	public static double generateRandomdoubleBetween(double lowerLimit,
			double upperLimit) {

		return lowerLimit + ((upperLimit - lowerLimit) * _r.NextDouble());
	}
}
}
