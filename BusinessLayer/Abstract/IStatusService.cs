﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStatusService : IGenericService<Status>
    {
		public void TMakePassiveStatus(int id);
		public void TMakeActiveStatus(int id);
	}
}
