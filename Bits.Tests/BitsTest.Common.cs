using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medallion.Tests
{
    public partial class BitsTest
    {
        private const int FuzzTestIterations = 2000;

        private Lazy<Random> _random;

        [SetUp]
        public void SetUp()
        {
            if (this._random?.IsValueCreated ?? true)
            {
                this._random = new Lazy<Random>(() => new Random(12345));
            }
        }
    }
}
