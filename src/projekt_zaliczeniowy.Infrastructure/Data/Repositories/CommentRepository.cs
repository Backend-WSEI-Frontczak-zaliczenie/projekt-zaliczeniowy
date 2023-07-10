﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;

namespace projekt_zaliczeniowy.Infrastructure.Data.Repositories;
internal class CommentRepository: ICommentRepository
{
  private readonly ApplicationDbContext _context;

  public CommentRepository(ApplicationDbContext context)
  {
    this._context = context;
  }

  public Comment Add(int restaurantId, string textContent)
  {
    var lastComment = _context.Comments.OrderBy(a => a.Id).LastOrDefault();
    _context.ChangeTracker.Clear();
    int lastCommentId = (lastComment != null && lastComment?.Id != null) ? lastComment.Id : 0;
    Comment comment = new Comment
    {
      Id = lastCommentId + 1,
      Restaurant = restaurantId,
      Comment1 = textContent
    };
    _context.Comments.Add(comment);
    _context.SaveChanges();
    return comment;
  }
}