using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_dot_core.Classes
{
    public static class Dictionary
    {
        public static IDictionary<string, string> frasesDictionary = new Dictionary<string, string>
        {
            {"Save","Сохранить"},
            {"Cancel","Отмена"},
            {"Delete","Удалить"},
            {"Edit","Редактировать"},
            {"DeleteConfirm","Вы действительно хотите удалить запись?"}
        };
    }
}
