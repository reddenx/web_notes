var CanvasModel = function (elementName) {
    var self = this;
    var m = {};

    m.canvas = document.getElementById(elementName);
    m.context = m.canvas.getContext('2d');
    m.context.backag
    m.inputDown = false;

    self.InputDown = function (x, y)
    {
        if (m.inputDown) {
            return;
        }

        m.inputDown = true;

        m.context.moveTo(x-1, y-1);//the one offset is a quick hack that can be fixed later to make the canvas draw
    }

    self.InputUp = function (x, y)
    {
        if (!m.inputDown) {
            return;
        }

        m.inputDown = false;

        //m.context.moveTo(x, y);
        m.context.lineTo(x, y);
        m.context.stroke();
    }

    self.InputMove = function (x, y)
    {
        if (m.inputDown) {
            //m.context.moveTo(x, y);
            m.context.lineTo(x, y);
            m.context.stroke();
        }
    }

    m.canvas.onmousedown = function (e) {
        self.InputDown(e.layerX, e.layerY);
    }

    m.canvas.onmouseup = function (e) {
        self.InputUp(e.layerX, e.layerY);
    }

    m.canvas.onmousemove = function (e) {
        self.InputMove(e.layerX, e.layerY);
    }

    return self;
}