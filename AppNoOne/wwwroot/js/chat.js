const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathup")
    .build();

connection.start().then(() => {
    connection.on("ReceiveMessage", (user, message) => {
        console.log(user)
        console.log(message)
    });
}).catch(err => console.error(err));

function sendMessage(user, message) {
    connection.invoke("SendMessage", user, message).catch(err => console.error(err));
}