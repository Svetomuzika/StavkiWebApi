using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stavki.Data.Data
{
    public class CommentInfo
    {
        public int CommentId { get; set; }

        public string Comment { get; set; }

        public int UserId { get; set; }

        public int RequestId { get; set; }
    }
}
