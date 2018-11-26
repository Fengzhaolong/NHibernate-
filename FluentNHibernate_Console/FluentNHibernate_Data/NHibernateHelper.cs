using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate_Data
{
    /// <summary>
    /// 增删查改帮助类
    /// </summary>
    public class NHibernateHelper
    {
        /// <summary>
        /// 获取实体List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetEntity_List<T>() where T : class
        {
            using (var session = NHibernateConnection.OpenSession())
            {
                using (var transection = session.BeginTransaction())
                {
                    var list = session.QueryOver<T>();
                    transection.Commit();
                    return list.List();
                }
            }
        }

        /// <summary>
        /// 获取实体单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">对象id</param>
        /// <returns></returns>
        public T GetEntity_Single<T>(int id) where T : class
        {
            using (var session = NHibernateConnection.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    T Entity = session.Get<T>(id);
                    return Entity;
                }
            }
        }

        /// <summary>
        /// 获取实体List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">lambda表达式</param>
        /// <returns></returns>
        public IList<T> GetEntity_List_Expression<T>(Expression<Func<T, bool>> expression) where T : class
        {
            using (var session = NHibernateConnection.OpenSession())
            {
                using (var transacation = session.BeginTransaction())
                {
                    var list = session.QueryOver<T>().Where(expression);
                    transacation.Commit();
                    return list.List();
                }
            }
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity">Model数据</param>
        /// <returns></returns>
        public bool Insert<T>(T Entity)where T:class
        {
            using (var session = NHibernateConnection.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    int result = 0;
                    try
                    {
                        result = Convert.ToInt32(session.Save(Entity));
                        transaction.Commit();
                        //session.Flush();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    return result > 0 ? true : false;
                }
            }
        }


        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity">Model数据</param>
        public void Update<T>(T Entity)where T:class
        {
            using (var session=NHibernateConnection.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    try
                    {
                        session.Update(Entity);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">实体id</param>
        public void Delete<T>(int id)where T:class
        {
            using (var session=NHibernateConnection.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    try
                    {
                        T Entity = this.GetEntity_Single<T>(id);
                        session.Delete(Entity);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }


    }
}
