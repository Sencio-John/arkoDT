import { CONTROL_IP } from "./config";

class WebSocketService {
    private socket: WebSocket | null = null;
    private readonly ip: string;

    constructor(ip: string = CONTROL_IP) { 
        this.ip = ip;
    }

    connect(onOpen: () => void, onClose: () => void, onError: (error: any) => void) {
        this.socket = new WebSocket(this.ip);

        this.socket.onopen = () => {
            console.log('Connected to the server at', this.ip);
            onOpen();
        };

        this.socket.onclose = () => {
            console.log('Disconnected from the server');
            onClose();
        };

        this.socket.onerror = (error) => {
            console.error('WebSocket error:', error);
            this.socket?.close();
            onError(error);
        };
    }

    sendMessage(message: string) {
        if (this.socket && this.socket.readyState === WebSocket.OPEN) {
            this.socket.send(message);
        }
    }

    close() {
        this.socket?.close();
    }
}

export default WebSocketService;
