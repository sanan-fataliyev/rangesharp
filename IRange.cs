using System;
using System.Linq;
using System.Collections.Generic;


public interface IRange : IEnumerable<int>
{
    bool Contains(int number);
    int IndexOf(int number);
    int this[int index] { get; }
}
