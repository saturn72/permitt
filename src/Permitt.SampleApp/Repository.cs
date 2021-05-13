using System;
using System.Collections.Generic;
using System.Linq;

namespace Permitt.SampleApp
{
    public class Repository
    {
        private static int _index = 0;
        private static IList<AddExpression> _items = new List<AddExpression>();
        public AddExpression GetbyId(string id)
        {
            return _items.FirstOrDefault(r => r.Id == id);
        }
        public IEnumerable<AddExpression> GetBy(Func<AddExpression, bool> predicate)
        {
            return _items.Where(predicate);
        }
        public void Create(AddExpression ae)
        {
            ae.Id = (++_index).ToString();
            _items.Add(ae);
        }
    }
}