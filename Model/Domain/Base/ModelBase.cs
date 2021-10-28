using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain.Base
{
    public abstract class ModelBase
    {
        protected ModelBase()
        {
            State = State.Unchanged;
        }

        public static T CreateInstance<T>(State state) where T : ModelBase, new()
        {
            T instance = new T
            {
                State = state
            };
            return instance;
        }
        public int Id { get; set; }

        [NotMapped]
        public State State { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
