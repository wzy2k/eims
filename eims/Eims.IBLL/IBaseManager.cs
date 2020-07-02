﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eims.IBLL
{
    public interface IBaseManager<T>
    {
        Task<T> _getOne(int id);
        Task<List<T>> _getPage(int pageSize, int pageIndex, string key);
        Task<List<T>> _getAll();
        Task<int> _getRowCount(string key = null);
        Task<int> _add(T model);
        Task<int> _edit(T model);
        Task<int> _del(int id);
    }
}
