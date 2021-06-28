using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Comment
    {
        public int CommentId { set; get; }
        public string C_Content { set; get; }
        public int NewsId { set; get; }
        public News News { set; get; }
    }
}
