﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents python like range of integer numbers.
/// All the provided methods' algorithms optimized to O(1) complexity.
/// </summary>
public struct Range : IRange, IEquatable<Range>
{

    private readonly int _start;
    private readonly int _stop;
    private readonly int _step;

    /// <summary>
    /// Generates new range object with given parametres.
    /// </summary>
    /// 
    /// <param name="start">Offset of range.</param>
    /// <param name="stop">Limit of range.</param>
    /// <param name="step">Differerce between neighbor elements of range. It can not be zero.</param>
    public Range(int start, int stop, int step = 1)
    {
        if (step == 0)
            throw new ArgumentException("Step can not be zero.");
        _start = start;
        _stop = stop;
        _step = step;
    }

    /// <summary>
    /// Generates new range object with given limit.
    /// Offset will be 0 and step will be 1.
    /// </summary>
    /// 
    /// <param name="stop">Limit of range.</param>
    public Range(int stop) : this(0, stop) { }



    /// <summary>
    /// Start point and first element of range.
    /// </summary>
    public int Start
    {
        get { return _start; }
    }



    /// <summary>
    /// Limit of range. This value is not include in elements.
    /// </summary>
    public int Stop
    {
        get { return _stop; }
    }

    /// <summary>
    /// Step etalon to limit.
    /// </summary>
    public int Step
    {
        get { return _step; }
    }

    /// <summary>
    /// Count of elements of this range.
    /// </summary>
    public int Count { get { return ShouldGenerate ? StepCount : 0; } }

    /// <summary>
    /// Sum of elements of this range.
    /// </summary>
    public int Sum { get { return ShouldGenerate ? ((_start + this[Count - 1]) * Count) >> 1 : 0; } }


    private bool ShouldGenerate { get { return (_start != _stop) && !((_start < _stop) ^ (_step > 0)); } }


    private int StepCount { get { return (_stop - _start - Math.Abs(_step) / _step) / _step + 1; } }


    /// <summary>
    /// Checks if this range contains given number.
    /// </summary>
    /// 
    /// <param name="number">Given number.</param>
    /// 
    /// <returns>True if given number contains in elements of this range, otherwise false.</returns>
    public bool Contains(int number)
    {
        //
        if ((_start % _step + _step) % _step != (number % _step + _step) % _step)
            return false;
        var vector = Math.Abs(_step) / _step;
        return vector * number >= vector * _start && vector * number < vector * _stop;
    }


    /// <summary>
    /// Searching index of given number in this range.
    /// </summary>
    /// 
    /// <param name="number">Given number.</param>
    /// 
    /// <returns>Returns the index of given number if it is in elements of range, otherwise -1.</returns>
    public int IndexOf(int number) { return Contains(number) ? (number - _start) / _step : -1; }


    /// <summary>
    /// Get a range element via given index. This index accessor is read only.
    /// </summary>
    /// 
    /// <param name="index">Zero based index.</param>
    /// 
    /// <returns>Returns index'th element of this range.</returns>
    public int this[int index]
    {
        get
        {
            if (!ShouldGenerate)
                throw new InvalidOperationException("The range is empty.");
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException($"Index must be between 0 and {Count}.");
            return _start + _step * index;
        }
    }




    // IEnumerable impl
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    public IEnumerator<int> GetEnumerator() { return ShouldGenerate ? new RangeEnumerator(ref this) : Enumerable.Empty<int>().GetEnumerator(); }




    public static bool operator ==(Range rigth, Range left)
    {
        return rigth._step == left._step &&
           rigth._stop == left._stop &&
           rigth._start == left._start;
    }


    public static bool operator !=(Range rigth, Range left) { return !(rigth == left); }


    // overriding object methods
    public bool Equals(Range other) { return other == this; }


    public override bool Equals(object other) { return other is Range && ((Range)other) == this; }


    public override int GetHashCode() { return ((~_start) | _stop) ^ _step; }


    public override string ToString()
    {

        var result = new StringBuilder();

        var rangeInfo = string.Format("Range(start: {0}, stop: {1}, step: {2})", _start, _stop, _step);
        result.AppendLine(rangeInfo);

        var elementsStr = Count > 10 ? string.Join(", ", this.Take(9)) + "..." + this[Count - 1] : string.Join(", ", this);
        result.Append(string.Concat("[", elementsStr, "]"));

        return result.ToString();
    }

}