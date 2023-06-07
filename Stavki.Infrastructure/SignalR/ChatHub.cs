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



        public ChatHub(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Send(CommentInfo comment)
        {
            Consts.Hub.Counts++;

            var res = _requestService.AddComment(comment);

            var comm = new CommentDomain
            {
                RequestId = comment.RequestId,
                UserId = comment.UserId,
                CreateDate = DateTime.Now,
                Text = comment.Comment,
                Id = res.Id
            };

            if (Consts.Hub.Counts == 1)
                Consts.Hub.Comm1 = comm;

            await Clients.Caller.SendAsync("Receive", comm);

            if (Consts.Hub.Counts == 2)
            {
                await Task.Delay(10000);

                await Clients.Others.SendAsync("Receive", Consts.Hub.Comm1);
            }
        }

        public async Task Update(CommentInfo comment)
        {
            var ab = _requestService.UpdateComment(comment);

            var comm = new CommentDomain
            {
                RequestId = comment.RequestId,
                UserId = comment.UserId,
                CreateDate = DateTime.Now,
                Text = comment.Comment,
                Id = comment.Id
            };

            await Clients.Others.SendAsync("Receive", comm);
        }
    }
}
