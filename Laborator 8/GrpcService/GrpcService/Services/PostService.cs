using GrpcService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostComment.APIStatic;
using Grpc.Core;
using PostComment;
using Google.Protobuf.WellKnownTypes;

namespace GrpcService
{
    public class PostService : RemotePost.RemotePostBase
    {
        private readonly ILogger<PostService> _logger;

        public PostService(ILogger<PostService> logger)
        {
            _logger = logger;
        }

        public override Task<PostModel> GetPostInfo(PostLookUpModel request, ServerCallContext context)
        {
            Post post = API.GetPostById(request.Id);

            return Task.FromResult(new PostModel
            {
                Id = post.PostId,
                Description = post.Description,
                Domain = post.Domain,
                Date = Timestamp.FromDateTime(post.Date)
            });
        }

        public override Task<PostActionReply> AddPost(PostModel request, ServerCallContext context)
        {
            Post post = new Post();
            post.Description = request.Description;
            post.Domain = request.Domain;
            post.Date = request.Date.ToDateTime();

            var success = API.AddPost(post);

            if (success)
                return Task.FromResult(new PostActionReply
                {
                    Message = "Success"
                });
            else
                return Task.FromResult(new PostActionReply
                {
                    Message = "Fail"
                });
        }

        public override Task<PostActionReply> UpdatePost(PostModel request, ServerCallContext context)
        {
            Post post = new Post();
            post.Description = request.Description;
            post.Domain = request.Domain;
            post.Date = request.Date.ToDateTime();

            var success = API.UpdatePost(post);

            if (success != null)
                return Task.FromResult(new PostActionReply
                {
                    Message = "Success"
                });
            else
                return Task.FromResult(new PostActionReply
                {
                    Message = "Fail"
                });
        }

        public override Task<PostActionReply> DeletePost(PostLookUpModel request, ServerCallContext context)
        {
            var success = API.DeletePost(request.Id);

            if (success != 0)
                return Task.FromResult(new PostActionReply
                {
                    Message = "Success"
                });
            else
                return Task.FromResult(new PostActionReply
                {
                    Message = "Fail"
                });
        }
    }
}
