using AutoMapper;
using ClientService.Application.Posts.Commands;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Common.Mappings
{
    public class PostProfiles : MappingProfiles
    {
        public PostProfiles() {
            CreateMap<CreatePostRequest, Post>();
            CreateMap<Post, PostResponse>();

            CreateMap<UpdatePostRequest, Post>();

            CreateMap<ApplyPostRequest, PostApplication>();
            CreateMap<PostApplication, ApplyPostResponse>();

            CreateMap<PostApplication, PostApplicationResponse>();

            CreateMap<Post, PostDetailResponse>();
        }
    }
}
