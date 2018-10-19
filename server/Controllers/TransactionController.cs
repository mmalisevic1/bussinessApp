using server.data;
using server.services;
using server.services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace server.Controllers
{
    [RoutePrefix("api")]
    public class TransactionController : ApiController
    {
        TransactionService _transactionService;
        GlobomanticsContext _globomanticsContext;

        public TransactionController()
        {
            _globomanticsContext = new GlobomanticsContext();
            _transactionService = new TransactionService(_globomanticsContext);
        }

        [HttpGet]
        [Route("transactions/{year}/{month}")]
        public IHttpActionResult GetTransactions(int year, int month)
        {
            if (Request.Headers.TryGetValues("userId", out IEnumerable<string> values))
            {
                return Ok(_transactionService.GetTransactions(Convert.ToInt64(values.FirstOrDefault()), year, month));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("balance/{year}/{month}")]
        public IHttpActionResult GetBalance(int year, int month)
        {
            if (Request.Headers.TryGetValues("userId", out IEnumerable<string> values))
            {
                return Ok(_transactionService.GetRunningBalance(Convert.ToInt64(values.FirstOrDefault()), year, month));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("transaction")]
        public async Task<IHttpActionResult> PostTransaction([FromBody]TransactionDTO transaction)
        {
            return Created(Request.RequestUri, await _transactionService.CreateTransaction(transaction));
        }
    }
}
