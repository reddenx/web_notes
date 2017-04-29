var NoteApi = function () {
    var self = this;
    var m = {};

    self.GetAllNotes = function () {
        return ApiPromiser.Call('GET', 'json/note', null, function (data) {
            return data.map(function (item) {
                return m.GetNoteDto(item.Id, item.Name, item.Description);
            });
        });
    }
    self.GetNoteById = function (id) {
        return ApiPromiser.Call('GET', 'json/note/' + id, null, function (data) {
            return m.GetNoteDto(data.Id, data.Name, data.Description);
        });
    }
    self.Create = function (name) {
        return ApiPromiser.Call('POST', 'json/note', JSON.stringify({
            Name: name
        }), function (data) {
            return m.GetNoteDto(data.Id, data.Name, data.Description);
        });
    }
    self.Delete = function (id) {
        return ApiPromiser.Call('DELETE', 'json/note/' + id);
    }

    m.GetNoteDto = function (id, name, description) {
        return {
            id: id,
            name: name,
            description: description
        }
    }

    self.GetStrokes = function () { }
    self.AddStroke = function () { }
    self.DeleteStroke = function () { }

    self.GetTexts = function () { }
    self.AddText = function () { }
    self.DeleteText = function () { }

    self.GetLines = function () { }
    self.AddLine = function () { }
    self.DeleteLine = function () { }
}