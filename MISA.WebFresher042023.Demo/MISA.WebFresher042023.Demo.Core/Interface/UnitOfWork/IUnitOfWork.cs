using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork
{
    /// <summary>
    /// interface unit of work
    /// </summary>
    /// Created by: vdtien (1/8/2023)
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        #region Property
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }
        #endregion

        /// <summary>
        /// mở transaction
        /// </summary>
        /// created by: vdtien (1/8/2023)
        void BeginTransaction();

        /// <summary>
        /// mở transaction async
        /// </summary>
        /// <returns></returns>
        /// created by: vdtien (1/8/2023)
        Task BeginTransactionAsync();

        /// <summary>
        /// commit transaction
        /// </summary>
        /// created by: vdtien (1/8/2023)
        void Commit();

        /// <summary>
        /// commit transaction async
        /// </summary>
        /// <returns></returns>
        /// created by: vdtien (1/8/2023)
        Task CommitAsync();

        /// <summary>
        /// rollback transaction
        /// </summary>
        /// created by: vdtien (1/8/2023)
        void Rollback();

        /// <summary>
        /// rollback transaction async
        /// </summary>
        /// <returns></returns>
        /// created by: vdtien (1/8/2023)
        Task RollbackAsync();


    }
}
