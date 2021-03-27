using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GameLib.Model.Entity;

namespace GameLib.Model.ViewModel
{
    public class CreateEditGameVM
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public short? Year { get; set; }
    }
    
    public class CreateEditGameVMProfile : Profile
	{
		public CreateEditGameVMProfile()
		{
			CreateMap<CreateEditGameVM, Game>().ReverseMap();
		}
	}
}