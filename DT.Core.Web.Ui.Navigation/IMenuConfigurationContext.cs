using DT.Core.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.Core.Web.Ui.Navigation
{
    public interface IMenuConfigurationContext
    {
        ModuleMenuItems Menu { get; }
    }
}
