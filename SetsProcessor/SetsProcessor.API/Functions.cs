using SetsProcessor.Domain;
using System;
using System.Linq;
using System.Text;

namespace SetsProcessor.API
{
    public class Functions
    {

        private SetsCollection Collection { get; }

        public Functions()
        {
            Collection = new SetsCollection();
        }

        /// <summary>
        /// Permite añadir un set
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public bool AddSet(string set)
        {
            return Collection.Add(set);
        }

        /// <summary>
        /// Devuelve la cantidad de set correctos que se han añadido y cuántos se han añadidos múltiples veces
        /// </summary>
        /// <returns></returns>
        public string Resume()
        {
            var repeated = Collection.ValidateSets.Count(x => x.Value > 1);
            var notRepeated = Collection.ValidateSets.Count(x => x.Value == 1);
            return String.Format(Resources.ResumeMessage, repeated, notRepeated);
        }

        /// <summary>
        /// Devuelve el listado de strings que no tenían un formato correcto
        /// </summary>
        /// <returns></returns>
        public string GetIncorrectSets()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var set in Collection.UnvalidSets)
                sb.Append(set.Key + Environment.NewLine);

            return String.Format(Resources.UnvalidateMessage, sb.ToString());
        }

        /// <summary>
        /// Devuelve el set más frecuentemente insertado
        /// </summary>
        /// <returns></returns>
        public string GetMoreFrequentSet()
        {
            return Collection.MoreRepeatSet.ToString();
        }
    }
}
