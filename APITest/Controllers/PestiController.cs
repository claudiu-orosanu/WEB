//using APITest.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace APITest.Controllers
//{
//    public class PestiController : ApiController
//    {
//        public static List<Peste> Pesti = new List<Peste>()
//        {
//            new Peste { Id = 1, Culoare = "Rosu", Specie = "Rosioara", EsteRapitor = false },
//            new Peste { Id = 2, Culoare = "Galben", Specie = "Galbior", EsteRapitor = false }
//        };

//        [HttpGet]
//        [Route("pesti")]
//        public List<Peste> All()
//        {
//            return Pesti;
//        }

//        [HttpGet]
//        [Route("pesti/{id}")]
//        public HttpResponseMessage GetById(int id)
//        {
//            var peste = Pesti.Where(p => p.Id == id).SingleOrDefault();
//            if(peste != null)
//            {
//                return Request.CreateResponse(HttpStatusCode.OK, peste);
//            }
//            else
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, $"Nu s-a gasit resursa cu id-ul {id},");
//            }
//        }

//        [HttpPost]
//        [Route("pesti")]
//        public IHttpActionResult Add([FromBody]Peste peste)
//        {
//            if(Pesti.Any(p => p.Specie == peste.Specie))
//            {
//                return BadRequest("Exista deja un peste cu aceasta specie!");
//            }
//            else
//            {
//                peste.Id = Pesti.Max(p => p.Id) + 1;
//                Pesti.Add(peste);

//                return Created($"/pesti/{peste.Id}", peste);

//                //return Request.CreateResponse(HttpStatusCode.Created, $"/pesti/{peste.Id}"); ..cu HttpResponse
//            }
//        }

//        [HttpPut]
//        [Route("pesti/{id}")]
//        public IHttpActionResult Update(int id, [FromBody] Peste peste)
//        {
//            var pesteDeModificat = Pesti.SingleOrDefault(p => p.Id == id);
//            if (pesteDeModificat != null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                pesteDeModificat.Culoare = peste.Culoare;
//                pesteDeModificat.EsteRapitor = peste.EsteRapitor;
//                pesteDeModificat.Specie = peste.Specie;
//                return Ok();
//            }
//        }
//    }
//}
