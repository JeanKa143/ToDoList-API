﻿using ToDoList_DAL.Contracts;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Data;
using ToDoLIst_DAL.Repositories;

namespace ToDoList_DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private ITaskListGroupRepository? _taskListGroups;
        private ITaskListRepository? _taskLists;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ITaskListGroupRepository TaskListGroups => _taskListGroups ??= new TaskListGroupRepository(_context);

        public ITaskListRepository TaskLists => _taskLists ??= new TaskListRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
