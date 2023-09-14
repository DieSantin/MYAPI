using MYAPI.NoSql.Entities;

namespace MYAPI.NoSql;

public class NoSqlContext
{
    public NoSqlContext()
    {

    }

    public virtual List<User> Users { get; set; }
}
