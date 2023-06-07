using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Stavki.Data.Data;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.Services.Interfaces;

namespace Stavki.Infrastructure.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IRequestService _requestService;
        private string? JobId;
        private int Count = 0;
        private static CommentDomain comm1;
        private static CommentDomain comm2;



        public ChatHub(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Send(CommentInfo comment)
        {
            var res = _requestService.AddComment(comment);

            var comm = new CommentDomain
            {
                RequestId = comment.RequestId,
                UserId = comment.UserId,
                CreateDate = DateTime.Now,
                Text = comment.Comment,
                Id = res.Id
            };

            if (Count == 1)
                comm1 = comm;

            await Clients.Caller.SendAsync("Receive", comm);

            if (Count == 2)
            {
                await Task.Delay(10000);

                await Clients.Others.SendAsync("Receive", comm1);
            }
        }

        public async Task Update(CommentInfo comment)
        {
            _requestService.UpdateComment(comment);

            var comm = new CommentDomain
            {
                RequestId = comment.RequestId,
                UserId = comment.UserId,
                CreateDate = DateTime.Now,
                Text = comment.Comment,
                Id = comment.Id
            };

            await Task.Delay(10000);

            await Clients.Others.SendAsync("Receive", comm);
        }
    }
}
