const WebSocket = require('ws');

// Create a WebSocket server listening on port 8080
const wss = new WebSocket.Server({ port: 8080 });

wss.on('connection', (ws) => {
  console.log('Peer connected');

  // Handle messages sent by clients
  ws.on('message', (message) => {
    console.log('Received message:', message);
    
    // Broadcast the message to all other connected clients
    wss.clients.forEach((client) => {
      if (client !== ws && client.readyState === WebSocket.OPEN) {
        client.send(message);
      }
    });
  });

  // Handle peer disconnection
  ws.on('close', () => {
    console.log('Peer disconnected');
  });
});

console.log('Signaling server running on ws://192.168.1.2:8080');
