using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
public interface ICommentRepository
{
  Comment Add(int restaurantId, string textContent);

  Comment? Edit(int commentId, string textContent);
}
