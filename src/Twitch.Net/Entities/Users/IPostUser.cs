using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
{
    public interface IPostUser : IUser
    {
        string Type { get; set; }
    }
}
