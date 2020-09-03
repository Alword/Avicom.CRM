using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avicom.CRM.Shared
{
    public class Map<T1, T2>
    {
        private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();
        private readonly Indexer<T1, T2> Forward;
        private readonly Indexer<T2, T1> Reverse;
        public Map()
        {
            this.Forward = new Indexer<T1, T2>(_forward);
            this.Reverse = new Indexer<T2, T1>(_reverse);
        }

        public T1 this[T2 index]
        {
            get { return Reverse[index]; }
            set { Reverse[index] = value; }
        }

        public T2 this[T1 index]
        {
            get { return Forward[index]; }
            set { Forward[index] = value; }
        }

        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

    }
}
