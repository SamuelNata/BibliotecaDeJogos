using System;
using System.ComponentModel.DataAnnotations;


namespace GameLib.Model
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public IBaseEntity WithId()
        {
            if(this.Id == null){
                this.Id = Guid.NewGuid();
                return this;
            }
            return this;
        }
    }
}
