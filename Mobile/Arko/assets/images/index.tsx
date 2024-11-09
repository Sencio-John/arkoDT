import React, { useState, useEffect, useRef } from 'react';
import { View, Text, Button, StyleSheet, Platform } from 'react-native';
import { RTCSessionDescription, RTCView, mediaDevices, RTCPeerConnection, RTCIceCandidate } from 'react-native-webrtc';


const SIGNALING_SERVER = 'ws://10.0.2.2:8080';  // Emulator case

console.log(`Signaling server URL: ${SIGNALING_SERVER}`);

const VideoCall = () => {
  const [localStream, setLocalStream] = useState<MediaStream | null>(null);
  const [remoteStream, setRemoteStream] = useState<MediaStream | null>(null);
  const [peerConnection, setPeerConnection] = useState<RTCPeerConnection | null>(null);
  const [isCallActive, setIsCallActive] = useState(false);
  const [statusMessage, setStatusMessage] = useState('Waiting for connection...');
  
  const localVideoRef = useRef<RTCView | null>(null);
  const remoteVideoRef = useRef<RTCView | null>(null);
  const socketRef = useRef<WebSocket | null>(null);

  const iceServers = [{ urls: 'stun:stun.l.google.com:19302' }];

  // WebSocket connection setup
  useEffect(() => {
    const socket = new WebSocket(SIGNALING_SERVER);
    socketRef.current = socket;

    socket.onopen = () => setStatusMessage('Connected to signaling server');
    socket.onmessage = (message) => handleSignalingMessage(message);
    socket.onerror = (error) => setStatusMessage('Error with signaling server');
    socket.onclose = () => setStatusMessage('Disconnected from signaling server');

    // Get local media stream
    mediaDevices.getUserMedia({ video: true, audio: true })
      .then((stream) => {
        setLocalStream(stream);
        if (localVideoRef.current) {
          localVideoRef.current.srcObject = stream;
        }
      })
      .catch((err) => console.error('Failed to get local stream:', err));

    // Cleanup
    return () => {
      if (socketRef.current) socketRef.current.close();
      if (localStream) {
        localStream.getTracks().forEach(track => track.stop());
      }
    };
  }, []);

  // Handle incoming signaling messages
  const handleSignalingMessage = (message: MessageEvent) => {
    try {
      const data = JSON.parse(message.data);
      console.log('Received signaling message:', data);

      if (data.type === 'offer') {
        handleOffer(data.offer);
      } else if (data.type === 'answer') {
        handleAnswer(data.answer);
      } else if (data.type === 'candidate') {
        handleCandidate(data.candidate);
      }
    } catch (error) {
      console.error('Error parsing signaling message:', error);
    }
  };

  const createPeerConnection = () => {
    const pc = new RTCPeerConnection({ iceServers });

    if (localStream) {
      localStream.getTracks().forEach(track => pc.addTrack(track, localStream));
    }

    pc.onicecandidate = (event) => {
      if (event.candidate) {
        if (socketRef.current && socketRef.current.readyState === WebSocket.OPEN) {
          socketRef.current.send(JSON.stringify({
            type: 'candidate',
            candidate: event.candidate,
          }));
        }
      }
    };

    pc.ontrack = (event) => {
      setRemoteStream(event.streams[0]);
      if (remoteVideoRef.current) {
        remoteVideoRef.current.srcObject = event.streams[0];
      }
    };

    return pc;
  };

  const startCall = () => {
    if (isCallActive) return;
    setStatusMessage('Starting call...');
    
    const pc = createPeerConnection();
    setPeerConnection(pc);
    
    pc.createOffer()
      .then((offer) => {
        return pc.setLocalDescription(offer);
      })
      .then(() => {
        socketRef.current?.send(JSON.stringify({ type: 'offer', offer: pc.localDescription }));
        setStatusMessage('Waiting for answer...');
      })
      .catch((error) => {
        console.error('Error creating offer:', error);
      });

    setIsCallActive(true);
  };

  const handleOffer = (offer) => {
    const pc = createPeerConnection();
    setPeerConnection(pc);

    const description = new RTCSessionDescription(offer);
    pc.setRemoteDescription(description)
      .then(() => {
        return pc.createAnswer();
      })
      .then((answer) => {
        return pc.setLocalDescription(answer);
      })
      .then(() => {
        socketRef.current?.send(JSON.stringify({ type: 'answer', answer: pc.localDescription }));
        setStatusMessage('Call answered');
      })
      .catch((error) => {
        console.error('Error handling offer:', error);
      });
  };

  const handleAnswer = (answer) => {
    const description = new RTCSessionDescription(answer);
    peerConnection?.setRemoteDescription(description)
      .catch((error) => {
        console.error('Error setting remote description:', error);
      });
  };

  const handleCandidate = (candidate) => {
    const iceCandidate = new RTCIceCandidate(candidate);
    peerConnection?.addIceCandidate(iceCandidate)
      .catch((error) => {
        console.error('Error adding ICE candidate:', error);
      });
  };

  const endCall = () => {
    if (peerConnection) {
      peerConnection.close();
      setPeerConnection(null);
    }

    if (localStream) {
      localStream.getTracks().forEach(track => track.stop());
      setLocalStream(null);
    }

    if (remoteStream) {
      remoteStream.getTracks().forEach(track => track.stop());
      setRemoteStream(null);
    }

    setIsCallActive(false);
    setStatusMessage('Call ended');
  };

  return (
    <View style={styles.container}>
      <Text style={styles.header}>Bidyakol</Text>
      <Text style={styles.status}>{statusMessage}</Text>

      <View style={styles.videoContainer}>
        <RTCView streamURL={localStream ? localStream.toURL() : ''} style={styles.localVideo} mirror={true} />
        <RTCView streamURL={remoteStream ? remoteStream.toURL() : ''} style={styles.remoteVideo} mirror={true} />
      </View>

      {!isCallActive ? (
        <Button title="Start Call" onPress={startCall} />
      ) : (
        <Button title="End Call" onPress={endCall} />
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  container: { flex: 1, justifyContent: 'center', alignItems: 'center', padding: 10 },
  header: { fontSize: 24, marginBottom: 20 },
  status: { fontSize: 18, marginBottom: 10, color: 'gray' },
  videoContainer: { flexDirection: 'row', marginBottom: 20 },
  localVideo: { width: 150, height: 200, borderWidth: 1, marginRight: 10 },
  remoteVideo: { width: 150, height: 200, borderWidth: 1 },
});

export default VideoCall;