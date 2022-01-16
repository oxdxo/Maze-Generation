using System.Collections.Generic;

namespace ZhaoXiaodan.Lab2
{
    public interface IGridGraph<T> : IEnumerable<T>
    {
        int NumberOfRows { get; }
        int NumberOfColumns { get; }

        T GetCellValue(int row, int column);
    }
}
