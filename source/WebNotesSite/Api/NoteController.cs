using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;
using WebNotesSite.Framework;
using WebNotesSite.Models.DataTypes;
using WebNotesSite.Models.Dtos;
using WebNotesSite.Models.inputs;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Api
{
    [RoutePrefix("json")]
    public class NoteController : ApiController
    {
        public NoteController()
        { }

        [HttpGet]
        [Route("note")]
        public NoteDto[] GetAllNotes()
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            var notes = user.Notes.Select(note => new NoteDto(note.ToData())).ToArray();
            return notes;
        }

        [HttpGet]
        [Route("note/{noteId:guid}")]
        public NoteDto GetNoteById(Guid noteId)
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            var note = user.Notes.FirstOrDefault(n => n.Id == noteId);

            if (note == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var noteDto = new NoteDto(note.ToData());
            return noteDto;
        }

        [HttpPost]
        [Route("note")]
        public NoteDto CreateNote([FromBody]NewNoteInput noteInput)
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            var newNote = user.AddNote(noteInput.Name);
            var noteDto = new NoteDto(newNote.ToData());
            return noteDto;
        }

        [HttpDelete]
        [Route("note/{noteId:guid}")]
        public void DeleteNote(Guid noteId)
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            user.DeleteNote(noteId);
        }




        [HttpGet]
        [Route("note/{noteId:guid}/strokes")]
        public StrokeDto[] GetAllNoteStrokes(Guid noteId)
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            var strokes = user.Notes.FirstOrDefault(n => n.Id == noteId)?.Strokes;
            if (strokes == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var strokeDtos = strokes.Select(stroke => new StrokeDto(stroke.ToData())).ToArray();
            return strokeDtos;
        }

        [HttpPost]
        [Route("note/{noteId:guid}/strokes")]
        public StrokeDto AddStroke(Guid noteId, [FromBody]NewStrokeInput strokeInput)
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            var note = user.Notes.FirstOrDefault(n => n.Id == noteId);

            if(strokeInput.Path == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var stroke = note.AddStroke(
                new BrushColor(strokeInput.R, strokeInput.B, strokeInput.G, strokeInput.A) { },
                new BrushSize(strokeInput.Radius) { },
                strokeInput.Path);

            var strokeDto = new StrokeDto(stroke.ToData());
            return strokeDto;
        }

        [HttpDelete]
        [Route("note/{noteId:guid}/strokes/{strokeId:guid}")]
        public void DeleteStroke(Guid noteId, Guid strokeId)
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            var note = user.Notes.FirstOrDefault(n => n.Id == noteId);

            if (note == null || !note.Strokes.Any(s => s.Id == strokeId))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            note.RemoveStroke(strokeId);
        }




        [HttpGet]
        [Route("note/{noteId:guid}/textelements")]
        public TextElementDto[] GetAllTextElements(Guid noteId)
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            var note = user?.Notes.FirstOrDefault(n => n.Id == noteId);

            if(note == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var textDtos = note.TextElements.Select(t => new TextElementDto(t.ToData())).ToArray();
            return textDtos;
        }

        [HttpPost]
        [Route("note/{noteId:guid}/textelements")]
        public TextElementDto AddTextElement(Guid noteId, [FromBody]NewTextElementInput input)
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            var note = user?.Notes.FirstOrDefault(n => n.Id == noteId);

            if (note == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var element = note.AddText(input.Text,
                input.FontSize, 
                new BrushColor(input.R,
                    input.B,
                    input.G,
                    input.A));

            var dto = new TextElementDto(element.ToData());
            return dto;
        }

        [HttpDelete]
        [Route("note/{noteId:guid}/textblocks/{textElementId:guid}")]
        public void DeleteTextElement(Guid noteId, Guid textElementId)
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            var note = user?.Notes.FirstOrDefault(n => n.Id == noteId);

            if (note == null || note.TextElements.Any(t => t.Id == textElementId))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            note.RemoveText(textElementId);
        }




#error you left off here, finish the lines, then move on to the constructors of the DTOs

        //[HttpGet]
        //[Route("note/{noteId:guid}/lineelements")]
        //public LineElementDto[] GetAllLines(Guid noteId) { }

        //[HttpPost]
        //[Route("note/{noteId:guid}/lineelements")]
        //public LineElementDto AddLine(Guid noteId, [FromBody]NewLineElementInput input) { }

        //[HttpDelete]
        //[Route("note/{noteId:guid}/lineelements/{lineElementId:guid}")]
        //public void DeleteLine(Guid noteId, Guid lineElementId) { }
































        //[HttpGet]
        //[Route("note/connect")]
        //public HttpResponseMessage OpenSocketConnection()
        //{
        //    if(HttpContext.Current.IsWebSocketRequest ||
        //        HttpContext.Current.IsWebSocketRequestUpgrading)
        //    {
        //        HttpContext.Current.AcceptWebSocketRequest(HandleSocket);
        //        return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        //    }


        //    var repo = new DataRepository(HttpContext.Current.Cache);
        //    throw new NotImplementedException();
        //}

        //private Task HandleSocket(AspNetWebSocketContext context)
        //{
        //    var socket = context.WebSocket;

        //    //receive
        //    var receiveThread = new Thread(new ThreadStart(async () =>
        //    {
        //        var inputBuffer = WebSocket.CreateServerBuffer(2048);

        //        while (true)
        //        {
        //            var readResult = await socket.ReceiveAsync(inputBuffer, CancellationToken.None);
        //        }
        //    }));
        //    receiveThread.IsBackground = true;
        //    receiveThread.Start();

        //    //send
        //    while(true)
        //    {
        //        if(socket.State != WebSocketState.Open)
        //        {
        //            break;
        //        }



        //        byte[] sampleData = { };//would be pumping message queue
        //        var sendSegment = new ArraySegment<byte>(sampleData);
        //        await socket.SendAsync(sendSegment, WebSocketMessageType.Binary, true, CancellationToken.None);
        //    }
        //}
    }
}