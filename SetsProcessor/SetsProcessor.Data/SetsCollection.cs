using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SetsProcessor.Domain
{
    public class SetsCollection
    {

        public Dictionary<string, int> UnvalidSets { get; }

        public Dictionary<Set, int> ValidateSets { get; }

        private int _moreRepeatCount { get; set; }
        public int MoreRepeatCount { get { return _moreRepeatCount; } }

        private Set _moreRepeatSet { get; set; }
        public Set MoreRepeatSet { get { return _moreRepeatSet; } }

        public SetsCollection()
        {
            ValidateSets = new Dictionary<Set, int>();
            UnvalidSets = new Dictionary<string, int>();
            _moreRepeatCount = 0;
        }

        /// <summary>
        /// Permite añadir un set
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public bool Add(string set)
        {
            if (set != null)
            {
                if (Regex.IsMatch(set, "^[0-9]+(,[0-9]+)*$"))
                {
                    int[] nums = Array.ConvertAll(set.Split(','), int.Parse);
                    Array.Sort(nums);
                    AddValidSet(new Set(nums));
                    return true;
                }
                else
                    AddUnvalidSet(set);
            }
            return false;
        }

        /// <summary>
        /// Realiza la inserción de un set validado y actualiza estadísticas
        /// </summary>
        /// <param name="set"></param>
        private void AddValidSet(Set set)
        {
            int repeat = 1;
            if (!ValidateSets.ContainsKey(set))
                ValidateSets.Add(set, repeat);
            else
                ValidateSets[set]++;

            UpdateMoreRepeat(set, ValidateSets[set]);
        }

        /// <summary>
        /// Actualiza el Set más insertado
        /// </summary>
        /// <param name="set"></param>
        /// <param name="count"></param>
        private void UpdateMoreRepeat(Set set, int count)
        {
            if (count > _moreRepeatCount)
            {
                _moreRepeatCount = count;
                _moreRepeatSet = set;
            }
        }

        /// <summary>
        /// Añade un elemento al listado de cadenas con formato incorrecto
        /// </summary>
        /// <param name="set"></param>
        private void AddUnvalidSet(string set)
        {
            int repeat = 1;
            if (!UnvalidSets.ContainsKey(set))
                UnvalidSets.Add(set, repeat);
            else
            {
                repeat = UnvalidSets[set]++;
                UnvalidSets[set] = repeat;
            }
        }

    }
}
