using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Provides iterating in foreach and manipulating via LINQ over elements of range
/// </summary>
public struct RangeEnumerator : IEnumerator<int>
    {
        private Range _range;
        private int _current;
        private int _mode; // 0 - not started, 1 - started, 2- finished

        private bool HasMoreElement => (_current != _range.Stop) && ((_current < _range.Stop) ^ (_range.Step < 0));
       

        internal RangeEnumerator(ref Range range)
        {

            _range = range;
            _mode = 0;
            _current = 0;
        }

        public void Dispose() { }


        public bool MoveNext()
        {
            switch (_mode)
            {
                case 0:
                    _mode = 1;
                    _current = _range.Start;
                    return true;
                case 1:
                    _current += _range.Step;
                    if (!HasMoreElement)
                    {
                        _mode = 2;
                        return false;
                    }
                    return true;
                default: // 2
                    return false;
            }
        }

        public void Reset()
        {
            _mode = 0;
        }

    bool IEnumerator.MoveNext()
    {
        return MoveNext();
    }

    void IEnumerator.Reset()
    {
        Reset();
    }

    void IDisposable.Dispose()
    {
        Dispose();
    }

    object IEnumerator.Current => Current as object;

    public int Current
        {
            get
            {
                switch (_mode)
                {
                    case 1:
                        return _current;
                    case 0:
                        throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
                    default: //2
                        throw new InvalidOperationException("Enumeration already finished.");
                }
            }

        }


}
