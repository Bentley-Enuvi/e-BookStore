﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreAPI.Domain.Entities;

public class PurchaseHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string BookId { get; set; }
    public DateTime PurchasedOn { get; set; }
}