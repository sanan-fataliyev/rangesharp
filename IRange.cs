namespace Sanan.DataStructs
{
	

	public interface IRange : IEnumerable<int>
    {
        bool Contains(int number);
        int IndexOf(int number);
        int this[int index] { get; }
    }

}