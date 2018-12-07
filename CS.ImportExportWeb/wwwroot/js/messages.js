
var connection = new signalR.HubConnectionBuilder().withUrl("/messageshub").build();

connection.on("receivemessage", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var li = document.createElement("li");
    li.textContent = msg;
    document.getElementById('messagesList').appendChild(li);
    document.getElementById('messagesList').style.display = 'none';
    document.getElementById('messagesList').style.display = 'block';
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
