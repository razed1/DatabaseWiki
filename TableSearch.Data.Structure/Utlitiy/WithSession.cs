using System;
using NHibernate;

namespace TableSearch.Data.Structure.Utlitiy
{
    public class WithSession
    {
        public Func<ISession> SessionMethod { get; set; }

        public WithSession(Func<ISession> sessionMethod)
        {
            SessionMethod = sessionMethod;
        }
        
        public void Do(Action<ISession> operation)
        {
            using (var session = SessionMethod())
            {
                operation(session);
            }
        }

        public T ReturnResult<T>(Func<ISession, T> operation)
        {
            T result;

            using (var session = SessionMethod())
            {
                result = operation(session);
            }

            return result;
        }
    }
}