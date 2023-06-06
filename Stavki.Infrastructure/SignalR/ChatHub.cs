﻿using Hangfire;
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
            var res = _requestService.AddComment(comment);

            var comm = new CommentDomain
            {
                RequestId = comment.RequestId,
                UserId = comment.UserId,
                CreateDate = DateTime.Now,
                Text = comment.Comment,
                Id = res.Id
            };

            await Clients.Caller.SendAsync("Receive", comm);

            await Task.Run(async () =>
            {
                await Task.Delay(10000);

                await Clients.Others.SendAsync("Receive", comm);
            });
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

            BackgroundJob.Delete(JobId);


            //var a = Clients.Others.SendAsync("Receive", comm);

            await Task.Run(async () =>
            {
                await Task.Delay(10000);

                await Clients.Others.SendAsync("Receive", comm);
            });
        }

        public async void SendDelayedMessagesJob(CommentDomain comm)
        {
            await Clients.Others.SendAsync("Receive", comm);
        }
    }
}
