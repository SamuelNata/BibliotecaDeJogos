using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GameLib.Model.Entity;

namespace GameLib.Model.ViewModel
{
    public class UserVM
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
    }

    public class UserVMProfile : Profile
	{
		public UserVMProfile()
		{
			CreateMap<UserVM, User>().ReverseMap();
		}
	}
}